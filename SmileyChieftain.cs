using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TheGame
{
    class SmileyChieftain
    {
        public double moveStart = 0;
        double moveDelay = 500;
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        public int health;
        int randomNumberToSix;

        public Vector2 position;
        Texture2D texture;
        public Rectangle rectangle;
        int scale;
        Rectangle visualRectangle;

        public bool isHyping;
        bool canHype = true;
        double timeSinceLastHyped = 0;

        public SmileyChieftain(Texture2D texture, Vector2 position, int scale, int health)
        {
            this.texture = texture;
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
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAction + 750)
            {
                randomNumberToSix = new Random().Next(1, 7);
                canDoAction = true;
            }   
            if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay) {
                if (!isHyping)
                {
                    if (randomNumberToSix == 1 && position.Y > 190 * scale || randomNumberToSix == 4 && position.Y > 190 * scale)
                    {
                        position.Y -= 3 * scale;
                    }
                    else if (randomNumberToSix == 1 && position.Y <= 190 * scale || randomNumberToSix == 4 && position.Y <= 190 * scale)
                    {
                        position.Y += 3 * scale;
                        randomNumberToSix = 2;
                    }
                    if (randomNumberToSix == 2 && position.Y < 370 * scale || randomNumberToSix == 6 && position.Y < 370 * scale)
                    {
                        position.Y += 3 * scale;
                    }
                    else if (randomNumberToSix == 2 && position.Y >= 370 * scale || randomNumberToSix == 6 && position.Y >= 370 * scale)
                    {
                        position.Y -= 3 * scale;
                        randomNumberToSix = 1;
                    }
                }
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastHyped + 10000)
                {
                    canHype = true;
                }
                if (randomNumberToSix == 3 && canHype)
                {
                    Hype(gameTime);
                    canDoAction = true;
                }
                else if (randomNumberToSix == 3 && !canHype)
                {
                    randomNumberToSix = new Random().Next(1, 7);
                }
                else if (randomNumberToSix == 5)
                {
                    timeSinceLastAction = timeSinceLastAction - 250;
                }
                if (isHyping)
                {
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastHyped + 5000)
                    {
                        isHyping = false;
                    }
                }
            }
        }

        public void Hype(GameTime gameTime)
        {
            canHype = false;
            isHyping = true;
            timeSinceLastHyped = gameTime.TotalGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y + 10 * scale, (int)texture.Width * 4 * scale, (int)texture.Height * 3 * scale);
            visualRectangle = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width * 4 * scale, (int)texture.Height * 4 * scale);
            spriteBatch.Draw(texture, visualRectangle, Color.White);
            //spriteBatch.Draw(texture, rectangle, Color.Green);
        }
    }
}
