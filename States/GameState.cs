using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FlappyBird.Core;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using FlappyBird.Sprites;
using FlappyBird.Models;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird.States
{
    class GameState : State
    {
        Texture2D backgroundText;
        Texture2D bird;
        Texture2D pipe;
        Texture2D baseText;
        Texture2D zeroText;
        Texture2D oneText;
        Texture2D twoText;
        Texture2D threeText;
        Texture2D fourText;
        Texture2D fiveText;
        Texture2D sixText;
        Texture2D sevenText;
        Texture2D eightText;
        Texture2D nineText;

        Component backgroundSprite;
        Component baseSprite;
        Component baseSprite2;
        Component birdSprite;
        Component pipeSprite;
        Component pipeSprite2;

        Animation birdAnimation;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            game.IsMouseVisible = false;
            _graphicsDevice = graphicsDevice;
            graphicsDevice.PresentationParameters.BackBufferWidth = Constants.GAME_SCREEN_WIDTH;
            graphicsDevice.PresentationParameters.BackBufferHeight = Constants.GAME_SCREEN_HEIGHT;
            _renderTarget = new RenderTarget2D(graphicsDevice,
                                               Constants.GAME_SCREEN_WIDTH,
                                               Constants.GAME_SCREEN_HEIGHT);

            #region Assets
            backgroundText = content.Load<Texture2D>("background-day");
            bird           = content.Load<Texture2D>("bluebird_spritesheet");
            pipe           = content.Load<Texture2D>("pipe-green");
            birdAnimation = new Animation(bird, 3);
            baseText  = content.Load<Texture2D>("base");
            zeroText  = content.Load<Texture2D>("0");
            oneText   = content.Load<Texture2D>("1");
            twoText   = content.Load<Texture2D>("2");
            threeText = content.Load<Texture2D>("3");
            fourText  = content.Load<Texture2D>("4");
            fiveText  = content.Load<Texture2D>("5");
            sixText   = content.Load<Texture2D>("6");
            sevenText = content.Load<Texture2D>("7");
            eightText = content.Load<Texture2D>("8");
            nineText = content.Load<Texture2D>("9");
            List<Texture2D> listNumbers = new List<Texture2D>() { zeroText, oneText, twoText, threeText, fourText, fiveText, sixText, sevenText, eightText, nineText };

            
            backgroundSprite = new Sprite(backgroundText);
            baseSprite = new Scrolling(baseText)
            {
                Position = new Vector2(0, backgroundText.Height - content.Load<Texture2D>("base").Height)
            };
            baseSprite2 = new Scrolling(baseText)
            {
                Position = new Vector2(baseText.Width, backgroundText.Height - baseText.Height)
            };
            birdSprite = new Bird(new Dictionary<string, Animation>()
                { {"Idle", birdAnimation } })
            {
                Position = new Vector2(backgroundText.Width / 3, backgroundText.Height / 2),
                Velocity = new Vector2(0, 1),
                ListNumbers = new List<Texture2D>() { zeroText, oneText, twoText, threeText, fourText, fiveText, sixText, sevenText, eightText, nineText }
            };
            pipeSprite = new Pipe(pipe)
            {
                Position = new Vector2(Constants.PIPE_START, backgroundText.Height - baseText.Height - pipe.Height)
            };
            pipeSprite2 = new Pipe(pipe)
            {
                Position = new Vector2(Constants.PIPE_START + Constants.PIPE_HORIZONTAL_DISTANCE, backgroundText.Height - baseText.Height - pipe.Height)
            };

            #endregion

            _components = new List<Component>()
            {
                backgroundSprite, baseSprite, baseSprite2, pipeSprite, pipeSprite2, birdSprite
            };
        }
    

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                _game.SetSaveState(this);
                _game.ChangeState(new PauseState(_game, _graphicsDevice, _content));
            }

            foreach (Component c in _components)
                c.Update(gameTime);

            Bird b = (Bird)birdSprite;
            Rectangle birdRect = b.drawRect;
            // Make the collision less harsh
            birdRect.X -= birdAnimation.FrameWidth / 2 - Constants.COLLISION_BUFFER / 2;
            birdRect.Y -= birdAnimation.FrameHeight / 2 - Constants.COLLISION_BUFFER / 2;
            birdRect.Width -= Constants.COLLISION_BUFFER;
            birdRect.Height -= Constants.COLLISION_BUFFER;

            Pipe p  = (Pipe)pipeSprite;
            Pipe p2 = (Pipe)pipeSprite2;
            Scrolling scrollingBase = (Scrolling)baseSprite;
            Scrolling scrollingBase2 = (Scrolling)baseSprite2;
            if (p.DoesCollide(birdRect) || p2.DoesCollide(birdRect) ||
                birdRect.Intersects(scrollingBase.drawRect) || birdRect.Intersects(scrollingBase2.drawRect))
                _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));

            else if (p.DoesScorePoint(b) || p2.DoesScorePoint(b))
            {
                b.AddPoint();
            }
        }
    }
}