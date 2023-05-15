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
 * Sept. 25th, 2020
 * Health Bar
 * Purpose: To create a game that utilizes a health bar mechanic!
*/
namespace HealthBar
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declares a Random class variable
        Random randomLocation;

        //Declares two Rectangle class variables for the player and parking lot
        Rectangle playerRec, parkingLotRec, healthBarRec;

        //Declares a list of Rectangle class variables for health power-ups
        List<Rectangle> medBottleRecList;

        //Declares all Texture2D class variables for the player and parking lot
        Texture2D playerPicRight, playerPicLeft, playerPicUp, playerPicDown, parkingLotPic, medBottlePic, healthBarPic;
        //Controls which texture is drawn onto playerRec
        Texture2D currentTexture;

        //Declares an Int class variable that determines movement speed
        int movementSpeed;

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
            //Constructs the random number generator
            randomLocation = new Random();

            //Constructs the Rectangles
            parkingLotRec = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            playerRec = new Rectangle(0, (GraphicsDevice.Viewport.Height - 100), 100, 100);

            //Constructs the medicine bottles and assigns them to random locations
            medBottleRecList = new List<Rectangle>();
            for (int i = 0; i < 3; ++i)
            {
                medBottleRecList.Add(new Rectangle(randomLocation.Next(playerRec.Width, GraphicsDevice.Viewport.Width),
                randomLocation.Next(0, (GraphicsDevice.Viewport.Height - playerRec.Height)), 30, 30));
            }

            //Constructs the health bar
            healthBarRec = new Rectangle(0, 0, (GraphicsDevice.Viewport.Width/4), 12);

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
            playerPicRight = Content.Load<Texture2D>("RaulRight");
            playerPicLeft = Content.Load<Texture2D>("RaulLeft");
            playerPicUp = Content.Load<Texture2D>("RaulUp");
            playerPicDown = Content.Load<Texture2D>("RaulDown");
            parkingLotPic = Content.Load<Texture2D>("ParkingLot");
            medBottlePic = Content.Load<Texture2D>("MedBottle");
            healthBarPic = Content.Load<Texture2D>("HealthBarTexture");
            //Designates playerRec's default texture
            currentTexture = playerPicUp;
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

            //If the right trigger is pressed, or if the number keys are pressed, then the amount by which the player moves is increased
            if (pad1.ThumbSticks.Left.X == 0 || pad1.ThumbSticks.Left.Y == 0 || keyboard1.IsKeyUp(Keys.W) || keyboard1.IsKeyUp(Keys.S)
                || keyboard1.IsKeyUp(Keys.A) || keyboard1.IsKeyUp(Keys.D))
                movementSpeed = 0;

            //Moves playerRec based on player input
            if (keyboard1.IsKeyDown(Keys.W) || pad1.ThumbSticks.Left.Y > 0)
            {
                movementSpeed = 7;
                playerRec.Y -= movementSpeed;

                currentTexture = playerPicUp;//Changes the texture drawn onto
                                                //ambulanceRec based on player input
                                                //Prevents the ambulance from going off the screen
                if (playerRec.Top < 0)
                    playerRec.Y += movementSpeed;
            }
            if (keyboard1.IsKeyDown(Keys.S) || pad1.ThumbSticks.Left.Y < 0)
            {
                movementSpeed = 7;
                playerRec.Y += movementSpeed;

                currentTexture = playerPicDown;//Changes the texture drawn onto
                                                  //ambulanceRec based on player input
                                                  //Prevents the ambulance from going off the screen
                if (playerRec.Bottom > GraphicsDevice.Viewport.Height)
                    playerRec.Y -= movementSpeed;
            }
            if (keyboard1.IsKeyDown(Keys.A) || pad1.ThumbSticks.Left.X < 0)
            {
                movementSpeed = 7;
                playerRec.X -= movementSpeed;

                currentTexture = playerPicLeft;//Changes the texture drawn onto
                                                  //ambulanceRec based on player input
                                                  //Prevents the ambulance from going off the screen
                if (playerRec.Left < 0)
                    playerRec.X += movementSpeed;
            }
            if (keyboard1.IsKeyDown(Keys.D) || pad1.ThumbSticks.Left.X > 0)
            {
                movementSpeed = 7;
                playerRec.X += movementSpeed;

                currentTexture = playerPicRight;//Changes the texture drawn onto
                                                   //ambulanceRec based on player input
                                                   //Prevents the ambulance from going off the screen
                if (playerRec.Right > GraphicsDevice.Viewport.Width)
                    playerRec.X -= movementSpeed;
            }

            //Removes the medicine bottle when the player intersects with it, and increases health bar
            for (int i = 0; i < medBottleRecList.Count; ++i)
            {
                if (playerRec.Intersects(medBottleRecList[i]))
                {
                    healthBarRec.Width += (GraphicsDevice.Viewport.Width/4);

                    medBottleRecList.RemoveAt(i);
                    --i;
                }
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
            spriteBatch.Draw(parkingLotPic, parkingLotRec, Color.White);//Draws parkingLotPic onto parkingLotRec
            //Draws medBottlePic onto medBottleRecList[i]
            for (int i = 0; i < medBottleRecList.Count; ++i)
            {
                spriteBatch.Draw(medBottlePic, medBottleRecList[i], Color.White);
            }
            spriteBatch.Draw(currentTexture, playerRec, Color.White);//Draws currentTexture onto playerRec
            spriteBatch.Draw(healthBarPic, healthBarRec, Color.Red);//Draws healthBarPic onto healthBarRec
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
