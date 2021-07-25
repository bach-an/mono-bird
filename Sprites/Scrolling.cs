using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FlappyBird.Core;

namespace FlappyBird.Sprites
{
    /* Represents a Sprite that will scroll across the screen */
    class Scrolling : Sprite
    {
        public Scrolling(Texture2D texture) : base(texture)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Position -= new Vector2(Constants.SCROLL_SPEED, 0);
            if (Position.X < -_texture.Width)
                // Offset the texture so it blends into the previous one
                Position = new Vector2(_texture.Width - 5, Position.Y);
            
        }
    }
}
