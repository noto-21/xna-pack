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

namespace JakeDisplay
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declares a Texture2D class variable for our image
        Texture2D pic;

        //Declares a Rectangle class variable
        Rectangle picRec;

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
            //Constructs picRec
            picRec = new Rectangle(0, 0, 200, 200);

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

            //Assigns the image "jake.jpg" to pic
            pic = Content.Load<Texture2D>("jake");
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

            //Allows for the query of player one's gamepad
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);
            //Allows for the query of the keyboard
            KeyboardState keys = Keyboard.GetState();

            //Allows the player to manipulate the image's position
            if (pad1.DPad.Up == ButtonState.Pressed || keys.IsKeyDown(Keys.W))
                picRec.Y--;
            if (pad1.DPad.Down == ButtonState.Pressed || keys.IsKeyDown(Keys.S))
                picRec.Y++;
            if (pad1.DPad.Left == ButtonState.Pressed || keys.IsKeyDown(Keys.A))
                picRec.X--;
            if (pad1.DPad.Right == ButtonState.Pressed || keys.IsKeyDown(Keys.D))
                picRec.X++;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Begins spriteBatch
            spriteBatch.Begin();
            //Displays pic on picRec in its original colour
            spriteBatch.Draw(pic, picRec, Color.White);
            //Ends spriteBatch
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
