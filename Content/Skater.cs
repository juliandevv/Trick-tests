using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Trick_tests;

namespace Trick_tests
{
    class Skater
    {
        private List<Texture2D> _skaterTextures;
        private List<Obstacle> _obstacles;
        private Rectangle _skaterBounds;
        private Board _board;
        public enum State
        {
            jumping,
            riding,
            up,
            falling,
            down
        }

        private State _currentState;

        //jump
        private bool jumping;
        private float velocity, elapsedTime, startTime, initialSpeed, yBeforeJump;

        //animation
        private float animationStartTime, elapsedAnimationTime;
        private int frame;

        public Skater(List<Texture2D> skaterTextures, Rectangle rect, List<Texture2D> boardTextures, List<Obstacle> obstacles)
        {
            _skaterTextures = skaterTextures;
            _skaterBounds = rect;
            jumping = false;
            startTime = 0;
            animationStartTime = 0;
            _currentState = State.riding;
            _obstacles = obstacles;

            _board = new Board(boardTextures, new Rectangle(_skaterBounds.X, _skaterBounds.Y, 112, 50));

            frame = 0;
        }

        public Rectangle Bounds
        {
            get { return _skaterBounds; }
            set { _skaterBounds = value; }
        }


        public void Update(GameTime gameTime, State state)
        {
            _board.Update(this);

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
            if (_currentState == State.jumping)
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

                if (_skaterBounds.Y >= yBeforeJump && elapsedTime >= 0.1f)
                {
                    _skaterBounds.Y = (int)yBeforeJump;
                    _currentState = State.riding;
                }
            }

            if (state == State.up && _currentState == State.riding)
            {
                _skaterBounds.Y -= 2;
                frame = 2;
                _board.Up();
            }

            if (state == State.down && _currentState == State.riding)
            {
                _skaterBounds.Y += 2;
                frame = 3;
                _board.Down();
            }

            if (_currentState == State.riding && state != State.down && state != State.up)
            {
                _board.Straight();
            }

            foreach (Obstacle obstacle in _obstacles)
            {
                if (obstacle.CheckCollisions(this) == State.up)
                {
                    _skaterBounds.Y -= 1;
                    frame = 1;
                }

                if (obstacle.CheckCollisions(this) == State.falling)
                {
                    _skaterBounds.Y += 2;
                }
            }

        }

        public void Jump(GameTime gameTime, float jumpStartTime)
        {
            if (_currentState == State.riding)
            {
                initialSpeed = (float)(gameTime.TotalGameTime.TotalMilliseconds - jumpStartTime) / 1000f * 55f;
                yBeforeJump = _skaterBounds.Y;
                startTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
                _currentState = State.jumping;
                //jumping = true;

                if (initialSpeed >= 40)
				{
                    initialSpeed = 40;
				}
            }
        }

        public void Trick(Game1.Trick trick, GameTime gameTime)
        {
            if (_currentState == State.jumping && trick == Game1.Trick.FrontsideShuv)
            {
                _board.FrontsideShuv(gameTime);
            }

            if (_currentState == State.jumping && trick == Game1.Trick.BacksideShuv)
            {
                _board.BacksideShuv(gameTime);
            }

            if (_currentState == State.jumping && trick == Game1.Trick.Kickflip)
            {
                _board.Kickflip(gameTime);
            }

            if (_currentState == State.jumping && trick == Game1.Trick.Heelflip)
            {
                _board.Heelflip(gameTime);
            }

            if (_currentState == State.jumping && trick == Game1.Trick.None)
            {
               
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _board.Draw(_spriteBatch);
            _spriteBatch.Draw(_skaterTextures[frame], _skaterBounds, Color.White);
        }
    }
}
