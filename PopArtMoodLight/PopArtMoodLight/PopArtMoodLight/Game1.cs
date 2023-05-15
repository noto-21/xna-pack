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
 * Oct. 2nd, 2020
 * PopArtMoodLight
 * Purpose: To create a mood light that displays four different images in different, random tints at regular intervals!
*/
namespace PopArtMoodLight
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;//Declares a SpriteFont class variable

        //Declares a two-dimensional array of Texture2D class variables
        Texture2D[][] popArtPic;

        //Declares two Constant int class variables that determine the dimesnions of the two-dimensional arrays
        const int quadrantWidth = 2, quadrantHeight = 2;

        //Declares a two-dimensional array of Rectangle class variables
        Rectangle[][] popArtRec = new Rectangle[quadrantWidth][];

        //Declares a two-dimensional array of Color class variables
        Color[][] popArtQuadrantColour = new Color[quadrantWidth][];

        //Declares a two-dimensional array of byte class variables for each respective RGB colour intensity
        byte[][] redIntensity = new byte[quadrantWidth][];
        byte[][] greenIntensity = new byte[quadrantWidth][];
        byte[][] blueIntensity = new byte[quadrantWidth][];

        //Declares a two-dimensional string array for the text to be drawn on the screen
        string[][] quadrantText;

        //Declares a random number generator that determines the initial intensity values of RGB
        Random randomIntensityValue = new Random();

        //Declares an int class variable that determines the amount of ticks that have elapsed
        int tick;

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
            //Assigns quadrantText custom strings
            quadrantText = new string[][]
            {
            new string[] {"Friendly", "Spider"},
            new string[] {"Neighborhood", "Man!"}
            };

            //Assigns the default value of tick
            tick = 0;

            //Sets the two-dimensional arrays of rectangles, intensity values, and colours
            for (int i = 0; i < popArtRec.Length; ++i)
            {
                popArtRec[i] = new Rectangle[quadrantHeight];

                popArtQuadrantColour[i] = new Color[quadrantHeight];

                redIntensity[i] = new byte[quadrantHeight];
                greenIntensity[i] = new byte[quadrantHeight];
                blueIntensity[i] = new byte[quadrantHeight];

                for (int j = 0; j < popArtRec[i].Length; ++j)
                {
                    popArtRec[i][j] = new Rectangle (((GraphicsDevice.Viewport.Width / quadrantWidth) * i),//Mathematically determines
                                                     ((GraphicsDevice.Viewport.Height / quadrantHeight) * j),//locations, widths, and heights
                                                     (GraphicsDevice.Viewport.Width / quadrantWidth),//of each rectangle
                                                     (GraphicsDevice.Viewport.Height / quadrantHeight));

                    //Assigns each variable a random starting value
                    redIntensity[i][j] = (byte)randomIntensityValue.Next(0, 256);
                    greenIntensity[i][j] = (byte)randomIntensityValue.Next(0, 256);
                    blueIntensity[i][j] = (byte)randomIntensityValue.Next(0, 256);

                    //Assigns popArtQuadrantColour the RGB values of redIntensity, greenIntensity, and blueIntensity
                    popArtQuadrantColour[i][j] = new Color (redIntensity[i][j], greenIntensity[i][j], blueIntensity[i][j]);
                }
            }

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

            //Loads images of Spider-Man into the two-dimensional popArtPic array
            popArtPic = new Texture2D[][]
            {
                new Texture2D[] {Content.Load<Texture2D>("SpiderManDesk"), Content.Load<Texture2D>("SpiderManConfused")},
                new Texture2D[] {Content.Load<Texture2D>("SpiderManSad"), Content.Load<Texture2D>("SpiderManOk")},
            };

            //Loads "SpriteFont1" into font
            font = Content.Load<SpriteFont>("SpriteFont1");
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

            //Increases tick by 1 with every update
            tick++;

            if (tick % 20 == 0)
            {
                for (int i = 0; i < popArtQuadrantColour.Length; ++i)
                {
                    for (int j = 0; j < popArtQuadrantColour[i].Length; j++)
                    {
                        //For every 20 ticks, sets redIntensity, greenIntensity, and blueIntensity to random byte values
                        redIntensity[i][j] = (byte)randomIntensityValue.Next(0, 256);
                        greenIntensity[i][j] = (byte)randomIntensityValue.Next(0, 256);
                        blueIntensity[i][j] = (byte)randomIntensityValue.Next(0, 256);

                        //Assigns popArtQuadrantColour the RGB values of redIntensity, greenIntensity, and blueIntensity
                        popArtQuadrantColour[i][j] = new Color(redIntensity[i][j], greenIntensity[i][j], blueIntensity[i][j]);
                    }
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

            //Declares a 2D-array of a Vector2 class variable that determines the position of the text on the screen,
            //and sets the position of each string using a mathematical equation
            Vector2[][] textVector = new Vector2[quadrantWidth][];
            for (int i = 0; i < quadrantText.Length; ++i)
            {
                textVector[i] = new Vector2[quadrantWidth];

                for (int j = 0; j < quadrantText[i].Length; ++j)
                {
                    textVector[i][j] = new Vector2((GraphicsDevice.Viewport.Width / (quadrantWidth * 2)) * ((quadrantWidth * i) + 1) - ((font.MeasureString(quadrantText[i][j]).X / quadrantWidth)),
                        (GraphicsDevice.Viewport.Height / (quadrantHeight * 2)) * ((quadrantHeight * j) + 1) - ((font.MeasureString(quadrantText[i][j]).Y / quadrantHeight)));
                }
            }

            //Initiates, coordinates, and ends spriteBatch
            spriteBatch.Begin();
            for (int i = 0; i < popArtRec.Length; ++i)
            {
                for (int j = 0; j < popArtRec[i].Length; ++j)
                {
                    spriteBatch.Draw(popArtPic[i][j], popArtRec[i][j], popArtQuadrantColour[i][j]);//Draws the four rectangles 
                    spriteBatch.DrawString(font, quadrantText[i][j], textVector[i][j], Color.White);//Draws each string
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
