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

namespace MoodLight
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

        //Declares boolean variables that determine whether or not the RGB intensity values should increase
        bool redCountingUp = true;
        bool greenCountingUp = true;
        bool blueCountingUp = true;
        
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
            //Assigns the intensity value variables a random byte value between 0 and 256
            //(Exclusive of 256)
            redIntensity = (byte)randomIntensityValue.Next(0, 256);
            greenIntensity = (byte)randomIntensityValue.Next(0, 256);
            blueIntensity = (byte)randomIntensityValue.Next(0, 256);

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

            /*Increases the intensity values by one per update and determines when each value is increased,
              and the amount by which their value increases*/
            if (redIntensity == 255)
                redCountingUp = false;
            else if (redIntensity == 0)
                redCountingUp = true;
            if (redCountingUp)
                redIntensity++;
            else
                redIntensity -= (byte)17;

            if (greenIntensity == 255)
                greenCountingUp = false;
            else if (greenIntensity == 0)
                greenCountingUp = true;
            if (greenCountingUp)
                greenIntensity += (byte)51;
            else
                greenIntensity -= (byte)17;

            if (blueIntensity == 255)
                blueCountingUp = false;
            else if (blueIntensity == 0)
                blueCountingUp = true;
            if (blueCountingUp)
                blueIntensity += (byte)5;
            else
                blueIntensity -= (byte)85;

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

            if (redIntensity % 2 == 0)
                GraphicsDevice.Clear(Color.Black);//Clears the screen to black every other tick
            else
                GraphicsDevice.Clear(backgroundColour);//Clears the screen to backgroundColour

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
