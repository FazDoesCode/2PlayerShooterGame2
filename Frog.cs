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
        int randomNumber;

        bool scaled = false;
        Vector2 point1 = new Vector2(475, 230);
        Vector2 point2 = new Vector2(590, 290);
        Vector2 point3 = new Vector2(465, 347);
        Vector2 point4 = new Vector2(630, 380);
        int atPoint;
        int speed = 5;
        float distanceToTravelX;
        float distanceToTravelY;
        float distanceToTravelTotal;
        float movementX;
        float movementY;
        bool canMove = true;
        bool isAttacking = false;
        bool canAttack = true;
        double timeSinceLastAttacked;
        int tongueLength;
        int tonguePos;
        double timeSinceLastIncrement;

        protected Game1 game;
        public Vector2 position;
        Texture2D frogSprite;
        Texture2D frogAttackSprite;
        Texture2D tongueSprite;
        public Rectangle hitbox;
        public Rectangle tongueRect;
        Rectangle visualRect;
        int scale;

        public Frog(Game1 game, Texture2D frogSprite, Texture2D frogAttackSprite, Texture2D tongueSprite, Vector2 position, int scale, int health)
        {
            this.game = game;
            this.frogSprite = frogSprite;
            this.frogAttackSprite = frogAttackSprite;
            this.tongueSprite = tongueSprite;
            this.position = position;
            this.scale = scale;
            this.health = health;
        }

        public void EnemyAction(GameTime gameTime)
        {
            if (!scaled)
            {
                point1 = new Vector2(475 * scale, 230 * scale);
                point2 = new Vector2(590 * scale, 290 * scale);
                point3 = new Vector2(465 * scale, 347 * scale);
                point4 = new Vector2(630 * scale, 380 * scale);
                scaled = true;
            }
            if (canDoAction == true)
            {
                timeSinceLastAction = gameTime.TotalGameTime.TotalMilliseconds;
                canDoAction = false;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAction + 850)
            {
                randomNumber = new Random().Next(1, 6);
                canMove = true;
                canDoAction = true;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAttacked + 1500)
                {
                    isAttacking = false;
                    canAttack = true;
                }
                if (!isAttacking)
                {
                    if (randomNumber == 1 && canMove)
                    {
                        if (atPoint == 1)
                        {
                            randomNumber = new Random().Next(1, 6);
                        }
                        else
                        {
                            distanceToTravelX = point1.X - position.X;
                            distanceToTravelY = point1.Y - position.Y;
                            distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                            movementX = distanceToTravelX / distanceToTravelTotal;
                            movementY = distanceToTravelY / distanceToTravelTotal;
                            position.X += movementX * (speed * scale);
                            position.Y += movementY * (speed * scale);
                            if (distanceToTravelTotal < 3 * scale)
                            {
                                canMove = false;
                                atPoint = 1;
                            }
                        }
                    }
                    if (randomNumber == 2 && canMove)
                    {
                        if (atPoint == 2)
                        {
                            randomNumber = new Random().Next(1, 6);
                        }
                        else
                        {
                            distanceToTravelX = point2.X - position.X;
                            distanceToTravelY = point2.Y - position.Y;
                            distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                            movementX = distanceToTravelX / distanceToTravelTotal;
                            movementY = distanceToTravelY / distanceToTravelTotal;
                            position.X += movementX * (speed * scale);
                            position.Y += movementY * (speed * scale);
                            if (distanceToTravelTotal < 3 * scale)
                            {
                                canMove = false;
                                atPoint = 2;
                            }
                        }
                    }
                    if (randomNumber == 3 && canMove)
                    {
                        if (atPoint == 3)
                        {
                            randomNumber = new Random().Next(1, 6);
                        }
                        else
                        {
                            distanceToTravelX = point3.X - position.X;
                            distanceToTravelY = point3.Y - position.Y;
                            distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                            movementX = distanceToTravelX / distanceToTravelTotal;
                            movementY = distanceToTravelY / distanceToTravelTotal;
                            position.X += movementX * (speed * scale);
                            position.Y += movementY * (speed * scale);
                            if (distanceToTravelTotal < 3 * scale)
                            {
                                canMove = false;
                                atPoint = 3;
                            }
                        }
                    }
                    if (randomNumber == 4 && canMove)
                    {
                        if (atPoint == 4)
                        {
                            randomNumber = new Random().Next(1, 6);
                        }
                        else
                        {
                            distanceToTravelX = point4.X - position.X;
                            distanceToTravelY = point4.Y - position.Y;
                            distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                            movementX = distanceToTravelX / distanceToTravelTotal;
                            movementY = distanceToTravelY / distanceToTravelTotal;
                            position.X += movementX * (speed * scale);
                            position.Y += movementY * (speed * scale);
                            if (distanceToTravelTotal < 3 * scale)
                            {
                                canMove = false;
                                atPoint = 4;
                            }
                        }
                    }
                }
                if (randomNumber == 5 && canAttack)
                {
                    Attack(gameTime);
                }
            }
        }

        public void Attack(GameTime gameTime)
        {
            isAttacking = true;
            canAttack = false;
            timeSinceLastAttacked = gameTime.TotalGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            visualRect = new Rectangle((int)position.X, (int)position.Y, frogSprite.Width * scale, frogSprite.Height * scale);
            hitbox = new Rectangle((int)position.X + 8 * scale, (int)position.Y + 15 * scale, (frogSprite.Width - 15) * scale, (frogSprite.Height - 15) * scale);
            if (!isAttacking)
            {
                tonguePos = 0;
                tongueLength = 0;
                tongueRect = new Rectangle((int)position.X, (int)position.Y, 0, 0);
                spriteBatch.Draw(frogSprite, visualRect, Color.White);
            }
            else
            {
                while (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastIncrement + 1)
                {
                    tonguePos += 15;
                    tongueLength += 15;
                    tongueRect = new Rectangle(((int)position.X + 22) - tonguePos, (int)position.Y + 19, tongueLength * scale, 8 * scale);
                    timeSinceLastIncrement = gameTime.TotalGameTime.TotalMilliseconds;
                }
                spriteBatch.Draw(frogAttackSprite, visualRect, Color.White);
                spriteBatch.Draw(tongueSprite, tongueRect, Color.White);
            }
        }
    }
}
