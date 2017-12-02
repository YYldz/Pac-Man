using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan_byYakupY.Objects
{
    class Wall: GameObjects
    {
        private Rectangle srcRect;
        
        public Wall(Texture2D texture, Vector2 position, Point size)
            : base(texture, position, size)
        {
            srcRect = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, srcRect, Color.Green);
        }
    }
}
