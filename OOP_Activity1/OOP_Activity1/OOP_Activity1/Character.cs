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
    class Character : Parent//Sets traits to be used by character objects, shared with Parent
    {
        //Sets all variables to be used in the class
        private int numLives;
        public Character(Texture2D Sprite, int Damage, int Lives) : base(Sprite, Damage)//Sets the parameters required to use Character
        {
            numLives = Lives;
        }
        //The "getters and setters"
        public int getNumLives()
        {
            return numLives;
        }
        public void setNumLives(int aNumLives)
        {
            numLives = aNumLives;
        }
        public void DoDamage(Obstacle other)//Does damage to Obstacle class variables
        {
            if (other.getDamage() > 0)
                other.setDamage(other.getDamage() - 1);
            if (numLives > 0)
                numLives--;
        }
        public void BonusLives()//Gives bonus lives to character if damage > 50
        {
            if (damage > 50)
                numLives++;
        }
    }
}
