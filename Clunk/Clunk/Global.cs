using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using Clunk.Entities;
using Clunk.Util;


namespace Clunk
{
    public class Global
    {
        public static Game CLUNK = null;
        public static Session PlayerSession;
        public static Player player = null;
        public static CameraShaker camShaker = new CameraShaker();
        public static bool paused = false;
        public static Music gameMusic = null;
        public const int DIR_UP = 0;
        public const int DIR_DOWN = 1;
        public const int DIR_LEFT = 2;
        public const int DIR_RIGHT = 3;
        // These variables will be used when creating our Tilemap related objects
        public const int GAME_WIDTH = 640;
        public const int GAME_HEIGHT = 480;
        public const int GRID_WIDTH = 32;
        public const int GRID_HEIGHT = 32;
        public enum Type
        {
            BULLET,
            ENEMY
        }
        

    }
}
