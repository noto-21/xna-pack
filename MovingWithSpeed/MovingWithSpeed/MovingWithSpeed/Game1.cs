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
 * Sept. 24th, 2020
 * Moving With Speed
 * Purpose: To create a game that can move a sprite in response to player inputs!
*/
namespace MovingWithSpeed
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declares two Rectangle class variables for the ambulance and the parking lot
        Rectangle ambulanceRec, parkingLotRec;

        //Declares all Texture2D class variables for the ambulance and parking lot
        Texture2D ambulancePicRight, ambulancePicLeft, ambulancePicUp, ambulancePicDown, parkingLotPic;
        //Controls which texture is drawn onto ambulanceRec
        Texture2D currentTexture;

        //Declares an Int class variable that determines movement speed
        int movementSpeed;

        //Declares a Color class variable that determines the default colour tint of the sprites
        Color defaultColour = Color.White;

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
            //Constructs the Rectangles
            parkingLotRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            ambulanceRec = new Rectangle(100, 100, 100, 100);

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

            //Loads images to be assigned to the Texture2D class variables
            ambulancePicRight = Content.Load<Texture2D>("AmbulanceRight");
            ambulancePicLeft = Content.Load<Texture2D>("AmbulanceLeft");
            ambulancePicUp = Content.Load<Texture2D>("AmbulanceForward");
            ambulancePicDown = Content.Load<Texture2D>("AmbulanceBackward");
            parkingLotPic = Content.Load<Texture2D>("Hospital Parking Lot");
            //Designates ambulanceRec's default texture
            currentTexture = ambulancePicRight;
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

            //Querys the Keyboard
            KeyboardState keyboard1 = Keyboard.GetState();
            //Querys the GamePad
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);

            //If the right trigger is pressed, or if the number keys are pressed, then the amount by which the ambulance moves is increased
            if (pad1.Triggers.Right == 0)
                movementSpeed = 0;
            if (pad1.Triggers.Right > 0.2 || keyboard1.IsKeyDown(Keys.D1))
                movementSpeed = (int)2;
            if (pad1.Triggers.Right > 0.4 || keyboard1.IsKeyDown(Keys.D2))
                movementSpeed = (int)4;
            if (pad1.Triggers.Right > 0.6 || keyboard1.IsKeyDown(Keys.D3))
                movementSpeed = (int)6;
            if (pad1.Triggers.Right > 0.8 || keyboard1.IsKeyDown(Keys.D4))
                movementSpeed = (int)8;
            if (pad1.Triggers.Right == 1 || keyboard1.IsKeyDown(Keys.D5))
                movementSpeed = (int)10;

            //Moves ambulanceRec based on player input
            if (keyboard1.IsKeyDown(Keys.W) || pad1.ThumbSticks.Left.Y > 0)
            {
                ambulanceRec.Y -= movementSpeed;
                currentTexture = ambulancePicUp;//Changes the texture drawn onto
                                                //ambulanceRec based on player input
                //Prevents the ambulance from going off the screen
                if (ambulanceRec.Top < 0)
                    ambulanceRec.Y += movementSpeed;
            }
            if (keyboard1.IsKeyDown(Keys.S) || pad1.ThumbSticks.Left.Y < 0)
            {
                ambulanceRec.Y += movementSpeed;
                currentTexture = ambulancePicDown;//Changes the texture drawn onto
                                                  //ambulanceRec based on player input
                //Prevents the ambulance from going off the screen
                if (ambulanceRec.Bottom > GraphicsDevice.Viewport.Height)
                    ambulanceRec.Y -= movementSpeed;
            }
            if (keyboard1.IsKeyDown(Keys.A) || pad1.ThumbSticks.Left.X < 0)
            {
                ambulanceRec.X -= movementSpeed;
                currentTexture = ambulancePicLeft;//Changes the texture drawn onto
                                                  //ambulanceRec based on player input
                //Prevents the ambulance from going off the screen
                if (ambulanceRec.Left < 0)
                    ambulanceRec.X += movementSpeed;
            }
            if (keyboard1.IsKeyDown(Keys.D) || pad1.ThumbSticks.Left.X > 0)
            {
                ambulanceRec.X += movementSpeed;
                currentTexture = ambulancePicRight;//Changes the texture drawn onto
                                                   //ambulanceRec based on player input
                //Prevents the ambulance from going off the screen
                if (ambulanceRec.Right > GraphicsDevice.Viewport.Width)
                    ambulanceRec.X -= movementSpeed;
            }

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
            spriteBatch.Draw(parkingLotPic, parkingLotRec, defaultColour);//Draws parkingLotPic onto parkingLotRec
            spriteBatch.Draw(currentTexture, ambulanceRec, defaultColour);//Draws currentTexture onto ambulanceRec
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
