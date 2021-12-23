using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trick_tests
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //skater
        List<Texture2D> skaterTextures = new List<Texture2D>();
        List<Texture2D> boardTextures = new List<Texture2D>();
        Skater skater;
        KeyboardState keyboardState;
        float jumpStartTime;
        bool jumpKeyPressed;

        //background
        List<BackgroundObject> backgroundObjects = new List<BackgroundObject>();
        List<Texture2D> backgroundTextures = new List<Texture2D>();
        Texture2D street;
        Vector2 speedLevel1, speedLevel2, speedLevel3;
        Random generator;

        //obstales
        List<Obstacle> obstacles = new List<Obstacle>();
        List<Texture2D> obstacleTextures = new List<Texture2D>();


        public enum Trick
        {
            None,
            BacksideShuv,
            FrontsideShuv,
            Kickflip,
            Heelflip

        }

        public enum Screen
        {
            Title,
            MainGame
        }

        Trick trick;
        Screen currentScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            speedLevel1 = new Vector2(-2f, 0);
            speedLevel2 = new Vector2(-1f, 0);
            speedLevel3 = new Vector2(-0.5f, 0);

            generator = new Random();

            base.Initialize();

            //initial screen
            currentScreen = Screen.Title;

            //background
            backgroundObjects.Add(new BackgroundObject(backgroundTextures[0], new Rectangle(200, 230, 60, 60), speedLevel1));
            backgroundObjects.Add(new BackgroundObject(backgroundTextures[0], new Rectangle(400, 200, 40, 40), speedLevel2));
            backgroundObjects.Add(new BackgroundObject(backgroundTextures[0], new Rectangle(600, 180, 30, 30), speedLevel3));
            backgroundObjects.Add(new BackgroundObject(backgroundTextures[0], new Rectangle(900, 180, 30, 30), speedLevel3));

            //sort list by speed
            backgroundObjects = backgroundObjects.OrderByDescending(o => o.Speed.X).ToList();

            //obstacles
            obstacles.Add(new Obstacle(obstacleTextures[0], new Rectangle(950, 300, 180, 100), speedLevel1));

            //skater
            skater = new Skater(skaterTextures, new Rectangle(_graphics.PreferredBackBufferWidth / 4, 280, 141, 180), boardTextures);
            jumpKeyPressed = false;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //street
            street = Content.Load<Texture2D>("ROAD 2 (bigger)");

            //background textures
            backgroundTextures.Add(Content.Load<Texture2D>("bush1"));
            backgroundTextures.Add(Content.Load<Texture2D>("tree"));

            //obstacle textures
            obstacleTextures.Add(Content.Load<Texture2D>("Kicker"));

            //skater textures
            skaterTextures.Add(Content.Load<Texture2D>("newskaterTexture1"));
            skaterTextures.Add(Content.Load<Texture2D>("newskaterTexture2"));
            skaterTextures.Add(Content.Load<Texture2D>("newUp"));
            skaterTextures.Add(Content.Load<Texture2D>("newDown"));

            //board textures
            boardTextures.Add(Content.Load<Texture2D>("Board Straight")); //-----[0]regular
            boardTextures.Add(Content.Load<Texture2D>("Board Up")); //-----------[1]ollie, front shuv
            boardTextures.Add(Content.Load<Texture2D>("Board Perpendicular")); //[2]back/front shuv
            boardTextures.Add(Content.Load<Texture2D>("Board Down")); //---------[3]ollie, back shuv
            boardTextures.Add(Content.Load<Texture2D>("Board Turn Up")); //------[4]kickflip
            boardTextures.Add(Content.Load<Texture2D>("Board Upside Down")); //--[5]kick/heel flip
            boardTextures.Add(Content.Load<Texture2D>("Board Turn Down")); //----[6]heelflip

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //check screen change
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                currentScreen = Screen.MainGame;

            //title update logic
            if (currentScreen == Screen.Title)
            {
                Title();
            }

            //maingame update logic
            if (currentScreen == Screen.MainGame)
            {
                MainGame(gameTime);
            }

            base.Update(gameTime);
        }

        protected void Title()
        {

        }

        protected void MainGame(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            //background
            for (int i = 0; i < backgroundObjects.Count; i++)
            {

                backgroundObjects[i].Move();

                if (backgroundObjects[i].Bounds.X <= (0 - backgroundObjects[i].Bounds.Width))
                {
                    backgroundObjects[i].Bounds = new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferWidth + 100), backgroundObjects[i].Bounds.Y, backgroundObjects[i].Bounds.Width, backgroundObjects[i].Bounds.Height);
                }
            }

            //obstacles
            for (int i = 0; i < obstacles.Count; i++)
            {

                obstacles[i].Move();

                if (obstacles[i].Bounds.X <= (0 - obstacles[i].Bounds.Width))
                {
                    obstacles[i].Bounds = new Rectangle(generator.Next(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferWidth + 100), generator.Next(300, 400), obstacles[i].Bounds.Width, obstacles[i].Bounds.Height);
                }
            }

            //skater
            if (keyboardState.IsKeyDown(Keys.S))
            {
                if (jumpKeyPressed == false)
                {
                    jumpStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
                    jumpKeyPressed = true;
                }
            }

            if (keyboardState.IsKeyUp(Keys.S) && jumpKeyPressed == true)
            {
                skater.Jump(gameTime, jumpStartTime);
                jumpKeyPressed = false;
            }

            else if (keyboardState.IsKeyDown(Keys.D))
            {
                trick = Trick.BacksideShuv;
            }

            else if (keyboardState.IsKeyDown(Keys.A))
            {
                trick = Trick.FrontsideShuv;
            }

            else if (keyboardState.IsKeyDown(Keys.W))
            {
                trick = Trick.Kickflip;
            }

            else if (keyboardState.IsKeyDown(Keys.X))
            {
                trick = Trick.Heelflip;
            }

            else
            {
                trick = Trick.None;
            }

            skater.Trick(trick, gameTime);
            skater.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            //draw title elements
            if (currentScreen == Screen.Title)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                DrawTitle();
            }

            //draw main game elements
            if (currentScreen == Screen.MainGame)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                DrawMainGame();
            }

            base.Draw(gameTime);
        }

        protected void DrawTitle()
        {

        }

        protected void DrawMainGame()
        {
            _spriteBatch.Begin();

            //draw street
            _spriteBatch.Draw(street, new Rectangle(0, 200, 1200, 400), Color.White);

            //draw background
            for (int i = 0; i < backgroundObjects.Count; i++)
            {
                backgroundObjects[i].Draw(_spriteBatch);
            }

            //obstacles
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Draw(_spriteBatch);
            }

            //draw skater
            skater.Draw(_spriteBatch);

            _spriteBatch.End();
        }

    }
}
