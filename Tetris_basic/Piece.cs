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
    public enum Type { I, L, O, J, T };
    public enum Orientation { HORIZONTAL_UP, VERTICAL_LEFT, HORIZONTAL_DOWN, VERTICAL_RIGHT };
    
    public abstract class Piece
    {
        public abstract void Draw(PaintEventArgs e, int x, int y, int width, int height);
        public abstract void MarkFinalPosition(bool[][] filledCells, Color[][] colorOfCells, int x, int y);
        public virtual bool IsObstructedForBottomMovement(bool[][] filledCells, int x, int y) { return false; }
        public virtual bool IsObstructedForLeftMovement(bool[][] filledCells, int x, int y) { return false; }
        public virtual bool IsObstructedForRightMovement(bool[][] filledCells, int x, int y) { return false; }

        public Type type { get; set; }
        public Orientation orientation { get; set; }
        public Color color { get; set; }
        public int leftBound { get; set; }
        public int rightBound { get; set; }
        public int bottomBound { get; set; }
    }
}
