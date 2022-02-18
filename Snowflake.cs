using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    public class Snowflake
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle hitbox;
        public float speedX;
        public float speedY;
        public Color color;
        int scale;
        public Snowflake(Texture2D texture, Vector2 position, float speedX, float speedY, int scale, Color color)
        {
            this.position = position;
            this.texture = texture;
            this.speedX = speedX;
            this.speedY = speedY;
            this.scale = scale;
            this.color = color;
        }
        public void Snowfall(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width * scale, texture.Height * scale);
            Movement(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, color);
        }

        private void Movement(GameTime gameTime)
        {
            position.X -= speedX * scale;
            position.Y += speedY * scale;
        }
    }
}
