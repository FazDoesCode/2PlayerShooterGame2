using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    class Statue
    {
        public double moveStart = 0;
        double moveDelay = 500;
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        public int health;
        int randomNumberToSix;

        bool canAttack = true;
        public bool isAttacking = false;
        public double attackTimerStart;
        public double chargeUpTime = 750;
        public Rectangle chargeupRect;
        public Rectangle beamRect;
        Texture2D chargeUpSprite;
        Texture2D beamSprite;
        public bool canDoDamage = false;
        protected Game1 game;
        int speed;

        public Vector2 position;
        Texture2D texture;
        public Rectangle rectangle;
        Rectangle visualRect;
        int scale;

        public Statue(Game1 game, Texture2D texture, Texture2D chargeUpSprite, Texture2D beamSprite, Vector2 position, int scale, int health)
        {
            this.game = game;
            this.texture = texture;
            this.chargeUpSprite = chargeUpSprite;
            this.beamSprite = beamSprite;
            this.position = position;
            this.scale = scale;
            this.health = health;
        }

        public void EnemyAction(GameTime gameTime)
        {
            if (game.inCoop)
            {
                if (isAttacking && gameTime.TotalGameTime.TotalMilliseconds > attackTimerStart + 1800)
                {
                    isAttacking = false;
                    canAttack = true;
                    randomNumberToSix = new Random().Next(1, 6);
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
                        randomNumberToSix = new Random().Next(1, 7);
                        canDoAction = true;
                    }
                }
                if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay)
                {
                    if (!isAttacking)
                    {
                        if (randomNumberToSix == 1 && position.Y > 140 * scale || randomNumberToSix == 4 && position.Y > 140 * scale)
                        {
                            position.Y -= 3 * scale;
                        }
                        else if (randomNumberToSix == 1 && position.Y <= 140 * scale || randomNumberToSix == 4 && position.Y <= 140 * scale)
                        {
                            position.Y += 3 * scale;
                            randomNumberToSix = new Random().Next(1, 6);
                        }
                        if (randomNumberToSix == 2 && position.Y < 300 * scale || randomNumberToSix == 3 && position.Y < 300 * scale)
                        {
                            position.Y += 3 * scale;
                        }
                        else if (randomNumberToSix == 2 && position.Y >= 300 * scale || randomNumberToSix == 3 && position.Y >= 300 * scale)
                        {
                            position.Y -= 3 * scale;
                            randomNumberToSix = new Random().Next(1, 6);
                        }
                    }
                    if (randomNumberToSix == 6 && canAttack)
                    {
                        Attack(gameTime);
                    }
                }
            }
            else if (game.inSingleplayer)
            {
                if (isAttacking)
                {
                    speed = 1;
                } else
                {
                    speed = 2;
                }
                if (isAttacking && gameTime.TotalGameTime.TotalMilliseconds > attackTimerStart + 1300)
                {
                    isAttacking = false;
                    canAttack = true;
                }
                if (canDoAction == true)
                {
                    timeSinceLastAction = gameTime.TotalGameTime.TotalMilliseconds;
                    canDoAction = false;
                }
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastAction + 850)
                {
                    if (game.redguyPos.Y + 10 * scale > position.Y + 65 * scale)
                    {
                        position.Y += speed * scale;
                    }
                    else if (game.redguyPos.Y + 10 * scale < position.Y + 65 * scale)
                    {
                        position.Y -= speed * scale;
                    }
                }
            }
        }

        public void Attack(GameTime gameTime)
        {
            canDoDamage = false;
            isAttacking = true;
            attackTimerStart = gameTime.TotalGameTime.TotalMilliseconds;
            canAttack = false;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            visualRect = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width * 2 * scale, (int)texture.Height * 2 * scale);
            rectangle = new Rectangle((int)position.X, (int)position.Y + 5 * scale, (int)texture.Width * 2 * scale, (int)texture.Height * 2 * scale - 10 * scale);
            spriteBatch.Draw(texture, visualRect, Color.White);

            if (isAttacking)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds < attackTimerStart + chargeUpTime)
                {
                    chargeupRect = new Rectangle((int)position.X - 70 * scale, (int)position.Y + 55 * scale, (int)chargeUpSprite.Width * 3 * scale, (int)chargeUpSprite.Height * 3 * scale);
                    spriteBatch.Draw(chargeUpSprite, chargeupRect, Color.White);
                } else if (gameTime.TotalGameTime.TotalMilliseconds > attackTimerStart + chargeUpTime)
                {
                    chargeupRect = new Rectangle((int)position.X - 70 * scale, (int)position.Y + 55 * scale, (int)chargeUpSprite.Width * 3 * scale, (int)chargeUpSprite.Height * 3 * scale);
                    beamRect = new Rectangle(0, (int)position.Y + 64 * scale, beamSprite.Width * 7 * scale, (beamSprite.Height - 4 * scale) * 3 * scale);
                    spriteBatch.Draw(beamSprite, beamRect, Color.White);
                    spriteBatch.Draw(chargeUpSprite, chargeupRect, Color.White);
                    canDoDamage = true;
                }
            }
        }

    }
}
