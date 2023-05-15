#define test//Enables certain code to run only while test is active

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
 * October 9th, 2020
 * Button Bash
 * Purpose: To create a game that can take input from multiple controllers!
 */
namespace ButtonBash
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;//Declares a SpriteFont class variable

        //Declares a constant int class variable that holds the maximum amount of gamepad controllers
        const int playerAmount = 4;

        //Declares arrays of int class variables that hold the button press count for each button on each gamepad
        int[] aButtonCount = new int[playerAmount];
        int[] bButtonCount = new int[playerAmount];
        int[] xButtonCount = new int[playerAmount];
        int[] yButtonCount = new int[playerAmount];
        //Holds the total amount of button presses per player
        int[] buttonCountTotal = new int[playerAmount];
        //Delcares arrays for the button counts to be displayed
        string[] aCountString = new string[playerAmount];
        string[] bCountString = new string[playerAmount];
        string[] xCountString = new string[playerAmount];
        string[] yCountString = new string[playerAmount];

        //Declares two 2D arrays of GamePadState class variables, one for holding the current state of each gamepad, 
        //and one for the old state of each gamepad
        GamePadState[] pad = new GamePadState[playerAmount];
        GamePadState[] oldPad = new GamePadState[playerAmount];

        //Delcares an array of Vector2 class variables for the location of each button count string
        Vector2[] aVector = new Vector2[playerAmount];
        Vector2[] bVector = new Vector2[playerAmount];
        Vector2[] xVector = new Vector2[playerAmount];
        Vector2[] yVector = new Vector2[playerAmount];

        //Declares an int class timer variable
        int timer;

        //Declares two boolean variables that determine the "old" start state of the game and whether or not the game has started currently
        bool startGame, oldStartGame;

        //Declares two string class variables that contain the intro and end messages
        string introString = "D-PAD LEFT FOR 4 SEC. GAME\n\nD-PAD RIGHT FOR 10 SEC. GAME";
        string endMessageString;

        //Declares three Vector2 class variables that determine the location of all accessory in-game text
        Vector2 introMessageVector, endMessageVector, timerVector;

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
            //Initializes timer
            timer = 0;

            //Initializes startGame
            startGame = false;
            //Initializes oldStartGame
            oldStartGame = startGame;

            //Sets the size of and populates each ButtonCount array
            for (int i = 0; i < aButtonCount.Length; ++i)
            {
                aButtonCount[i] = 0;
                bButtonCount[i] = 0;
                xButtonCount[i] = 0;
                yButtonCount[i] = 0;
            }

            //Populates the Vector2 arrays
            aVector[0] = new Vector2(150, 115);
            aVector[1] = new Vector2(650, 115);
            aVector[2] = new Vector2(150, 410);
            aVector[3] = new Vector2(650, 410);

            bVector[0] = new Vector2(200, 65);
            bVector[1] = new Vector2(700, 65);
            bVector[2] = new Vector2(200, 360);
            bVector[3] = new Vector2(700, 360);

            xVector[0] = new Vector2(100, 65);
            xVector[1] = new Vector2(600, 65);
            xVector[2] = new Vector2(100, 360);
            xVector[3] = new Vector2(600, 360);

            yVector[0] = new Vector2(150, 15);
            yVector[1] = new Vector2(650, 15);
            yVector[2] = new Vector2(150, 310);
            yVector[3] = new Vector2(650, 310);

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

            font = Content.Load<SpriteFont>("SpriteFont1");//Loads SpriteFont1 into font

            //Sets introMessageVector
            introMessageVector = new Vector2((GraphicsDevice.Viewport.Width / 2) - (font.MeasureString(introString).X / 2),
                (GraphicsDevice.Viewport.Height - font.MeasureString(introString).Y) - (font.MeasureString(introString).Y) + 10);
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

            //Sets timerVector and ensures that timerVector is always centered
            timerVector = new Vector2((GraphicsDevice.Viewport.Width / 2) - (font.MeasureString(timer.ToString()).X / 2),
                0);

            //Initializes each gamepad
            pad[0] = GamePad.GetState(PlayerIndex.One);
            pad[1] = GamePad.GetState(PlayerIndex.Two);
            pad[2] = GamePad.GetState(PlayerIndex.Three);
            pad[3] = GamePad.GetState(PlayerIndex.Four);

