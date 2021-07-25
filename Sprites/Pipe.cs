using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using FlappyBird.Core;
using System.Diagnostics;

namespace FlappyBird.Sprites
{
    class Pipe : Scrolling
    {
        private int pipeHeight;
        private readonly Random rd = new Random();
        private Rectangle _invertedDrawRect;
        public Rectangle _pointBarrier;
        public Pipe(Texture2D texture) : base(texture) 
        {
            // A rectangle next to the pipe that adds a point on intersection
            _pointBarrier = new Rectangle((int)Position.X + texture.Width, 0, 5, Constants.GAME_SCREEN_HEIGHT);
            GeneratePipeHeight(texture);
        }

        public void GeneratePipeHeight(Texture2D texture)
        {
            pipeHeight = rd.Next(10, texture.Height - 10);
            drawRect.Height = texture.Height - pipeHeight;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Account for the random pipeHeight
            Vector2 drawPosition = new Vector2(Position.X, Position.Y + pipeHeight);

            _invertedDrawRect = new Rectangle(drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height + Constants.GAME_SCREEN_HEIGHT);

            spriteBatch.Draw(_texture, drawPosition, drawRect, Color.White);

            // Invert the pipe
            spriteBatch.Draw(_texture, new Vector2(drawPosition.X + _texture.Width, drawPosition.Y - Constants.PIPE_VERTICAL_DISTANCE),
                _invertedDrawRect, 
                Color.White, (float)Math.PI, Vector2.Zero, 1.0f, SpriteEffects.None, 1);
        }

        public override void Update(GameTime gameTime)
        {
            Position -= new Vector2(Constants.SCROLL_SPEED, 0);
            if (Position.X < -_texture.Width)
            {
                GeneratePipeHeight(_texture);
                Position = new Vector2(_texture.Width * 2 + Constants.PIPE_HORIZONTAL_DISTANCE, Position.Y);
            }
            _pointBarrier.X = (int)Position.X + _texture.Width / 2;
        }

        public override bool DoesCollide(Rectangle rect)
        {
            Vector2 drawPosition = new Vector2(Position.X, Position.Y + pipeHeight);

            Rectangle r = new Rectangle((int)drawPosition.X, (int)drawPosition.Y, _texture.Width, _texture.Height - pipeHeight);
            // Not sure why it's -5, but it works
            Rectangle invertedR = new Rectangle((int)drawPosition.X, 0,
                drawRect.Width, _texture.Height - Math.Abs(pipeHeight - _texture.Height) + 13);
            return r.Intersects(rect) || invertedR.Intersects(rect);
        }

        // Check if the bird crosses the point line 
        public bool DoesScorePoint(Bird bird)
        {
            return bird.drawRect.Intersects(_pointBarrier);
        }
    }
}
