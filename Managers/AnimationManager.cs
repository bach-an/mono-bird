using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlappyBird.Models;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Managers
{
    // Always keeps track of one animation at a time
    class AnimationManager
    {
        private Animation _animation;
        private float _timer; // to know when to increment the current frame
        public Vector2 Position { get; set; }

        public AnimationManager(Animation animation)
        {
            _animation = animation;
        }

        public void Draw(SpriteBatch spriteBatch, float rotation)
        {
            spriteBatch.Draw(_animation.Texture, Position, 
                // Destination texture -- what part of the texture we're viewing
                new Rectangle(_animation.CurrentFrame * _animation.FrameWidth, 0, _animation.FrameWidth, _animation.FrameHeight), 
                    Color.White, rotation, new Vector2(_animation.FrameWidth / 2, _animation.FrameHeight / 2), 1.0f, SpriteEffects.None, 1);
        }
         
        public void Play(Animation animation)
        {
            // if trying to play current animation
            if (_animation == animation)
            {
                return;
            }
            _animation = animation;
            _animation.CurrentFrame = 0;
            _timer = 0;
        }

        public void Stop()
        {
            _timer = 0f;
            _animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0f;
                _animation.CurrentFrame++;
                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }
    }
}
