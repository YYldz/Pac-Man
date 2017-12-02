using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PacMan_byYakupY
{
    class GameObjects
    {
        protected Texture2D texture;
        public Vector2 position;
        protected Point size;
        public Rectangle boundingBox;
        protected Rectangle spriteRec;
        public Vector2 previousSpot;
        public bool beenEaten = false;
        public Rectangle ghostSprite;
        public Vector2 direction;


        public GameObjects(Texture2D texture, Vector2 position, Point size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
            spriteRec = new Rectangle(0, 0, 50, 86);
            ghostSprite = new Rectangle(0, 0, 21, 15);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, Color.White);
        }

        public Rectangle Bounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, size.X, size.Y);
        }

    }
}
