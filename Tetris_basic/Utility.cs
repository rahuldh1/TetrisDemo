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
