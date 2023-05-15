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
    class Parent//Sets common traits that all of the objects share
    {
        //Sets all variables to be used in the class
        private Texture2D sprite;
        protected int damage;
        public Parent(Texture2D Sprite, int Damage)//Sets the parameters required to use Parent
        {
            sprite = Sprite;
            damage = Damage;
        }
        //The "getters and setters"
        public Texture2D getSprite()
        {
            return sprite;
        }
        public void setSprite(Texture2D aSprite)
        {
            sprite = aSprite;
        }
        public int getDamage()
        {
            return damage;
        }
        public void setDamage(int aDamage)
        {
            damage = aDamage;
        }
    }
}
