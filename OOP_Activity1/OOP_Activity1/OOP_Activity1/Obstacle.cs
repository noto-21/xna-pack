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

namespace OOP_Activity1
{
    class Obstacle : Parent
    {
        //Sets all variables to be used in the class
        private int blocks;
        //The "getters and setters"
        public int getBlocks()
        {
            return blocks;
        }
        public void setBlocks(int aBlocks)
        {
            blocks = aBlocks;
        }
        public Obstacle(Texture2D Sprite, int Damage, int Blocks) : base(Sprite, Damage)//Sets the parameters required to use Obstacle
        {
            blocks = Blocks;
        }
        public void DoDamage(Character other)//Does damage to Character class variables
        {
            if (other.getDamage() > 0)
                other.setDamage(other.getDamage() - 1);
            if (blocks > 0)
                blocks--;
        }
    }
}
