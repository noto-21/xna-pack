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

namespace ManualMoodLight
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declares a random number generator that determines the initial intensity values of RGB
        Random randomIntensityValue = new Random();

        //Declares variables for each respective RGB colour intensity in bytes
        byte redIntensity;
        byte greenIntensity;
        byte blueIntensity;

        //Declares a boolean variable that determines if the game is in its "Game Over" state
        bool gameOver;

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
            //Assigns the intensity value variables a random byte value between 1 and 254
            //(Exclusive of 255)
            redIntensity = (byte)randomIntensityValue.Next(1, 255);
            greenIntensity = (byte)randomIntensityValue.Next(1, 255);
            blueIntensity = (byte)randomIntensityValue.Next(1, 255);

            //Initalizes gameOver as false
            gameOver = false;

            // TODO: Add your initialization logic here

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

            // TODO: use this.Content to load your game content here
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

            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);//Allows for the query of input from Player One's GamePad
            KeyboardState keyboard = Keyboard.GetState();//Allows for the query of input from a connected keyboard

            // Allows the game to exit via keyboard
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();

            //Sets the default controller rumble level
            GamePad.SetVibration(PlayerIndex.One, 0, 0);

            //Changes the intensity values when player input is given and determines the amount by which the value increases or decreases
            if (pad1.Buttons.B == ButtonState.Pressed || keyboard.IsKeyDown(Keys.R))
                redIntensity++;
            if (pad1.DPad.Left == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Left))
                redIntensity--;

            if (pad1.Buttons.A == ButtonState.Pressed || keyboard.IsKeyDown(Keys.G))
                greenIntensity++;
            if (pad1.DPad.Down == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Down))
                greenIntensity--;

            if (pad1.Buttons.X == ButtonState.Pressed || keyboard.IsKeyDown(Keys.B))
                blueIntensity++;
            if (pad1.DPad.Right == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Right))
                blueIntensity--;

            if (pad1.Buttons.Y == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Y))//Increases or decreases the intensity values of
            {                                                                      //both red and green when the yellow "Y" button/DPad Up button is
                redIntensity++;                                                   //pressed, or when the Y-key/Up-Arrow-Key is pressed, respectively
                greenIntensity++;
            }
            if (pad1.DPad.Up == ButtonState.Pressed || keyboard.IsKeyDown(Keys.Up))
            {
                redIntensity--;
                greenIntensity--;
            }

            //Controls the vibration levels and input ability of PlayerIndex.One's GamePad and the keyboard at specific intensity values
            if (redIntensity == 255 || redIntensity == 0 || greenIntensity == 255 || greenIntensity == 0 || blueIntensity == 255 || blueIntensity == 0)
                gameOver = true;
            if (gameOver)
            {
                GamePad.SetVibration(PlayerIndex.One, 1, 1);
                redIntensity = 1;
                greenIntensity = 1;
                blueIntensity = 1;
            }
            else if (!gameOver)
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
            //Allows the game (and the vibration levels) to restart via player input
            if (keyboard.IsKeyDown(Keys.M) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                gameOver = false;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Color backgroundColour;//Declares a Colour type variable identified as "backgroundColour"
            backgroundColour = new Color(redIntensity, greenIntensity, blueIntensity);//Assigns backgroundColour custom RGB intensity values
                                                                                      //(Those of redIntensity, greenIntensity, and blueIntensity)
            GraphicsDevice.Clear(backgroundColour);//Clears the screen to backgroundColour

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
