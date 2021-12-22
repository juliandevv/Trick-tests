using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trick_tests
{
    class BackgroundObject
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        private Vector2 _location;
       

        public BackgroundObject(Texture2D texture, Rectangle rect, Vector2 speed)
        {
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
            _location = new Vector2(rect.X, rect.Y);
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value;
                _location = new Vector2(value.X, value.Y); }
        }
        
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        //public Point Location
        //{
        //    get { return _location; }
        //    set { _location = value; }
        //}

        //public BackgroundObject CompareTo(BackgroundObject compareSpeed)
        //{
        //    return this.Speed.X.CompareTo(compareSpeed.Speed.X);
        //}

        //public void Reset(GraphicsDeviceManager _graphics)
        //{
        //    _location.X = genertaor.Next(_graphics.PreferredBackBufferWidth - 100, _graphics.PreferredBackBufferWidth);
        //    _rectangle = new Rectangle((int)_location.X, (int)_location.Y, _rectangle.Width, _rectangle.Height);
        //}

        public void Move()
        {
     
            _location.X += _speed.X;
            _rectangle.Location = new Point(_location.ToPoint().X, _location.ToPoint().Y);

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(this.Texture, this.Bounds, Color.White);
        }
    }

}
