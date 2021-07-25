using FlappyBird.Core;
using FlappyBird.Managers;
using FlappyBird.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.Sprites
{
    class Sprite : Component
    {
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        protected Vector2 _position;
        protected Texture2D _texture;
        public Rectangle drawRect;
        protected float _rotation;
       
        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                if (_animationManager != null)
                {
                    _animationManager.Position = _position;
                }
            }

        }
        public float Speed = Constants.GRAVITY;
        public Vector2 Velocity;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, drawRect, Color.White);
            }
            else if (_animationManager != null)
                _animationManager.Draw(spriteBatch, _rotation);
            else throw new Exception("Neither texture nor animationManager have values");
        }

        public Sprite(Dictionary<string, Animation> animations)
        {
            _animations = animations;
            Animation idleAnimation = animations["Idle"];
            drawRect = new Rectangle((int)Position.X, (int)Position.Y, idleAnimation.FrameWidth, idleAnimation.FrameHeight);
            _animationManager = new AnimationManager(_animations.First().Value);
            _rotation = 0f;
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            drawRect = new Rectangle((int) Position.X, (int) Position.Y, _texture.Width, _texture.Height);
            _rotation = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            if (_animationManager != null)
                _animationManager.Update(gameTime);
            
            drawRect.X = (int)Position.X;
            drawRect.Y = (int)Position.Y;
        }

        public virtual bool DoesCollide(Sprite sprite) {
            return drawRect.Intersects(sprite.drawRect);
        }

        // override with custom rectangle
        public virtual bool DoesCollide(Rectangle rect)
        {
            return drawRect.Intersects(rect);
        }
    }
}
