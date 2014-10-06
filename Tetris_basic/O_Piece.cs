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
