using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    public class Hat
    {
        string effect;
        public Vector2 position;
        Texture2D texture;
        public Rectangle hatRect;
        public int scale;
        public bool isEquipped = false;
        public bool isPurchased = false;

        public Hat(Texture2D texture, Vector2 position, string effect, int scale)
        {
            this.texture = texture;
            this.position = position;
            this.effect = effect;
            this.scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            hatRect = new Rectangle((int)position.X, (int)position.Y, texture.Width * scale, texture.Height * scale);
            spriteBatch.Draw(texture, hatRect, Color.White);
        }
    }
}
