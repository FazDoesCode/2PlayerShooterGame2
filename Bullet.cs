using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    public class Bullet
    {
        private Vector2 position;
        private Texture2D texture2D;
        Vector2 direction;
        int scale;

        public Rectangle bulletRect;

        public Bullet(Texture2D texture2D, Vector2 position, Vector2 direction, int scale)
        {
            this.position = position;
            this.texture2D = texture2D;
            this.direction = direction;
            this.scale = scale;
        }

        public void MoveBullet()
        {
            position += direction;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            bulletRect = new Rectangle((int)position.X, (int)position.Y, (int)(texture2D.Width * 0.25f) * scale, (int)(texture2D.Height * 0.25f) * scale);
            spriteBatch.Draw(texture2D, bulletRect, Color.White);
        }
    }
}
