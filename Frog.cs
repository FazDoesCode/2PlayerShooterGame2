using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    class Frog
    {
        public double moveStart = 0;
        double moveDelay = 500;
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        public int health;

        protected Game1 game;
        public Vector2 position;
        Texture2D frogSprite;
        Texture2D frogAttackSprite;
        Texture2D tongueSprite;
        public Rectangle hitbox;
        public Rectangle toungeRect;
        Rectangle visualRect;
        int scale;

        // TEMPORARY, DELETE THIS
        Texture2D whitebox;

        public Frog(Game1 game, Texture2D frogSprite, Texture2D frogAttackSprite, Texture2D tongueSprite, Vector2 position, int scale, int health, Texture2D whitebox)
        {
            this.game = game;
            this.frogSprite = frogSprite;
            this.frogAttackSprite = frogAttackSprite;
            this.tongueSprite = tongueSprite;
            this.position = position;
            this.scale = scale;
            this.health = health;
            this.whitebox = whitebox;
        }

        public void EnemyAction(GameTime gameTime)
        {

        }

        public void Attack(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            visualRect = new Rectangle((int)position.X, (int)position.Y, frogSprite.Width * scale, frogSprite.Height * scale);
            hitbox = new Rectangle((int)position.X + 8 * scale, (int)position.Y + 15 * scale, (frogSprite.Width - 15) * scale, (frogSprite.Height - 15) * scale);
            spriteBatch.Draw(frogSprite, visualRect, Color.White);
            //spriteBatch.Draw(whitebox, hitbox, Color.Red);
        }
    }
}
