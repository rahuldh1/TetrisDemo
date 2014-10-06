using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris_basic
{
    public class L_Piece : Piece
    {
        public L_Piece(Orientation orientationParam)
        {
            orientation = orientationParam;
            color = Color.BlueViolet;

            if (orientation == Orientation.VERTICAL_RIGHT)
            {
                leftBound = 0;
                rightBound = 8;
                bottomBound = 19;
            }
            else if (orientation == Orientation.VERTICAL_LEFT)
            {
                leftBound = 0;
                rightBound = 8;
                bottomBound = 19;
            }
            else if (orientation == Orientation.HORIZONTAL_DOWN)
            {
                leftBound = 0;
                rightBound = 7;
                bottomBound = 19;
            }
            else if (orientation == Orientation.HORIZONTAL_UP)
            {
                leftBound = 0;
                rightBound = 7;
                bottomBound = 19;
            }

        }

        public override void Draw(PaintEventArgs e, int x, int y, int width, int height)
        {
            if (orientation == Orientation.VERTICAL_RIGHT)
            {
                leftBound = 0;
                rightBound = 8;
                bottomBound = 19;

                Utility.DrawCell(e, x, y - 2 * height, width, height, color);
                Utility.DrawCell(e, x, y - 1 * height, width, height, color);
                Utility.DrawCell(e, x, y, width, height, color);
                Utility.DrawCell(e, x + 1 * width, y, width, height, color);
            }
            else if (orientation == Orientation.VERTICAL_LEFT)
            {
                leftBound = 0;
                rightBound = 8;
                bottomBound = 19;

                Utility.DrawCell(e, x, y - 2 * height, width, height, color);
                Utility.DrawCell(e, x + 1 * width, y - 2 * height, width, height, color);
                Utility.DrawCell(e, x + 1 * width, y - 1 * height, width, height, color);
                Utility.DrawCell(e, x + 1 * width, y, width, height, color);
            }
            else if (orientation == Orientation.HORIZONTAL_DOWN)
            {
                leftBound = 0;
                rightBound = 7;
                bottomBound = 19;

                Utility.DrawCell(e, x, y - 1 * height, width, height, color);
                Utility.DrawCell(e, x, y, width, height, color);
                Utility.DrawCell(e, x + 1 * width, y - 1 * height, width, height, color);
                Utility.DrawCell(e, x + 2 * width, y - 1 * height, width, height, color);
            }
            else if (orientation == Orientation.HORIZONTAL_UP)
            {
                leftBound = 0;
                rightBound = 7;
                bottomBound = 19;

                Utility.DrawCell(e, x, y, width, height, color);
                Utility.DrawCell(e, x + 1 * width, y, width, height, color);
                Utility.DrawCell(e, x + 2 * width, y, width, height, color);
                Utility.DrawCell(e, x + 2 * width, y - 1 * height, width, height, color);
            }
        }

        public override void MarkFinalPosition(bool[][] filledCells, Color[][] colorOfCells, int x, int y)
        {
            if (orientation == Orientation.VERTICAL_RIGHT)
            {
                if (y > 1)
                {
                    colorOfCells[x][y - 2] = color;
                    filledCells[x][y - 2] = true;
                }

                if (y > 0)
                {
                    colorOfCells[x][y - 1] = color;
                    filledCells[x][y - 1] = true;
                }

                colorOfCells[x][y] = color;
                filledCells[x][y] = true;

                colorOfCells[x + 1][y] = color;
                filledCells[x + 1][y] = true;
            }
            else if (orientation == Orientation.VERTICAL_LEFT)
            {
                if (y > 1)
                {
                    colorOfCells[x][y - 2] = color;
                    filledCells[x][y - 2] = true;

                    colorOfCells[x + 1][y - 2] = color;
                    filledCells[x + 1][y - 2] = true;
                }

                if (y > 0)
                {
                    colorOfCells[x + 1][y - 1] = color;
                    filledCells[x + 1][y - 1] = true;
                }

                colorOfCells[x + 1][y] = color;
                filledCells[x + 1][y] = true;
            }
            else if (orientation == Orientation.HORIZONTAL_DOWN)
            {
                if (y > 0)
                {
                    colorOfCells[x][y - 1] = color;
                    filledCells[x][y - 1] = true;

                    colorOfCells[x + 1][y - 1] = color;
                    filledCells[x + 1][y - 1] = true;

                    colorOfCells[x + 2][y - 1] = color;
                    filledCells[x + 2][y - 1] = true;
                }

                colorOfCells[x][y] = color;
                filledCells[x][y] = true;
            }
            else if (orientation == Orientation.HORIZONTAL_UP)
            {
                colorOfCells[x][y] = color;
                filledCells[x][y] = true;

                colorOfCells[x + 1][y] = color;
                filledCells[x + 1][y] = true;

                colorOfCells[x + 2][y] = color;
                filledCells[x + 2][y] = true;

                if (y > 0)
                {
                    colorOfCells[x + 2][y - 1] = color;
                    filledCells[x + 2][y - 1] = true;
                }
            }
        }

        public override bool IsObstructedForBottomMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;

            if (orientation == Orientation.VERTICAL_RIGHT)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y + 1] == true))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.VERTICAL_LEFT)
            {
                if ((filledCells[x + 1][y + 1] == true) ||
                ((y > 0) && (filledCells[x][y - 1] == true)))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.HORIZONTAL_DOWN)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y] == true) ||
                (filledCells[x + 2][y] == true))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.HORIZONTAL_UP)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y + 1] == true) ||
                (filledCells[x + 2][y + 1] == true))
                {
                    isObstructed = true;
                }
            }

            return isObstructed;
        }

        public override bool IsObstructedForLeftMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;

            if (orientation == Orientation.VERTICAL_RIGHT)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y + 1] == true))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.VERTICAL_LEFT)
            {
                if ((filledCells[x + 1][y + 1] == true) ||
                ((y > 0) && (filledCells[x][y - 1] == true)))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.HORIZONTAL_DOWN)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y] == true) ||
                (filledCells[x + 2][y] == true))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.HORIZONTAL_UP)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y + 1] == true) ||
                (filledCells[x + 2][y + 1] == true))
                {
                    isObstructed = true;
                }
            }

            return isObstructed;
        }

        public override bool IsObstructedForRightMovement(bool[][] filledCells, int x, int y)
        {
            bool isObstructed = false;

            if (orientation == Orientation.VERTICAL_RIGHT)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y + 1] == true))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.VERTICAL_LEFT)
            {
                if ((filledCells[x + 1][y + 1] == true) ||
                ((y > 0) && (filledCells[x][y - 1] == true)))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.HORIZONTAL_DOWN)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y] == true) ||
                (filledCells[x + 2][y] == true))
                {
                    isObstructed = true;
                }
            }
            else if (orientation == Orientation.HORIZONTAL_UP)
            {
                if ((filledCells[x][y + 1] == true) ||
                (filledCells[x + 1][y + 1] == true) ||
                (filledCells[x + 2][y + 1] == true))
                {
                    isObstructed = true;
                }
            }

            return isObstructed;
        }
    }
}


