using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris_basic
{
    public class I_Piece: Piece
    {
        public I_Piece(Orientation orientationParam)
        {
            orientation = orientationParam;
            color = Color.Red;

            if ((orientation == Orientation.HORIZONTAL_UP) || (orientation == Orientation.HORIZONTAL_DOWN))
            {
                leftBound = 1;
                rightBound = 7;
                bottomBound = 19;
            }
            else 
            {
                leftBound = 0;
                rightBound = 9;
                bottomBound = 19;
            }
        }

        public override void Draw(PaintEventArgs e, int x, int y, int width, int height)
        {
            if ((orientation == Orientation.HORIZONTAL_UP) || (orientation == Orientation.HORIZONTAL_DOWN))
            {
                leftBound = 1;
                rightBound = 7;
                bottomBound = 19;

                Utility.DrawCell(e, x, y, width, height, color);
                Utility.DrawCell(e, x - 1 * width, y, width, height, color);
                Utility.DrawCell(e, x + 1 * width, y, width, height, color);
                Utility.DrawCell(e, x + 2 * width, y, width, height, color);
            }
            else
            {
                leftBound = 0;
                rightBound = 9;
                bottomBound = 19;

                Utility.DrawCell(e, x, y - 2 * height, width, height, color);
                Utility.DrawCell(e, x, y - 3 * height, width, height, color);
                Utility.DrawCell(e, x, y - 1 * height, width, height, color);
                Utility.DrawCell(e, x, y, width, height, color);
            }
        }

        public override void MarkFinalPosition(bool[][] filledCells, Color[][] colorOfCells, int x, int y) 
        {
            if ((orientation == Orientation.HORIZONTAL_UP) || (orientation == Orientation.HORIZONTAL_DOWN))
            {
                colorOfCells[x][y] = color;
                filledCells[x][y] = true;

                colorOfCells[x - 1][y] = color;
                filledCells[x - 1][y] = true;

                colorOfCells[x + 1][y] = color;
                filledCells[x + 1][y] = true;

                colorOfCells[x + 2][y] = color;
                filledCells[x + 2][y] = true;
            }
            else
            {
                if (y > 1)
                {
                    colorOfCells[x][y - 2] = color;
                    filledCells[x][y - 2] = true;
                }

                if (y > 2)
                {
                    colorOfCells[x][y - 3] = color;
                    filledCells[x][y - 3] = true;
                }

                if (y > 0)
                {
                    colorOfCells[x][y - 1] = color;
                    filledCells[x][y - 1] = true;
                }

                colorOfCells[x][y] = color;
                filledCells[x][y] = true;
            }
        }

        public override bool IsObstructedForBottomMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;
            if ((orientation == Orientation.HORIZONTAL_UP) || (orientation == Orientation.HORIZONTAL_DOWN))
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x - 1][y + 1] == true) ||
                (filledCells[x + 1][y + 1] == true) ||
                (filledCells[x + 2][y + 1] == true))
                {
                    isObstructed = true;
                }
            }
            else
            {
                if (filledCells[x][y + 1] == true)
                    isObstructed = true;
            }
            return isObstructed;
        }

        public override bool IsObstructedForLeftMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;

            if ((orientation == Orientation.HORIZONTAL_UP) || (orientation == Orientation.HORIZONTAL_DOWN))
            {
                if ((x - 1) > leftBound)
                {
                    if ((y >= 0) && (filledCells[x - 2][y] == true))
                    {
                        isObstructed = true;
                    }
                }
            }
            else 
            {
                if (x > leftBound)
                {
                    if (((y > 2) && (filledCells[x - 1][y - 3] == true)) ||
                        ((y > 1) && (filledCells[x - 1][y - 2] == true)) ||
                        ((y > 0) && (filledCells[x - 1][y - 1] == true)) ||
                        ((y >= 0) && (filledCells[x - 1][y] == true)))
                    {
                        isObstructed = true;
                    }
                }
            }

            return isObstructed;
        }

        public override bool IsObstructedForRightMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;

            if ((orientation == Orientation.HORIZONTAL_UP) || (orientation == Orientation.HORIZONTAL_DOWN))
            {
                if (x <= (rightBound - 3))
                {
                    if ((y >= 0) && (filledCells[x + 3][y] == true))
                    {
                        isObstructed = true;
                    }
                }
            }
            else
            {
                if (x <= (rightBound - 1))
                {
                    if (((y > 2) && (filledCells[x + 1][y - 3] == true)) || 
                        ((y > 1) && (filledCells[x + 1][y - 2] == true)) || 
                        ((y > 0) && (filledCells[x + 1][y - 1] == true)) ||
                        ((y >= 0) && (filledCells[x + 1][y] == true)))
                    {
                        isObstructed = true;
                    }
                }
            }

            return isObstructed;
        }
    }
}
