using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    class Smiley
    {
        public double moveStart = 0;
        double moveDelay = 500;
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        int randomNumberToSix;

        public int health;
        public int speed;

        bool canCharge = true;
        bool isCharging = false;

        public Vector2 position;
        Vector2 originalPos;
        Texture2D smileySprite;

        public Rectangle smileyRect;
        int scale;

        public Smiley(Texture2D smileySprite, Vector2 position, Vector2 originalPos, int scale, int health, int speed)
        {
            this.position = position;
            this.originalPos = originalPos;
            this.smileySprite = smileySprite;
            this.scale = scale;
            this.health = health;
            this.speed = speed;
        }

        public void EnemyAction(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if (canDoAction == true)
            {
                timeSinceLastAction = gameTime.TotalGameTime.TotalMilliseconds;
                canDoAction = false;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAction + 500)
            {
                randomNumberToSix = new Random().Next(1, 7);
                canDoAction = true;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay)
            {
                if (!isCharging)
                {
                    if (randomNumberToSix == 1 && position.Y > 210 * scale || randomNumberToSix == 4 && position.Y > 180 * scale)
                    {
                        MoveUp();
                    }
                    else if (randomNumberToSix == 1 && position.Y <= 210 * scale || randomNumberToSix == 4 && position.Y <= 180 * scale)
                    {
                        MoveDown();
                        randomNumberToSix = 2;
                    }
                    if (randomNumberToSix == 2 && position.Y < graphics.PreferredBackBufferHeight - 5 * scale - smileySprite.Height * 3 * scale || randomNumberToSix == 6 && position.Y < graphics.PreferredBackBufferHeight - 5 * scale - smileySprite.Height * 3 * scale)
                    {
                        MoveDown();
                    }
                    else if (randomNumberToSix == 2 && position.Y > graphics.PreferredBackBufferHeight - 5 * scale - smileySprite.Height * 3 * scale || randomNumberToSix == 6 && position.Y > graphics.PreferredBackBufferHeight - 5 * scale - smileySprite.Height * 3 * scale)
                    {
                        MoveUp();
                        randomNumberToSix = 1;
                    }
                    else if (randomNumberToSix == 3)
                    {
                        Charge();
                    }
                }
                if (isCharging)
                {
                    if (canCharge)
                    {
                        if (position.X > 5 * scale)
                        {
                            position.X -= (speed * 2) * scale;
                        }
                        if (position.X < 5 * scale)
                        {
                            canCharge = false;
                        }
                    } else
                    {
                        if (position.X <= originalPos.X)
                        {
                            position.X += (speed * 2 - 2) * scale;
                        }
                        else
                        {
                            isCharging = false;
                            canCharge = true;
                        }
                    }
                }
            }
        }
        void MoveUp()
        {
            position.Y -= speed * scale;
        }

        void MoveDown()
        {
            position.Y += speed * scale;
        }

        public void Charge()
        {
            canCharge = true;
            isCharging = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            smileyRect = new Rectangle((int)position.X, (int)position.Y, (int)(smileySprite.Width * 3 * scale), (int)(smileySprite.Height * 3 * scale));
            spriteBatch.Draw(smileySprite, smileyRect, Color.White);

        }
    }
}
