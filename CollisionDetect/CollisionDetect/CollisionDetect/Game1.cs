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

namespace CollisionDetect
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declares two Rectangle class variables
        Rectangle rocketRec, rocketRec1, rocketRec2, asteroidRec;

        //Declares two Texture2D class variables
        Texture2D rocketPic, asteroidPic;

        //Declares a Color class variable and assigns it the colour white
        Color recColour = Color.White;

        //Declares a KeyboardState class variable that allows for query from the keyboard
        KeyboardState keyboard1;
        //Declares a GamePadState class variable that allows for query from two gamepads
        GamePadState pad1, pad2;

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
            asteroidRec = new Rectangle(100, 100, 100, 100);

            rocketRec = new Rectangle(600, 100, 200, 100);
            rocketRec1 = new Rectangle(600, 130, 200, 50);
            rocketRec2 = new Rectangle(715, 100, 55, 100);

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
            asteroidPic = Content.Load<Texture2D>("asteroid");
            rocketPic = Content.Load<Texture2D>("rocket");
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

            //Querys the keyboard
            keyboard1 = Keyboard.GetState();
            //Querys the GamePads
            pad1 = GamePad.GetState(PlayerIndex.One);
            pad2 = GamePad.GetState(PlayerIndex.Two);

            //Moves rocketRec based on player input
            if (keyboard1.IsKeyDown(Keys.W))
            {
                rocketRec.Y--;
                rocketRec1.Y--;
                rocketRec2.Y--;
            }
            if (keyboard1.IsKeyDown(Keys.S))
            {
                rocketRec.Y++;
                rocketRec1.Y++;
                rocketRec2.Y++;
            }
            if (keyboard1.IsKeyDown(Keys.A))
            {
                rocketRec.X--;
                rocketRec1.X--;
                rocketRec2.X--;
            }
            if (keyboard1.IsKeyDown(Keys.D))
            {
                rocketRec.X++;
                rocketRec1.X++;
                rocketRec2.X++;
            }

            //Moves asteroidRec based on player input
            if (keyboard1.IsKeyDown(Keys.Up))
                asteroidRec.Y--;
            if (keyboard1.IsKeyDown(Keys.Down))
                asteroidRec.Y++;
            if (keyboard1.IsKeyDown(Keys.Left))
                asteroidRec.X--;
            if (keyboard1.IsKeyDown(Keys.Right))
                asteroidRec.X++;

            //Detects collision between asteroidRec and rocketRec, and acts upon the collison
            if (asteroidRec.Intersects(rocketRec1) || asteroidRec.Intersects(rocketRec2))
                recColour = Color.Red;
            else
                recColour = Color.White;

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
            spriteBatch.Draw(asteroidPic, asteroidRec, recColour);//Draws asteroidPic onto asteroidRec
            spriteBatch.Draw(rocketPic, rocketRec, recColour);//Draws rocketPic onto rocketRec
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
