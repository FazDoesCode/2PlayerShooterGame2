using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    class SmileyChieftain
    {
        public double moveStart = 0;
        double moveDelay = 500;
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        public int health;

        public Vector2 position;
        Texture2D texture;
        public Rectangle rectangle;
        int scale;

        public SmileyChieftain(Texture2D texture, Vector2 position, int scale, int health)
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
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width * 4 * scale, (int)texture.Height * 4 * scale);
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
