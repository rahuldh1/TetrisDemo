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
