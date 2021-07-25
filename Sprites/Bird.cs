using FlappyBird.Core;
using FlappyBird.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlappyBird.Sprites
{
    class Bird : Sprite
    {
        // MouseState cache to prevent button holding
        private MouseState oldState;
        public Bird(Dictionary<string, Animation> animations) : base(animations) {}
        private int _fallDelayCount = 0;
        public int GameScore = 0;
        private bool gameStart = false;
        private int _pointDelayCounter = 0;
        public List<Texture2D> ListNumbers = new List<Texture2D>();
        public override void Update(GameTime gameTime)
        {

            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && oldState.LeftButton != ButtonState.Pressed)
            {
                gameStart = true;
                Velocity.Y = Constants.JUMP_SPEED;
                _rotation = Constants.JUMP_ROTATION;
                _fallDelayCount = 0;
            }
            else if (_rotation <= -Constants.JUMP_ROTATION && _fallDelayCount >= Constants.FALL_DELAY)
            {
                _rotation += Constants.FALL_ROTATION;
            }

            if (gameStart)
            {
                _fallDelayCount += 1;
                Position += Velocity;
                Velocity.Y += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                oldState = mouseState;
            }
            base.Update(gameTime);
            _pointDelayCounter++;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            String points = GameScore.ToString();
            int charXPos = 130;

            foreach (char c in points)
            {
                Texture2D charText = GetNumberFromChar(c);
                spriteBatch.Draw(charText, new Vector2(charXPos, 0), Color.White);
                charXPos += charText.Width + 2;
            }

        }

        private Texture2D GetNumberFromChar(char c)
        {
            return c switch
            {
                '0' => ListNumbers[0],
                '1' => ListNumbers[1],
                '2' => ListNumbers[2],
                '3' => ListNumbers[3],
                '4' => ListNumbers[4],
                '5' => ListNumbers[5],
                '6' => ListNumbers[6],
                '7' => ListNumbers[7],
                '8' => ListNumbers[8],
                '9' => ListNumbers[9],
                _   => throw new Exception("Incorrect char")
            };
        }

        // Add a point, but only if a point has not been scored in Constants.POINT_DELAY amount of time
        public void AddPoint()
        {
            if(_pointDelayCounter >= Constants.POINT_DELAY)
            {
                GameScore++;
                _pointDelayCounter = 0;
            }
        }
    }
}
