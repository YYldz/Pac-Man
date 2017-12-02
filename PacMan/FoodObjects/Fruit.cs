using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan_byYakupY.FoodObjects
{
    class Fruit: GameObjects
    {
        private bool spawnOK;
        public Fruit(Texture2D texture, Vector2 position, Point size, bool beenEaten)
            : base(texture, position, size)
        {

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Manager.playerScore == 1000)
            {
                spawnOK = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
            if (!beenEaten && spawnOK)
            {
                spriteBatch.Draw(texture, boundingBox, Color.White);
            }
        }
    }
}
