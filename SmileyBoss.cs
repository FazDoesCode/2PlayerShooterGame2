using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheGame
{
    public class SmileyBoss
    {
        public double moveStart = 0;
        double moveDelay = 500;
        double timeSinceLastAction = 0;
        int randomNumber;
        int health;
        int scale;
        Texture2D smileyBossSprite;
        Texture2D smileyWallSprite;
        Vector2 position;
        Rectangle hitBox;
        Rectangle wallRect;

        bool canDoAction = true;
        bool moving;
        bool attacking;

        public SmileyBoss(Texture2D smileyBossSprite, Texture2D smileyWallSprite, Vector2 position, int scale, int health)
        {
            this.smileyBossSprite = smileyBossSprite;
            this.smileyWallSprite = smileyWallSprite;
            this.position = position;
            this.scale = scale;
            this.health = health;
        }
        
        public void EnemyAction(GameTime gameTime)
        {
            if (canDoAction)
            {

            }
            if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay)
            {
                
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            hitBox = new Rectangle((int)position.X, (int)position.Y, smileyBossSprite.Width * 7 * scale, smileyBossSprite.Height * 7 * scale);
            spriteBatch.Draw(smileyBossSprite, hitBox, Color.White);
        }
    }
}
