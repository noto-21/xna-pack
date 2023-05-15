using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
/*
 * DrumPad
 * October 14th, 2020
 * Purpose: To create a game that allows the player to play/manipulate sounds!
 */
namespace DrumPad
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declares SoundEffect class varibales for the .wav sounds to be used
        SoundEffect cymbolTing, kick, snare, top;

        //Creates a Song class variable
        Song tune;

        //Declares three float variables that determine the volume, pitch, and pan of the sound effects to be played
        float volume, pitch, pan;

        //Declares a Rectangle class variable that determines the image to be drawn onto the screen
        Rectangle instrumentRec;

        //Declares a Texture2D class variable that determines the image of the instrument to be drawn
        Texture2D cymbalPic, kickPic, snarePic, topPic, defaultPic, instrumentPic;

        //Declares two GamePadState class variables, one for the "old" state of the gamepad, and one for the current state
        GamePadState oldPad, pad;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initializes the floats
            volume = 0.5f;
            pitch = 0;
            pan = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Loads the .wav files into the sound effects
            cymbolTing = Content.Load<SoundEffect>("cymbolTing");
            kick = Content.Load<SoundEffect>("kick");
            snare = Content.Load<SoundEffect>("snare");
            top = Content.Load<SoundEffect>("top");

            //Loads the .mp3 file into song
            tune = Content.Load<Song>("Song");

            //Loads the Texture2D variables
            cymbalPic = Content.Load<Texture2D>("cymbalImage");
            kickPic = Content.Load<Texture2D>("kickDrumImage");
            snarePic = Content.Load<Texture2D>("snareDrumImage");
            topPic = Content.Load<Texture2D>("topImage");
            defaultPic = Content.Load<Texture2D>("drumSet");

            instrumentPic = defaultPic;

            //Plays "tune" and sets it to loop
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(tune);
                MediaPlayer.Volume = .1f;
                MediaPlayer.IsRepeating = true;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //Initializes pad
            pad = GamePad.GetState(PlayerIndex.One);

            //Controls the "volume" float using the d-pad, and prevents Exceptions
            if (oldPad.DPad.Up == ButtonState.Released && pad.DPad.Up == ButtonState.Pressed)
            {
                volume += .1f;

                if (volume > 1f)
                {
                    volume = 1f;
                }
            }
            if (oldPad.DPad.Down == ButtonState.Released && pad.DPad.Down == ButtonState.Pressed)
            {
                volume -= .1f;

                if (volume < 0f)
                {
                    volume = 0f;
                }
            }

            //Sets the pitch equal to the value of the left thumbstick press in the y-direction
            pitch = pad.ThumbSticks.Left.Y;
            if (pitch > 1f)
                pitch = 1f;
            if (pitch < -1f)
                pitch = -1f;

            //Sets the pan equal to the value of the left thumbstick press in the x-direction
            pan = pad.ThumbSticks.Left.X;
            if (pan > 1f)
                pan = 1f;
            if (pan < -1f)
                pan = -1f;

            if (oldPad.Buttons.A == ButtonState.Released && pad.Buttons.A == ButtonState.Pressed)//Plays "snare" when A is pressed
            {
                snare.Play(volume, pitch, pan);

                instrumentPic = snarePic;//Sets instrumentPic to snarePic
            }
            if (oldPad.Buttons.B == ButtonState.Released && pad.Buttons.B == ButtonState.Pressed)//Plays "cymbolTing" when B is pressed
            {
                cymbolTing.Play(volume, pitch, pan);

                instrumentPic = cymbalPic;//Sets instrumentPic to cymbalPic
            }
            if (oldPad.Buttons.X == ButtonState.Released && pad.Buttons.X == ButtonState.Pressed)//Plays "kick" when X is pressed
            {
                kick.Play(volume, pitch, pan);

                instrumentPic = kickPic;//Sets instrumentPic to kickPic
            }
            if (oldPad.Buttons.Y == ButtonState.Released && pad.Buttons.Y == ButtonState.Pressed)//Plays "top" when Y is pressed
            {
                top.Play(volume, pitch, pan);

                instrumentPic = topPic;//Sets instrumentPic to topPic
            }

            //Sets oldPad to pad
            oldPad = pad;

            //Initializes instrumentRec
            instrumentRec = new Rectangle(GraphicsDevice.Viewport.Width / 2 - instrumentPic.Width / 2,
                GraphicsDevice.Viewport.Height / 2 - instrumentPic.Height / 2,
                instrumentPic.Width, instrumentPic.Height);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Initiates, coordinates, and ends spriteBatch
            spriteBatch.Begin();
            spriteBatch.Draw(instrumentPic, instrumentRec, Color.White);//Draws the instrument
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
