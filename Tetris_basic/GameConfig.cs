using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_basic
{
    public class GameConfig
    {
        public const int START_X = 4;
        public const int START_Y = 0;

        public const int X_COORD = 30;
        public const int Y_COORD = 65;

        public const int X_WIDTH = 200;
        public const int Y_WIDTH = 340;
        public const int X_DIVS = 10;
        public const int Y_DIVS = 20;

        public const int PIECE_COUNT = 5;
        public const int ORIENTATION_COUNT = 4;

        public int Xcoord { get; set; }
        public int Ycoord { get; set; }
        public int Score { get; set; }
        public int Level { get; set; }

        public GameConfig()
        {
            Xcoord = START_X;
            Ycoord = START_Y;            
            Score = 0;
            Level = 0;
        }

        public void ResetGameConfig()
        {
            Xcoord = START_X;
            Ycoord = START_Y;
            Score = 0;
            Level = 0;
        }
    }
}
