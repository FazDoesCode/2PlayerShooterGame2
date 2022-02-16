using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    class Tree
    {
        public Vector2 position;
        Texture2D texture;

        Rectangle rectangle;
        int scale;

        public Tree(Texture2D texture, Vector2 position, int scale)
        {
            this.position = position;
            this.texture = texture;
            this.scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width * scale, texture.Height * scale);
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
    }
}
