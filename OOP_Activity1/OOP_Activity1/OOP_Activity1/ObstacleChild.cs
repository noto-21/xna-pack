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
    class ObstacleChild : Obstacle
    {
        //Sets all variables to be used in the class
        private int explosionChance;
        Texture2D explosion;
        //The "getters and setters"
        public int getExplosionChance()
        {
            return explosionChance;
        }
        public void setExplosionChance(int aExplosionChance)
        {
            explosionChance = aExplosionChance;
        }
        public ObstacleChild(Texture2D Sprite, int Damage, int Blocks, int ExplosionChance) : base(Sprite, Damage, Blocks)//Sets the parameters required to use ObstacleChild
        {
            explosionChance = ExplosionChance;
        }
        public void Explode(Obstacle explodingObstacle)//Handles the "explosion" of the obstacle
        {
            if (explodingObstacle.getDamage() <= 0)
            {
                explodingObstacle.setSprite(explosion);
            }
        }
    }
}
