using FlappyBird.Controls;
using FlappyBird.Core;
using FlappyBird.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace FlappyBird.States
{
    class MenuState : State
    {

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            game.IsMouseVisible = true;
            _graphicsDevice = graphicsDevice;
            graphicsDevice.PresentationParameters.BackBufferWidth  = Constants.GAME_SCREEN_WIDTH;
            graphicsDevice.PresentationParameters.BackBufferHeight = Constants.GAME_SCREEN_HEIGHT;
            _renderTarget = new RenderTarget2D(graphicsDevice,
                                               Constants.GAME_SCREEN_WIDTH,
                                               Constants.GAME_SCREEN_HEIGHT);

            #region Buttons
            Texture2D buttonTexture = _content.Load<Texture2D>("Button");
            SpriteFont buttonFont    = _content.Load<SpriteFont>("Font");
            Button newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(85, 185),
                Text = "Play"
            };
            newGameButton.Click += NewGameButton_Click;

            Button quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(85, 245),
                Text = "Quit"
            };
            quitGameButton.Click += QuitGameButton_Click;
            #endregion
            #region Assets
            Texture2D backgroundDay = content.Load<Texture2D>("background-day");
            Texture2D baseText = content.Load<Texture2D>("base");

            Component backgroundSprite = new Sprite(backgroundDay);
            Component baseSprite = new Scrolling(baseText)
            {
                Position = new Vector2(0, backgroundDay.Height - baseText.Height)
            };
            Component baseSprite2 = new Scrolling(baseText)
            {
                Position = new Vector2(baseText.Width, backgroundDay.Height - baseText.Height)
            };
            #endregion

            _components = new List<Component>()
            {

                backgroundSprite,
                baseSprite,
                baseSprite2,
                newGameButton,
                quitGameButton,
            };
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Component c in _components)
                c.Update(gameTime);
        }
    }
}
