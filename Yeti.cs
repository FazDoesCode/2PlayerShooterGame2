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
        bool isAttacking = false;
        double timeSinceLastAttacked;
        new Point redGuyPos;
        new Point blueGuyPos;
        float distanceToTravelX;
        float distanceToTravelY;
        float distanceToTravelTotal;
        float movementX;
        float movementY;

        protected Game1 game;
        public Vector2 position;
        Texture2D yetisprite;
        Texture2D rocktexture;
        public Rectangle hitbox;
        public Rectangle rockRect;
        Vector2 rockPos;
        Rectangle visual;
        int scale;

        public Yeti(Game1 game, GameTime gameTime, Texture2D yetisprite, Texture2D rocktexture, Vector2 position, int scale, int health)
        {
            this.game = game;
            this.yetisprite = yetisprite;
            this.rocktexture = rocktexture;
            this.position = position;
            this.scale = scale;
            this.health = health;
            timeSinceLastAttacked = gameTime.TotalGameTime.TotalMilliseconds;
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

                if (randomNumber == 6 && canAttack || gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAttacked + 5000)
                {
                    Attack(gameTime);
                }
                if (isAttacking)
                {
                    rockPos.X += movementX * (10 * scale);
                    rockPos.Y += movementY * (10 * scale);
                    if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAttacked + 2000)
                    {
                        isAttacking = false;
                        canAttack = true;
                    }
                }
            }
        }

        public void Attack(GameTime gameTime)
        {
            canAttack = false;
            isAttacking = true;
            if (game.inSingleplayer)
            {
                if (game.redguyHealth > 0)
                {
                    redGuyPos.X = (int)game.redguyPos.X;
                    redGuyPos.Y = (int)game.redguyPos.Y;

                    distanceToTravelX = redGuyPos.X - rockPos.X;
                    distanceToTravelY = redGuyPos.Y - rockPos.Y;
                    distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                    movementX = distanceToTravelX / distanceToTravelTotal;
                    movementY = distanceToTravelY / distanceToTravelTotal;
                }
            }
            if (game.inCoop)
            {
                int randomNumber = new Random().Next(1, 101);
                if (game.redguyHealth > 0 && randomNumber < 51)
                {
                    redGuyPos.X = (int)game.redguyPos.Y;
                    redGuyPos.X = (int)game.redguyPos.Y;

                    distanceToTravelX = redGuyPos.X - redGuyPos.X;
                    distanceToTravelY = redGuyPos.Y - redGuyPos.Y;
                    distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                    movementX = distanceToTravelX / distanceToTravelTotal;
                    movementY = distanceToTravelY / distanceToTravelTotal;
                }
                if (game.blueguyHealth > 0 && randomNumber > 50)
                {
                    blueGuyPos.X = (int)game.blueguyPos.X;
                    blueGuyPos.Y = (int)game.blueguyPos.Y;

                    distanceToTravelX = blueGuyPos.X - blueGuyPos.X;
                    distanceToTravelY = blueGuyPos.Y - blueGuyPos.Y;
                    distanceToTravelTotal = (float)Math.Sqrt((distanceToTravelX * distanceToTravelX) + (distanceToTravelY * distanceToTravelY));
                    movementX = distanceToTravelX / distanceToTravelTotal;
                    movementY = distanceToTravelY / distanceToTravelTotal;
                }
            }
            timeSinceLastAttacked = gameTime.TotalGameTime.TotalMilliseconds;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            visual = new Rectangle((int)position.X, (int)position.Y, yetisprite.Width * 2 * scale, yetisprite.Height * 2 * scale);
            hitbox = new Rectangle((int)position.X + 15 * scale, (int)position.Y, yetisprite.Width * 3 / 2 * scale, yetisprite.Height * 2 * scale);
            rockRect = new Rectangle((int)rockPos.X, (int)rockPos.Y, rocktexture.Width * 2 * scale, rocktexture.Height * 2 * scale);
            spriteBatch.Draw(yetisprite, visual, Color.White);
            if (!isAttacking && canAttack)
            {
                rockPos.X = position.X + 5 * scale;
                rockPos.Y = position.Y - 60 * scale;
            }
            spriteBatch.Draw(rocktexture, rockRect, Color.White);
        }
    }
}
