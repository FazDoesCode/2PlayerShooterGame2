using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TheGame
{
    public class SmileyBoss
    {
        public double moveStart = 0;
        double moveDelay = 500;
        double timeSinceLastAction = 0;
        int randomNumber;
        public int health;
        int scale;
        Texture2D smileyBossSprite;
        Texture2D smileyWallSprite;
        Vector2 position;
        public Rectangle hitBox;
        Rectangle wallRect;

        bool canDoAction = true;
        bool moving;
        bool isAttacking = false;
        bool canAttack = true;
        public double attackTimerStart;

        public SmileyBoss(Texture2D smileyBossSprite, Texture2D smileyWallSprite, Vector2 position, int scale, int health)
        {
            this.smileyBossSprite = smileyBossSprite;
            this.smileyWallSprite = smileyWallSprite;
            this.position = position;
            this.scale = scale;
            this.health = health;
        }
        
        public void EnemyAction(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if (isAttacking && gameTime.TotalGameTime.TotalMilliseconds > attackTimerStart + 1800)
            {
                isAttacking = false;
                canAttack = true;
                randomNumber = new Random().Next(1, 6);
            }
            if (!isAttacking)
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
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay)
            {
                if (!isAttacking)
                {
                    if (randomNumber == 1 && position.Y > 180 * scale || randomNumber == 4 && position.Y > 180 * scale)
                    {
                        position.Y -= 3 * scale;
                    }
                    else if (randomNumber == 1 && position.Y <= 180 * scale || randomNumber == 4 && position.Y <= 180 * scale)
                    {
                        position.Y += 3 * scale;
                        randomNumber = new Random().Next(1, 6);
                    }
                    if (randomNumber == 2 && position.Y < graphics.PreferredBackBufferHeight - 10 * scale - smileyBossSprite.Height * 7 * scale || randomNumber == 3 && position.Y < graphics.PreferredBackBufferHeight - 10 * scale - smileyBossSprite.Height * 7 * scale)
                    {
                        position.Y += 3 * scale;
                    }
                    else if (randomNumber == 2 && position.Y >= graphics.PreferredBackBufferHeight - 10 * scale - smileyBossSprite.Height * 7 * scale || randomNumber == 3 && position.Y > graphics.PreferredBackBufferHeight - 10 * scale - smileyBossSprite.Height * 7 * scale)
                    {
                        position.Y -= 3 * scale;
                        randomNumber = new Random().Next(1, 6);
                    }
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

        public void Draw(SpriteBatch spriteBatch)
        {
            hitBox = new Rectangle((int)position.X, (int)position.Y, smileyBossSprite.Width * 7 * scale, smileyBossSprite.Height * 7 * scale);
            spriteBatch.Draw(smileyBossSprite, hitBox, Color.White);
        }
    }
}
