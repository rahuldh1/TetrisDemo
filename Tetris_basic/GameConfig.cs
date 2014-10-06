/*
Copyright 2014 Rahul Dharmadhikari

This file is part of Tetris_basic.

Tetris_basic is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License 
as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Tetris_basic is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty 
of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Tetris_basic. If not, see http://www.gnu.org/licenses/.
 */

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
        public long Score { get; set; }
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