#if test//While test is active, each player is routed to player 1's gamepad
            pad[1] = pad[0];
            pad[2] = pad[0];
            pad[3] = pad[0];
#endif

            if (!startGame)//Runs while the game itself has NOT started
            {
                for (int i = 0; i < pad.Length; ++i)
                {
                    if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)//Runs when the left dpad is pressed
                    {
                        aButtonCount[i] = 0;//Resets the aButtonCount variable whenever start is pressed
                        bButtonCount[i] = 0;//Resets the bButtonCount variable whenever start is pressed
                        xButtonCount[i] = 0;//Resets the xButtonCount variable whenever start is pressed
                        yButtonCount[i] = 0;//Resets the yButtonCount variable whenever start is pressed

                        timer = 240;//Sets timer to approx. 4 sec.

                        startGame = true;//Sets startGame to true
                    }
                    if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)//Runs when the right dpad is pressed
                    {
                        aButtonCount[i] = 0;//Resets the aButtonCount variable whenever start is pressed
                        bButtonCount[i] = 0;//Resets the bButtonCount variable whenever start is pressed
                        xButtonCount[i] = 0;//Resets the xButtonCount variable whenever start is pressed
                        yButtonCount[i] = 0;//Resets the yButtonCount variable whenever start is pressed

                        timer = 600;//Sets timer to approx. 10 sec.

                        startGame = true;//Sets startGame to true
                    }
                }
            }

            for (int i = 0; i < pad.Length; ++i)
            {
                if (pad[i].Buttons.Start == ButtonState.Pressed)//Runs whenever start is pressed
                {
                    oldPad[i] = pad[i];//Initializes oldPad1 to pad1

                    timer = 0;//Initializes timer

                    oldStartGame = startGame;//Sets oldStartGame to startGame

                    startGame = false;//Updates the state of startGame
                }
            }

            if(startGame)//Runs while the game itself HAS started
            {
                for (int i = 0; i < pad.Length; ++i)
                {
                    if (pad[i].IsConnected)//Only runs when a gamepad is connected
                    {
                        if (oldPad[i].Buttons.A == ButtonState.Released && pad[i].Buttons.A == ButtonState.Pressed)
                        {
                            aButtonCount[i]++;//Increments buttonCount for A by one whenever A is pressed   
                        }
                        if (oldPad[i].Buttons.B == ButtonState.Released && pad[i].Buttons.B == ButtonState.Pressed)
                        {
                            bButtonCount[i]++;//Increments buttonCount for B by one whenever B is pressed
                        }
                        if (oldPad[i].Buttons.X == ButtonState.Released && pad[i].Buttons.X == ButtonState.Pressed)
                        {
                            xButtonCount[i]++;//Increments buttonCount for X by one whenever X is pressed
                        }
                        if (oldPad[i].Buttons.Y == ButtonState.Released && pad[i].Buttons.Y == ButtonState.Pressed)
                        {
                            yButtonCount[i]++;//Increments buttonCount for Y by one whenever Y is pressed
                        }
                    }

                    oldPad[i] = pad[i];//Sets oldPad to pad
                }

                //Decrements timer by one per tick
                timer--;

                if (timer == 0)//Runs when timer == 0
                {
                    oldStartGame = startGame;//Sets oldStartGame to startGame
                    
                    startGame = false;//Updates startGame

                    for (int i = 0; i < pad.Length; ++i)
                    {
                        buttonCountTotal[i] = aButtonCount[i] + bButtonCount[i] + xButtonCount[i] + yButtonCount[i];//Adds up the total amount of button
                    }                                                                                               //presses for each player
                }
            }

            //Determines the winner message based on each player's total amount of button presses
            if (buttonCountTotal[0] > buttonCountTotal[1] && buttonCountTotal[0] > buttonCountTotal[2] && buttonCountTotal[0] > buttonCountTotal[3])
            {
                endMessageString = "PLAYER 1 WINS! TRY AGAIN?";
            }
            else if (buttonCountTotal[1] > buttonCountTotal[0] && buttonCountTotal[1] > buttonCountTotal[2] && buttonCountTotal[1] > buttonCountTotal[3])
            {
                endMessageString = "PLAYER 2 WINS! TRY AGAIN?";
            }
            else if (buttonCountTotal[2] > buttonCountTotal[0] && buttonCountTotal[2] > buttonCountTotal[1] && buttonCountTotal[2] > buttonCountTotal[3])
            {
                endMessageString = "PLAYER 3 WINS! TRY AGAIN?";
            }
            else if (buttonCountTotal[3] > buttonCountTotal[0] && buttonCountTotal[3] > buttonCountTotal[1] && buttonCountTotal[3] > buttonCountTotal[2])
            {
                endMessageString = "PLAYER 4 WINS! TRY AGAIN?";
            }
            else
            {
                endMessageString = "TIE GAME! TRY AGAIN?";
            }

            //Sets endMessageVector
            endMessageVector = new Vector2((GraphicsDevice.Viewport.Width / 2) - (font.MeasureString(endMessageString).X / 2),
                (GraphicsDevice.Viewport.Height / 2) - (font.MeasureString(endMessageString).Y / 2));

            for (int i = 0; i < aButtonCount.Length; ++i)//Assigns each button count in string form to each string 
            {
                aCountString[i] = aButtonCount[i].ToString();
                bCountString[i] = bButtonCount[i].ToString();
                xCountString[i] = xButtonCount[i].ToString();
                yCountString[i] = yButtonCount[i].ToString();

            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkOrange);//Clears the screen to Dark Orange

            //Initiates, coordinates, and ends SpriteBatch
            spriteBatch.Begin();

            spriteBatch.DrawString(font, timer.ToString(), timerVector, Color.Blue);//Draws the timer

            if (!startGame && !oldStartGame)//If the game has been started for the "first" time, the following runs
                spriteBatch.DrawString(font, introString, introMessageVector, Color.Red);//Draws the intro message

            if (!startGame && oldStartGame)//If the game has NOT been started for the "first" time, the following runs
            {                              
                for (int i = 0; i < pad.Length; ++i)//This allows players to see their button counts even after a round has ended
                {
                    if (pad[i].IsConnected)//Draws the button counts for each connected controller
                    {
                        spriteBatch.DrawString(font, aCountString[i], aVector[i], Color.Green);
                        spriteBatch.DrawString(font, bCountString[i], bVector[i], Color.Red);
                        spriteBatch.DrawString(font, xCountString[i], xVector[i], Color.Blue);
                        spriteBatch.DrawString(font, yCountString[i], yVector[i], Color.Yellow);
                    }
                }

                spriteBatch.DrawString(font, endMessageString, endMessageVector, Color.Gold);//Draws the end message

                spriteBatch.DrawString(font, introString, introMessageVector, Color.Red);//Draws the intro message
            }

            if (startGame)//Runs while the game has started
            {
                for (int i = 0; i < pad.Length; ++i)
                {
                    if (pad[i].IsConnected)//Draws the button counts for each connected controller
                    {
                        spriteBatch.DrawString(font, aCountString[i], aVector[i], Color.Green);
                        spriteBatch.DrawString(font, bCountString[i], bVector[i], Color.Red);
                        spriteBatch.DrawString(font, xCountString[i], xVector[i], Color.Blue);
                        spriteBatch.DrawString(font, yCountString[i], yVector[i], Color.Yellow);
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
