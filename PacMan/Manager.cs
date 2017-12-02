using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;

namespace PacMan_byYakupY
{
    class Manager
    {
        #region Variabler
        public static GameObjects[,] gameObjArray;
        List<string> myScore = new List<string>();
        public static int nrOfRowTiles = 15;
        public static int nrOfColTiles = 15;
        List<string> myMap = new List<string>();
        SpriteFont myFont;
        SpriteFont gameOverFont;

        public static int playerScore = 0;

        Texture2D wallTex, spriteSheet, food;
        GameObjects pacman;

        public static int tileSize = 50;
        public int foodOffset = 15;

        int dotsRemaining;

        private Texture2D enemyOne;
        //private Texture2D enemyTwo;

        bool hasWon = false;

        string failText = "You Have Died! Press Enter to restart";
        string winText = "You have Done It! Press Enter to restart";


        public GameObjects enemies;
        //public static GameObjects enemies2;

        Random rand = new Random();
        protected Texture2D bonusRupee;

        #endregion

        #region Const

        public Manager()
        {
            gameObjArray = new GameObjects[nrOfRowTiles, nrOfColTiles];
        }

        public void LoadContent(ContentManager contentMan)
        {

            wallTex = contentMan.Load<Texture2D>("wall");
            spriteSheet = contentMan.Load<Texture2D>("KidLink");
            food = contentMan.Load<Texture2D>("YellowRupee");
            enemyOne = contentMan.Load<Texture2D>("RedEnemy");
            //enemyTwo = contentMan.Load<Texture2D>("BlueEnemy");
            myFont = contentMan.Load<SpriteFont>(@"myFont");
            gameOverFont = contentMan.Load<SpriteFont>(@"GameOver");
            bonusRupee = contentMan.Load<Texture2D>("RedRupee");


            ReadFromMap();
            CreateGameObjects();

        }
        public void Initialize()
        {

        }

        #endregion


        #region ReadFromFiles


        private void ReadFromMap()
        {
            StreamReader mapReader = new StreamReader(@"...\debug\Levels\map.txt");
            do
            {
                myMap.Add(mapReader.ReadLine());

            } while (!mapReader.EndOfStream);
            mapReader.Close();
            // Sparar ned något i en fil!
            // False används om man bara vill skriva in något en gång i en fil.
            StreamWriter sw = new StreamWriter(@"...\debug\Levels\HS.txt",false);
            sw.WriteLine("hahahah");
            sw.Close();
        }
        #endregion

        #region CreateGameObjects
        private void CreateGameObjects()
        {
            // Rader = X-led, Kolumner = Y-led
            for (int column = 0; column < nrOfColTiles; column++)
            {
                for (int row = 0; row < nrOfRowTiles; row++)
                {
                    if (myMap[column][row] == 'w')
                    {
                        gameObjArray[row, column] = new Objects.Wall(wallTex,
                            new Vector2(row * tileSize,
                                column * tileSize),
                            new Point(tileSize, tileSize));
                    }

                    if (myMap[column][row] == 'p')
                    {
                        pacman = new Objects.PacMan(spriteSheet,
                            new Vector2(row * tileSize,
                                column * tileSize),
                            new Point(tileSize, tileSize), 5);

                        gameObjArray[row, column] = pacman;
                    }
                    if (myMap[column][row] == 'e')
                    {
                        enemies = new Objects.Ghost(enemyOne,
                            new Vector2(row * tileSize, column * tileSize),
                            new Point(tileSize, tileSize));

                        gameObjArray[row, column] = enemies;
                    }
                    // Skulle användas till "smarta" spöket
                    //if (myMap[column][row] == 't')
                    //{
                    //    enemies2 = new Objects.Ghost(enemyTwo,
                    //        new Vector2(row * tileSize, column * tileSize),
                    //        new Point(tileSize, tileSize));

                    //    gameObjArray[row, column] = enemies2;
                    //}
                    if (myMap[column][row] == 'f')
                    {
                        gameObjArray[row,column] = new FoodObjects.Fruit(bonusRupee,
                            new Vector2(row * tileSize + 5,
                                column * tileSize + 5),
                                new Point(40, 40), false);
                    }
                    if (myMap[column][row] == '-')
                    {
                        gameObjArray[row, column] = new FoodObjects.Dots(food,
                            new Vector2(row * tileSize + foodOffset,
                                column * tileSize + foodOffset),
                            new Point(20, 20), false);
                        dotsRemaining++;
                    }
                }
            }
        }
        #endregion


