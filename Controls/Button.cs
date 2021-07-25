using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.Controls
{
    class Button : Component
    {
        #region Fields
        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D _texture;

        #endregion

        #region Properties

        public event EventHandler Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; } // Color of text

        public Vector2 Position { get; set; }
        public Rectangle DrawRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        private Rectangle ClickRectangle
        {
            get
            {
                return new Rectangle(((int)Position.X + _texture.Width / 2 + 23), (int)Position.Y * 2, _texture.Width * 2, _texture.Height * 2);
            }

        }

        public string Text;
        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColor = Color.Black;
        }
        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            if (_isHovering)
                color = Color.Gray;
            spriteBatch.Draw(_texture, DrawRectangle, color);
            if(!string.IsNullOrEmpty(Text))
            {
                float x = (DrawRectangle.X + (DrawRectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                float y = (DrawRectangle.Y + (DrawRectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);
                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);

            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHovering = false;
            if (mouseRectangle.Intersects(ClickRectangle))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    // If click event handler != null
                    Click?.Invoke(this, new EventArgs());
                }

            }
        }
        #endregion
    }
}
