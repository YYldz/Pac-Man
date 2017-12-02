using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace PacMan_byYakupY.Objects
{
    class PacMan : GameObjects
    {
        private double frameTimer = 100;
        private int frameinterval = 100;
        private int frame;
        public static bool isAlive = true;

        //public static Vector2 targetPosPac;

        private int speed;

        public static int tries = 3;

        int pixelWalked = 0;



        public PacMan(Texture2D texture, Vector2 position,
            Point size, int speed)
            : base(texture, position, size)
        {
            this.speed = speed;
        }

        public void MovePacperTile()
        {
            if (pixelWalked == Manager.tileSize - speed)
            {
                pixelWalked = 0;
                direction.X = 0;
                direction.Y = 0;
            }

            if (pixelWalked != Manager.tileSize - speed)
            {
                if (!(position == previousSpot && (int)direction.X
                    == 0 && (int)direction.Y == 0))
                {
                    pixelWalked += speed;
                }
            }
        }

        

        public override void Update(GameTime gameTime)
        {
            previousSpot = position;
            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameinterval;
                frame++;
                spriteRec.X = (frame % 4) * 50;
            }
            
            MovePacperTile();
            
            if (pixelWalked == 0 && PacMan.isAlive == true)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    // Vänder på spriten genom att starta första frame till nästa rad.
                    spriteRec.Y = 86;
                    direction.Y = 0;
                    direction.X = -1;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    spriteRec.Y = 172;
                    direction.Y = 0;
                    direction.X = 1;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    spriteRec.Y = 258;
                    direction.X = 0;
                    direction.Y = -1;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    spriteRec.Y = 0;
                    direction.X = 0;
                    direction.Y = 1;
                } 
            }

            position.X += speed * direction.X;
            position.Y += speed * direction.Y;

            // Byt håll på "pacman" när han går utanför banan
            if (position.X == Game1.screenWidth + 40)
            {
                position.X = -30;
            }
            if (position.X == -40)
            {
                position.X = Game1.screenWidth;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (isAlive)
            {
                spriteBatch.Draw(texture, Bounds(),
                        spriteRec, Color.White, 0,
                        Vector2.Zero, SpriteEffects.None, 1); 
            }
        }


    }
}