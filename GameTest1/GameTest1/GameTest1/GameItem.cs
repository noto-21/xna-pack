using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameTest1
{
    class GameItem
    {
        //All variables that will be used in this class
        protected Rectangle rectangle;
        protected Texture2D texture2D;
        protected Color clr;
        //Gets and sets all above variables
        public Texture2D getTexture2D()
        {
            return texture2D;
        }
        public void setSprite(Texture2D aTexture2D)
        {
            texture2D = aTexture2D;
        }
        public Rectangle getRectangle()
        {
            return rectangle;
        }
        public void setRectangle(Rectangle aRectangle)
        {
            rectangle = aRectangle;
        }
        public Color getClr()
        {
            return clr;
        }
        public void setClr(Color aClr)
        {
            clr = aClr;
        }
        //Sets up the constructor and determines the arguments it takes
        public GameItem(Rectangle Rectangle, Texture2D Texture, Color Colour)
        {
            rectangle = Rectangle;
            texture2D = Texture;
            clr = Colour;
        }
        //Sets up the game item to draw itself using Game1's spriteBatch
        public void DrawSprite(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.texture2D, this.rectangle, this.clr);
        }
        //Allows the player to move the game item using a keyboard or gamepad
        public void MoveSprite(KeyboardState keyboard1, GamePadState pad1)
        {
            if (keyboard1.IsKeyDown(Keys.W) || pad1.ThumbSticks.Left.Y > 0)
            {
                this.rectangle.Y -= 7;
            }
            if (keyboard1.IsKeyDown(Keys.S) || pad1.ThumbSticks.Left.Y < 0)
            {
                this.rectangle.Y += 7;
            }
            if (keyboard1.IsKeyDown(Keys.A) || pad1.ThumbSticks.Left.X < 0)
            {
                this.rectangle.X -= 7;
            }
            if (keyboard1.IsKeyDown(Keys.D) || pad1.ThumbSticks.Left.X > 0)
            {
                this.rectangle.X += 7;
            }
        }
    }
}
