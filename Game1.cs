using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Trick_tests
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Texture2D> skaterTextures = new List<Texture2D>();
        List<Texture2D> boardTextures = new List<Texture2D>();
        Skater skater;
        KeyboardState keyboardState;
        
        public enum Trick
        {
            None,
            BacksideShuv,
            FrontsideShuv
        }

        Trick trick;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            skater = new Skater(skaterTextures, new Rectangle(_graphics.PreferredBackBufferWidth / 4, 280, 141, 180), boardTextures);

            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            skaterTextures.Add(Content.Load<Texture2D>("newskaterTexture1"));
            skaterTextures.Add(Content.Load<Texture2D>("newskaterTexture2"));
            skaterTextures.Add(Content.Load<Texture2D>("newUp"));
            skaterTextures.Add(Content.Load<Texture2D>("newDown"));

            boardTextures.Add(Content.Load<Texture2D>("Board Straight"));
            boardTextures.Add(Content.Load<Texture2D>("Board Up"));
            boardTextures.Add(Content.Load<Texture2D>("Board Perpendicular"));
            boardTextures.Add(Content.Load<Texture2D>("Board Down"));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.S))
            {
                skater.Jump(gameTime);
            }

            else if (keyboardState.IsKeyDown(Keys.D))
            {
                trick = Trick.BacksideShuv;
            }

            else if (keyboardState.IsKeyDown(Keys.A))
            {
                trick = Trick.FrontsideShuv;
            }

            else
            {
                trick = Trick.None;
            }

            skater.Trick(trick, gameTime);
            skater.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            skater.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
