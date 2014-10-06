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
using System.Drawing;
using System.Windows.Forms;

namespace Tetris_basic
{
    public class O_Piece : Piece
    {
        public O_Piece(Orientation orientationParam)
        {
            orientation = orientationParam;
            color = Color.Green;

            leftBound = 0;
            rightBound = 8;
            bottomBound = 19;
        }

        public override void Draw(PaintEventArgs e, int x, int y, int width, int height)
        {
            Utility.DrawCell(e, x, y - 1 * height, width, height, color);
            Utility.DrawCell(e, x + 1 * width, y - 1 * height, width, height, color);
            Utility.DrawCell(e, x, y, width, height, color);
            Utility.DrawCell(e, x + 1 * width, y, width, height, color);
        }

        public override void MarkFinalPosition(bool[][] filledCells, Color[][] colorOfCells, int x, int y)
        {
             if (y > 0)
            {
                colorOfCells[x][y - 1] = color;
                filledCells[x][y - 1] = true;

                colorOfCells[x + 1][y - 1] = color;
                filledCells[x + 1][y - 1] = true;
            }

            colorOfCells[x][y] = color;
            filledCells[x][y] = true;

            colorOfCells[x + 1][y] = color;
            filledCells[x + 1][y] = true;
        }

        public override bool IsObstructedForBottomMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;

            if ((filledCells[x][y + 1] == true) ||
            (filledCells[x + 1][y + 1] == true))
            {
                isObstructed = true;
            }

            return isObstructed;
        }

        public override bool IsObstructedForLeftMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;
            if (x > leftBound)
            {
                if (((y > 0) && (filledCells[x - 1][y - 1] == true)) ||
                ((y >= 0) && (filledCells[x - 1][y] == true)))
                {
                    isObstructed = true;
                }
            }

            return isObstructed;
        }

        public override bool IsObstructedForRightMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;
            if (x <= (rightBound - 2))
            {
                if ( ((y > 0) && (filledCells[x + 2][y - 1] == true)) ||
                ((y >= 0) && (filledCells[x + 2][y] == true)) )
                {
                    isObstructed = true;
                }
            }

            return isObstructed;
        }
    }
}
