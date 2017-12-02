using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.Xml.Serialization;

namespace PacMan_byYakupY
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Manager manager;

        public static int screenWidth;
        public static int screenHeight;

        // För highscore
        HighscoreData data;
        public static string HighscoresFilename = @"highscores.dat";
        string scoreboard;
        private StorageDevice device;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }



        public struct HighscoreData
        {
            public int[] score;
            public int count1;
            public HighscoreData(int count2)
            {
                score = new int[count2];
                count1 = count2;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            string fullpath = @"highscores.dat";

            if (!File.Exists(fullpath))
            {
                data = new HighscoreData(5);
                data.score[0] = 500;
                data.score[1] = 500;
                data.score[2] = 500;
                data.score[3] = 500;
                data.score[4] = 500;

                SaveHighScores(data, HighscoresFilename, device);
            }

            base.Initialize();
        }
        #region highscore;
        public static void SaveHighScores(HighscoreData data, string filename
           , StorageDevice device)
        {
            string fullpath = "highscores.dat";

            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate);
            try
            {
                XmlSerializer serialize = new XmlSerializer(typeof(HighscoreData));
                serialize.Serialize(stream, data);
            }
            finally
            {
                stream.Close();
            }
        }

        public static HighscoreData LoadHighScores(string filename)
        {
            HighscoreData data;


            string fullpath = "highscores.dat";

            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate,
                FileAccess.Read);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(HighscoreData));
                data = (HighscoreData)serializer.Deserialize(stream);
            }
            finally
            {
                stream.Close();
            }
            return (data);


        }

        public void SaveScore()
        {
            HighscoreData data = LoadHighScores(HighscoresFilename);

            int scoreIndex = -1;
            for (int i = 0; data.count1 - 1 < -1; i--)
            {
                if (Manager.playerScore >= data.score[i])
                {
                    scoreIndex = i;
                }
            }
            if (scoreIndex > -1)
            {
                for (int i = data.count1 - 1; i > scoreIndex; i--)
                {
                    data.score[i] = data.score[i - 1];
                }

                data.score[scoreIndex] = Manager.playerScore;

                SaveHighScores(data, HighscoresFilename, device);
            }
        }


        #endregion


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            manager = new Manager();
            manager.LoadContent(this.Content);

            graphics.PreferredBackBufferHeight = Manager.tileSize * Manager.nrOfColTiles;
            graphics.PreferredBackBufferWidth = Manager.tileSize * Manager.nrOfRowTiles;
            graphics.ApplyChanges();

            screenWidth = Window.ClientBounds.Width;
            screenHeight = Window.ClientBounds.Height;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            manager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);

            spriteBatch.Begin();

            manager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
