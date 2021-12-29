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
        private SpriteFont _scoreFont;
        private bool i;
        public Score(SpriteFont font, List<string> tricks)
        {
            _tricks = tricks;
            _scoreFont = font;
            _trickIndex = 0;
            i = false;
        }

        public void Trick(int trickIndex)
        {
            _trickIndex = trickIndex;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(_scoreFont, _tricks[_trickIndex], new Vector2(100, 100), Color.White);
        }
    }
}
