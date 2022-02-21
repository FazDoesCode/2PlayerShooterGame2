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
        bool canDoAction = true;
        double timeSinceLastAction = 0;
        int randomNumber;
        int health;
        int scale;
        Texture2D smileyBossSprite;
        Texture2D smileyWallSprite;
        Vector2 position;
        Rectangle hitBox;
        Rectangle wallRect;

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
            if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay)
            {

            }
        }

        public void Draw(GameTime gameTime)
        {

        }
    }
}