        #region CollisionSystem
        public void StudsaTebax()
        {
            pacman.position = pacman.previousSpot;
        }
        public void StudsaTebaxGhost(GameObjects g)
        {
            g.position = g.previousSpot;

            float tempX = g.direction.X;
            float tempY = g.direction.Y;


            g.direction.X = 0;
            g.direction.Y = 0;


            g.direction = GhostAI(g.direction.X, g.direction.Y, tempX, tempY);
        }

        // Denna metod rör fienderna runt på banan.
        public Vector2 GhostAI(float X, float Y, float tempX, float tempY)
        {
            if (tempX != 0)
            {
                do
                {
                    Y = rand.Next(-1, 2);
                } while (Y == 0);
            }
            if (tempY != 0)
            {
                do
                {
                    X = rand.Next(-1, 2);
                } while (X == 0);
            }

            return new Vector2(X, Y);
        }

        public void Collision()
        {
            foreach (GameObjects g in gameObjArray)
            {
                if (g is Objects.Wall && g.Bounds().Intersects(pacman.Bounds()))
                {
                    StudsaTebax();
                }
                if (g is FoodObjects.Dots && g.Bounds().Intersects(pacman.Bounds()))
                {
                    if (!g.beenEaten)
                    {
                        g.beenEaten = true;
                        playerScore += 50;
                        dotsRemaining--;
                    }

                }
                if (g is FoodObjects.Fruit && g.Bounds().Intersects(pacman.Bounds()))
                {
                    if (!g.beenEaten)
                    {
                        g.beenEaten = true;
                        playerScore += 1000;
                    }
                }
            }
            foreach (GameObjects g in gameObjArray)
            {
                foreach (GameObjects v in gameObjArray)
                {
                    if (g is Objects.Ghost && v is Objects.Wall && g.Bounds().Intersects(v.Bounds()))
                    {
                        StudsaTebaxGhost(g);
                    }
                    if (g is Objects.PacMan && v is Objects.Ghost && g.Bounds().Intersects(v.Bounds()))
                    {
                        Objects.PacMan.tries--;
                        pacman.position.X = 350;
                        pacman.position.Y = 500;
                    }
                }
            }

        }
        #endregion
        
        #region Update
        public void Update(GameTime gameTime)
        {
            if (!hasWon && Objects.PacMan.isAlive)
            {
                foreach (GameObjects g in gameObjArray)
                {
                    if (g != null)
                    {
                        g.Update(gameTime);
                    }
                }

                if (Objects.PacMan.tries == 0)
                {
                    Objects.PacMan.isAlive = false;
                    dotsRemaining = 0;
                    playerScore = 0;
                }

                if (dotsRemaining == 0)
                {
                    hasWon = true;
                }

                Collision();
            }

            if (hasWon && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                hasWon = false;
                ReadFromMap();
                CreateGameObjects();
                Objects.PacMan.isAlive = true;
                Objects.PacMan.tries = 3;
            }
            if (!Objects.PacMan.isAlive &&
                Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Objects.PacMan.isAlive = true;
                Objects.PacMan.tries = 3;
                ReadFromMap();
                CreateGameObjects();
            }

            //Objects.SmartGhost.SmartGhostCollision();
        }
        #endregion

        public string makeHighScoreString()
        {
            Game1.HighscoreData data2 = Game1.LoadHighScores(Game1.HighscoresFilename);

            string scoreBoardString = "Highscores:\n\n";

            for (int i = 0; i < 5; i++)
            {
                scoreBoardString = scoreBoardString + data2.score[i] + "\n";
            }
            return scoreBoardString;
        }

        #region Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!Objects.PacMan.isAlive)
            {
                spriteBatch.DrawString(gameOverFont, failText, new Vector2(Game1.screenWidth / 4, Game1.screenHeight / 3), Color.Red);
                spriteBatch.DrawString(gameOverFont, makeHighScoreString(), new Vector2(50, 50), Color.Black);
            }
            else if (hasWon)
            {
                spriteBatch.DrawString(gameOverFont, makeHighScoreString(), new Vector2(50, 50), Color.Black);
                spriteBatch.DrawString(gameOverFont, winText, new Vector2(Game1.screenWidth / 4, Game1.screenHeight / 3), Color.Black);
            }
            else if (!hasWon && Objects.PacMan.isAlive)
            {

                foreach (GameObjects g in gameObjArray)
                {
                    if (g != null)
                    {
                        g.Draw(spriteBatch);
                    }
                }
            }

            string pts = "Points: " + playerScore;
            string lives = "Lives: " + Objects.PacMan.tries;

            spriteBatch.DrawString(myFont, pts, Vector2.Zero, Color.LightBlue);
            spriteBatch.DrawString(myFont, lives, new Vector2(0, Game1.screenHeight - 50), Color.LightBlue);
        }
        #endregion
    }
}
