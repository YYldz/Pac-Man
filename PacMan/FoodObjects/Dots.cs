using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan_byYakupY.FoodObjects
{
    class Dots: GameObjects
    {
        
        public Dots(Texture2D texture, Vector2 position, Point size, bool beenEaten)
            : base(texture, position, size)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!beenEaten)
            {
                spriteBatch.Draw(texture, boundingBox, Color.White);
            }
        }
    }
}
