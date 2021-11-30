using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trick_tests
{
    class Skater
    {
        private List<Texture2D> _skaterTextures;
        private Rectangle _skaterBounds;

        //jump
        private bool jumping;
        private float velocity, elapsedTime, startTime, initialSpeed, yBeforeJump;

        //animation
        private float animationStartTime, elapsedAnimationTime;
        private int frame;

        public Skater(List<Texture2D> textures, Rectangle rect)
        {
            _skaterTextures = textures;
            _skaterBounds = rect;
            jumping = false;
            initialSpeed = 35;
            startTime = 0;
            animationStartTime = 0;

            frame = 0;
        }

        public Rectangle Bounds
        {
            get { return _skaterBounds; }
            set { _skaterBounds = value; }
        }

        public void Update(GameTime gameTime, KeyboardState keyboardState)
        {
            //animate

            elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 450 && jumping == false)
            {
                if (frame == 1)
                    frame = 0;
                else
                    frame = 1;

                animationStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            }

            //move

            if (jumping == true)
            {
                //jumping
                elapsedTime = (float)(gameTime.TotalGameTime.TotalMilliseconds - startTime) / 1000f;
                velocity = (initialSpeed - (40f * elapsedTime));
                _skaterBounds.Y -= (int)(velocity * elapsedTime);

                if (velocity >= -velocity)
                {
                    frame = 2;
                }

                if (velocity <= -velocity)
                {
                    frame = 3;
                }

                if (_skaterBounds.Y >= yBeforeJump && elapsedTime >= 0.5f)
                {
                    _skaterBounds.Y = (int)yBeforeJump;
                    jumping = false;
                }
            }
        }

        public void Jump(GameTime gameTime)
        {
            if (jumping == false)
            {
                yBeforeJump = _skaterBounds.Y;
                startTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
                jumping = true;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_skaterTextures[frame], _skaterBounds, Color.White);
        }
    }
}
