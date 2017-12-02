using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan_byYakupY.Objects
{
    class Ghost : GameObjects
    {
        private double frameTimer = 100;
        private int frameinterval = 100;
        private int frame;

        int ghostSpeed = 2;

        public static Random rand = new Random();

        public Ghost(Texture2D texture, Vector2 position, Point size)
            : base(texture, position, size)
        {
            ghostSprite = new Rectangle(0, 0, 21, 15);
            direction.Y = 1;

        }
        #region GhostMechanics

        #endregion

        public override void Update(GameTime gameTime)
        {
            previousSpot = position;
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameinterval;
                frame++;
                ghostSprite.X = (frame % 2) * 21;
            }
            position.X += ghostSpeed * direction.X;
            position.Y += ghostSpeed * direction.Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Bounds(),
                ghostSprite, Color.White, 0,
                Vector2.Zero, SpriteEffects.None, 1);
        }
    }

}
