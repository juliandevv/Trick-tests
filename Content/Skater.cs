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
        private Score _score;
        private bool _crash;
        public enum State
        {
            jumping,
            riding,
            up,
            falling,
            down
        }

        private State _currentState;
        private State _lastState;

        //jump
        private float velocity, elapsedTime, startTime, initialSpeed, yBeforeJump;

        //animation
        private float animationStartTime, elapsedAnimationTime;
        private int frame;

        public Skater(List<Texture2D> skaterTextures, Rectangle rect, List<Texture2D> boardTextures, List<Obstacle> obstacles, Score score)
        {
            _skaterTextures = skaterTextures;
            _skaterBounds = rect;
            startTime = 0;
            animationStartTime = 0;
            _currentState = State.riding;
            _lastState = _currentState;
            _obstacles = obstacles;
            _score = score;
            _crash = false;

            _board = new Board(boardTextures, new Rectangle(_skaterBounds.X, _skaterBounds.Y, 112, 50));

            frame = 0;
        }

        public Rectangle Bounds
        {
            get { return _skaterBounds; }
            set { _skaterBounds = value; }
        }

        public bool CrashState
        {
            get { return _crash; }
        }


        public void Update(GameTime gameTime, State state)
        {
            _board.Update(this);

            //animate
            elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 450 && _currentState == State.riding)
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

            if (_currentState == State.up)
            {
                _skaterBounds.Y -= 2;
            }

            if (_currentState == State.down)
            {
                _skaterBounds.Y += 2;
            }

            foreach (Obstacle obstacle in _obstacles)
            {
                if (obstacle.CheckCollisions(this) == State.up)
                {
                    frame = 2;
                    _skaterBounds.Y -= 1;
                }

                if (obstacle.CheckCollisions(this) == State.falling)
                {
                    _skaterBounds.Y += 3;
                }
            }

            //did the trick land
            if (_board.Frame != 0 && _currentState == State.riding && _crash == false)
            {
                _score.HighScore();
                _score.Trick(6);
                _crash = true;
            }

            else
            {
                _crash = false;
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

                if (initialSpeed >= 40)
				{
                    initialSpeed = 40;
				}
            }
        }

        public void Trick(Game1.Trick trick, GameTime gameTime)
        {
            if (_currentState == State.jumping && trick == Game1.Trick.None)
            {
                _score.Trick(1);
            }

            if (_currentState == State.jumping && trick == Game1.Trick.FrontsideShuv)
            {
                _board.FrontsideShuv(gameTime);
                _score.Trick(2);
            }

            if (_currentState == State.jumping && trick == Game1.Trick.BacksideShuv)
            {
                _board.BacksideShuv(gameTime);
                _score.Trick(3);
            }

            if (_currentState == State.jumping && trick == Game1.Trick.Kickflip)
            {
                _board.Kickflip(gameTime);
                _score.Trick(4);
            }

            if (_currentState == State.jumping && trick == Game1.Trick.Heelflip)
            {
                _board.Heelflip(gameTime);
                _score.Trick(5);
            }

            if (_currentState != State.jumping && trick == Game1.Trick.Up)
            {
                frame = 2;
                _board.Up();
                _currentState = State.up;
            }

            if (_currentState != State.jumping && trick == Game1.Trick.Down)
            {
                frame = 3;
                _board.Down();
                _currentState = State.down;
            }

            if (_currentState != State.jumping && trick == Game1.Trick.None)
            {
                _board.Straight();
                _score.Trick(0);
                _currentState = State.riding;
            }
        }

        public void Crash()
        {
            frame = 3;
        }

        public void Reset()
        {
            frame = 0;
            _crash = false;
            _board.Reset();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            //if (_currentState == State.riding)
            //{
            //}

            _score.Draw(_spriteBatch);
            _board.Draw(_spriteBatch);
            _spriteBatch.Draw(_skaterTextures[frame], _skaterBounds, Color.White);
        }
    }
}
