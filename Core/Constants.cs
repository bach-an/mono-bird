using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Core
{
    public static class Constants
    {
        public static readonly int GAME_SCREEN_WIDTH = 576;
        public static readonly int GAME_SCREEN_HEIGHT = 1024;
        public static readonly float GRAVITY = 15f;
        public static readonly float JUMP_SPEED = -3.5f;
        public static readonly int SCROLL_SPEED = 1;
        public static readonly int PIPE_VERTICAL_DISTANCE = 65;
        public static readonly int PIPE_HORIZONTAL_DISTANCE = 185;
        public static readonly int PIPE_START = 3 * GAME_SCREEN_WIDTH / 4;
        public static readonly int COLLISION_BUFFER = 4;
        public static readonly float JUMP_ROTATION = -(float)Math.PI / 10;
        public static readonly int FALL_DELAY = 30;
        public static readonly float FALL_ROTATION = 0.3f;
        public static readonly int POINT_DELAY = 60;

    }
}
