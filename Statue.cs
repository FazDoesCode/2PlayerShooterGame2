using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    class Statue
    {
        public double moveStart = 0;
        double moveDelay = 500;
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        public int health;
        int randomNumberToSix;

        public Vector2 position;
        Texture2D texture;
        public Rectangle rectangle;
        int scale;

        public Statue(Texture2D texture, Vector2 position, int scale, int health)
        {
            this.texture = texture;
            this.position = position;
            this.scale = scale;
            this.health = health;
        }

        public void EnemyAction()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

    }
}
