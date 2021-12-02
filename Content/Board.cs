using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trick_tests
{
    class Board
    {
        private List<Texture2D> _boardTextures = new List<Texture2D>();
        private Rectangle _boardBounds;
        private float animationStartTime, elapsedAnimationTime;
        int frame;

        public Board(List<Texture2D> textures, Rectangle rect)
        {
            _boardTextures = textures;
            _boardBounds = rect;
            frame = 0;
        }

        public Rectangle Bounds
        {
            get { return _boardBounds; }
            set { _boardBounds = value; }
        }

        public void Update(Skater skater)
        {
            _boardBounds.X = skater.Bounds.Center.X - _boardBounds.Width/2;
            _boardBounds.Y = skater.Bounds.Bottom - _boardBounds.Height;
            _boardBounds.Width = _boardTextures[frame].Width;
            _boardBounds.Height = _boardTextures[frame].Height;

        }
        public void FrontsideShuv(GameTime gameTime)
        {
            elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 180)
            {
                if (frame < 3)
                    frame++;

                else if (frame >= 3)
                {
                    frame = 0;
                }

                animationStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void BacksideShuv(GameTime gameTime)
        {
            elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 150)
            {
                if (frame == 0 || frame >= 4)
                    frame = 3;

                else if (frame > 0 && frame < 4)
                {
                    frame--;
                }

                animationStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void Kickflip(GameTime gameTime)
        {
            elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 150)
            {
                if (frame < 4)
                    frame = 4;

                else if (frame < 6)
                {
                    frame++;
                }

                else if (frame >= 6)
				{
                    frame = 0;
				}

                animationStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void Heelflip(GameTime gameTime)
        {
            elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 180)
            {
                if (frame < 4)
                    frame = 6;

                else if (frame > 4)
                {
                    frame--;
                }

                else if (frame <= 4)
                {
                    frame = 0;
                }

                animationStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_boardTextures[frame], _boardBounds, Color.White);
        }

    }
}
