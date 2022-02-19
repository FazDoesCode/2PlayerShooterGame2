using Microsoft.Xna.Framework;
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
        List<Smiley> bodyParts = new List<Smiley>();

        void EnemyAction(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds > moveStart + moveDelay)
            {

            }
        }
    }
}
