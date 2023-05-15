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
Sept. 22nd, 2020
Thumbsticks And Triggers
Purpose: To create a program that can respond to specific inputs from the thumbsticks and triggers!
*/
namespace ThumbsticksAndTriggers
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declares a GamePadState class variable for the player's gamepad
        GamePadState pad;

        //Declares a Color class variable for the background colour
        Color backgroundColour;

        //Declares byte class variables for the respective RGB intensities
        byte redIntensity, greenIntensity, blueIntensity;

        //Declares a byte class variable that determines the speed at which the colour changes
        byte lightChange;

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

            //Queries player one's gamepad for input
            pad = GamePad.GetState(PlayerIndex.One);

            //Changes the increment by which the light is changed as the left thumbsick is moved to the left/right
            lightChange = (byte)(2 * pad.ThumbSticks.Left.X);

            //If the right trigger is pressed, then the increment by which the light increases is increased
            if (pad.Triggers.Right > 0)
                lightChange = (byte)(2 * pad.Triggers.Right);
            //If the left trigger is pressed, then the increment by which the light increases is decreased
            if (pad.Triggers.Left > 0)
                lightChange = (byte)((-2) * pad.Triggers.Left);

            redIntensity += lightChange;
            greenIntensity += lightChange;
            blueIntensity += lightChange;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //Constructs backgroundColour and assigns it custom RGB values
            //(Those of redIntensity, greenIntensity, and blueIntensity)
            backgroundColour = new Color(redIntensity, greenIntensity, blueIntensity);

            GraphicsDevice.Clear(backgroundColour);

            base.Draw(gameTime);
        }
    }
}
