using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    class Yeti
    {
        public double moveStart = 0;
        double moveDelay = 500;
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        public int health;
        int randomNumber;

        bool canAttack = true;
        double timeSinceLastAttacked;

        protected Game1 game;
        public Vector2 position;
        Texture2D yetisprite;
        Texture2D rocktexture;
        public Rectangle hitbox;
        public Rectangle rockRect;
        Rectangle visual;
        int scale;

        public Yeti(Game1 game, Texture2D yetisprite, Texture2D rocktexture, Vector2 position, int scale, int health)
        {
            this.game = game;
            this.yetisprite = yetisprite;
            this.rocktexture = rocktexture;
            this.position = position;
            this.scale = scale;
            this.health = health;
        }

        public void EnemyAction(GameTime gameTime)
        {
            if (canDoAction == true)
            {
                timeSinceLastAction = gameTime.TotalGameTime.TotalMilliseconds;
                canDoAction = false;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAction + 850)
            {
                randomNumber = new Random().Next(1, 7);
                canDoAction = true;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay)
            {
                if (randomNumber == 1 && position.Y > 165 * scale || randomNumber == 4 && position.Y > 165 * scale)
                {
                    position.Y -= 3 * scale;
                }
                else if (randomNumber == 1 && position.Y <= 165 * scale || randomNumber == 4 && position.Y <= 165 * scale)
                {
                    position.Y += 3 * scale;
                    randomNumber = new Random().Next(1, 6);
                }
                if (randomNumber == 2 && position.Y < 350 * scale || randomNumber == 3 && position.Y < 350 * scale)
                {
                    position.Y += 3 * scale;
                }
                else if (randomNumber == 2 && position.Y >= 350 * scale || randomNumber == 3 && position.Y >= 350 * scale)
                {
                    position.Y -= 3 * scale;
                    randomNumber = new Random().Next(1, 6);
                }

                if (randomNumber == 6 && canAttack)
                {
                    Attack(gameTime);
                }
            }
        }

        public void Attack(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            visual = new Rectangle((int)position.X, (int)position.Y, yetisprite.Width * 2 * scale, yetisprite.Height * 2 * scale);
            hitbox = new Rectangle((int)position.X, (int)position.Y, yetisprite.Width * 2 * scale, yetisprite.Height * 2 * scale);
            spriteBatch.Draw(yetisprite, visual, Color.White);
        }
    }
}
