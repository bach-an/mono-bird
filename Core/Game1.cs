using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FlappyBird.Sprites;
using FlappyBird.Models;
using FlappyBird.Managers;
using System.Collections.Generic;

using System.Diagnostics;
using FlappyBird.States;

namespace FlappyBird.Core
{

    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        private State _currentState;
        private State _nextState;
        private State _savedState;
        private RenderTarget2D _renderTarget;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public void SetSaveState(State state)
        {
            _savedState = state;
        }

        public void LoadState()
        {
            _currentState = _savedState;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            IsMouseVisible = true;
            _graphics.ApplyChanges();
            base.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if(_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            _currentState.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }
    }
}
