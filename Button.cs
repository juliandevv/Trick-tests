using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trick_tests
{
    class Button
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        //string _buttonName;

        public Button(Texture2D texture, Rectangle rect)
        {
            _texture = texture;
            _rectangle = rect;
           // _buttonName = name;
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public bool EnterButton(MouseState mouseState)
        {
            if (_rectangle.Contains(mouseState.X, mouseState.Y))
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch _spriteBatch, Color color)
        {
            _spriteBatch.Draw(_texture, _rectangle, color);
        }
    }
}
