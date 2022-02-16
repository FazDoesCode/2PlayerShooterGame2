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
        private float speed;
        int scale;
        public Snowflake(Texture2D texture, Vector2 position, int scale)
        {
            this.position = position;
            this.texture = texture;
            speed = new Random().Next(2, 7);
            this.scale = scale;
        }
        public void Snowfall(GameTime gameTime)
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width * scale, texture.Height * scale);
            Movement(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.White);
        }
        private void Movement(GameTime gameTime)
        {
            position.X -= speed * scale;
            position.Y += speed * scale;
        }
    }
}
