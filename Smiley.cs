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
        double moveDelay = 250;
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        int randomNumberToSix;

        public int health;

        bool canCharge = true;
        bool isCharging = false;

        public Vector2 position;
        Vector2 originalPos;
        Texture2D smileySprite;

        public Rectangle smileyRect;
        int scale;

        public Smiley(Texture2D smileySprite, Vector2 position, Vector2 originalPos, int scale, int health)
        {
            this.position = position;
            this.originalPos = originalPos;
            this.smileySprite = smileySprite;
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
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAction + 750)
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
                    if (randomNumberToSix == 2 && position.Y < 370 * scale || randomNumberToSix == 6 && position.Y < 370 * scale)
                    {
                        MoveDown();
                    }
                    else if (randomNumberToSix == 2 && position.Y >= 370 * scale || randomNumberToSix == 6 && position.Y >= 370 * scale)
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
                            position.X -= 8 * scale;
                        }
                        if (position.X < 5 * scale)
                        {
                            canCharge = false;
                        }
                    } else
                    {
                        if (position.X <= originalPos.X)
                        {
                            position.X += 6 * scale;
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
            position.Y -= 4 * scale;
        }

        void MoveDown()
        {
            position.Y += 4 * scale;
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
