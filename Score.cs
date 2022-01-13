using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trick_tests
{
    class Score
    {
        private List<string> _tricks = new List<string>();
        private int _trickIndex;
        private double _score;
        private double _highScore;
        private double _lastScore;
        private SpriteFont _scoreFont;
        private bool i;
        public Score(SpriteFont font, List<string> tricks)
        {
            _tricks = tricks;
            _scoreFont = font;
            _trickIndex = 0;
            _score = 0;
            _highScore = 0;
            i = false;
        }

        public void Trick(int trickIndex)
        {
            _trickIndex = trickIndex;

            _lastScore = trickIndex;
            _score = (_score) + (_lastScore / 10);

            if (trickIndex == 6)
            {
                _score = 0;
            }
        }

        public void HighScore()
        {
            if (_score > _highScore)
            {
                _highScore = _score;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(_scoreFont, _tricks[_trickIndex], new Vector2(50, 50), Color.White);
            _spriteBatch.DrawString(_scoreFont, "score: " + Math.Round(_score).ToString(), new Vector2(50, 90), Color.White);
            _spriteBatch.DrawString(_scoreFont, "highscore: " + Math.Round(_highScore).ToString(), new Vector2(50, 130), Color.White);
        }
    }
}
