using FlappyBird.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.States
{
    public abstract class State
    {
        #region Fields
        protected ContentManager _content;
        protected Game1 _game;
        protected GraphicsDevice _graphicsDevice;
        protected RenderTarget2D _renderTarget;
        protected List<Component> _components;
        #endregion

        #region Methods
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            _graphicsDevice.SetRenderTarget(_renderTarget);

            _graphicsDevice.Clear(Color.CornflowerBlue);
            // Draw on Rendertarget
            spriteBatch.Begin();
            foreach (Component c in _components)
                c.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            _graphicsDevice.SetRenderTarget(null);
            // Draw rendertarget on screen
            _graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(_renderTarget, Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
        }
        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            // prevent null when iterating through list
            _components = new List<Component>();
        }
        public abstract void Update(GameTime gameTime);
        #endregion
    }
}
