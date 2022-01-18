using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    class Mountain
    {
        Vector2 position;
        Texture2D mountainSprite;

        public Rectangle mountainRect;
        int scale;

        public Mountain(Texture2D mountainSprite, Vector2 position, int scale)
        {
            this.position = position;
            this.mountainSprite = mountainSprite;
            this.scale = scale;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            mountainRect = new Rectangle((int)position.X + 10 * scale, (int)position.Y, (int)((mountainSprite.Width - 20) * scale), (int)(25 * scale));
            Rectangle mountainRectshow = new Rectangle((int)position.X, (int)position.Y, (int)(mountainSprite.Width * scale), (int)(mountainSprite.Height * scale));
            spritebatch.Draw(mountainSprite, mountainRectshow, Color.White);
        }
    }
}
