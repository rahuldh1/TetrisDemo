using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris_basic
{
    public class Utility
    {
        private static Random random = new Random();

        public static int GetRandomNumber(int start, int end)
        {
            return random.Next(start, end);
        }

        public static void DrawCell(PaintEventArgs e, int x, int y, int width, int height, Color color)
        {
            SolidBrush brushFill = new SolidBrush(color);
            Rectangle rect = new Rectangle(x, y, width, height);
            e.Graphics.FillRectangle(brushFill, rect);

            SolidBrush brush = new SolidBrush(Color.Gray);
            Pen pen = new Pen(brush);
            e.Graphics.DrawRectangle(pen, rect);
        }
    }
}
