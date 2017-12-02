using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PacMan_byYakupY.Objects
{
    class SmartGhost: GameObjects
    {
        // EJ KLAR!!!!!!!!
        
        //private double frameTimer = 100;
        //private int frameinterval = 100;
        //private int frame;
        public SmartGhost(Texture2D texture, Vector2 position, Point size)
           : base(texture, position, size)
        {
            

        }
        //#region GhostMechanics
        //public static void SmartGhostCollision()
        //{
        //    foreach (GameObjects g in Manager.gameObjArray)
        //    {
        //        if (g is Objects.PacMan && g.Bounds().Intersects(Manager.enemies2.Bounds()))
        //        {
        //            if (PacMan.isAlive)
        //            {
        //                PacMan.isAlive = false;
        //            }
        //        }
        //    }
        //}

        //public static void ghostAI()
        //{
        //    if (true)
        //    {
                
        //    }
        //}
        //#endregion

        //public override void Update(GameTime gameTime)
        //{
        //    frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
        //    if (frameTimer <= 0)
        //    {
        //        frameTimer = frameinterval;
        //        frame++;
        //        ghostSprite.X = (frame % 2) * 21;
        //    }
        //}

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(texture, Bounds(),
        //        ghostSprite, Color.White, 0,
        //        Vector2.Zero, SpriteEffects.None, 1);
        //}
    }
    
}
