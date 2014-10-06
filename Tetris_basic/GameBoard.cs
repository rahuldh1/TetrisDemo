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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Tetris_basic
{
    public partial class GameBoard : Form
    {
        public Piece gamePiece { get { return piece; } }

        private Color[][] colorOfCells;
        private bool[][] filledCells;
        private GameConfig gameConfig;
        private Piece piece;
        private Piece nextPiece;
        private Font scoreFont;
        private SolidBrush scoreBrush;
        private PointF scorePoint;
        private PointF levelPoint;
        private SolidBrush boundaryBrush;

        private Pen penBoundary;
        private Pen nextPiecePenBoundary;
        private Rectangle rectBoundary;
        private Rectangle outerRectBoundary;
        private Rectangle nextPieceRectBoundary;

        public GameBoard(GameConfig gameConfigParam)
        {
            InitializeComponent();

            int width = GameConfig.X_WIDTH / GameConfig.X_DIVS;
            int height = GameConfig.Y_WIDTH / GameConfig.Y_DIVS;

            gameConfig = gameConfigParam;
            Type type = (Tetris_basic.Type)Utility.GetRandomNumber(0, GameConfig.PIECE_COUNT);
            Orientation orientation = (Tetris_basic.Orientation)Utility.GetRandomNumber(0, GameConfig.ORIENTATION_COUNT);
            piece = CreatePiece(type, orientation);

            Type nextPieceType = (Tetris_basic.Type)Utility.GetRandomNumber(0, GameConfig.PIECE_COUNT);
            Orientation nextPieceOrientation = (Tetris_basic.Orientation)Utility.GetRandomNumber(0, GameConfig.ORIENTATION_COUNT);
            nextPiece = CreatePiece(nextPieceType, nextPieceOrientation);

            //Initialize all cells with the background color
            colorOfCells = new Color[GameConfig.X_DIVS][];
            filledCells = new bool[GameConfig.X_DIVS][];

            for (int x = 0; x < GameConfig.X_DIVS; x++)
            {
                colorOfCells[x] = new Color[GameConfig.Y_DIVS];
                filledCells[x] = new bool[GameConfig.Y_DIVS];
            }

            for (int x = 0; x < GameConfig.X_DIVS; x++)
            {
                for (int y = 0; y < GameConfig.Y_DIVS; y++)
                {
                    colorOfCells[x][y] = Color.DeepSkyBlue;
                    filledCells[x][y] = false;
                }
            }

            scoreFont = new Font("Arial", 14, FontStyle.Bold);
            scoreBrush = new SolidBrush(Color.Red);
            scorePoint = new PointF(GameConfig.X_COORD + GameConfig.X_WIDTH + width * 1 + 3, GameConfig.Y_COORD + height * 8);
            levelPoint = new PointF(GameConfig.X_COORD + GameConfig.X_WIDTH + width * 1 + 3, GameConfig.Y_COORD + height * 10);

            boundaryBrush = new SolidBrush(Color.OrangeRed);
            penBoundary = new Pen(boundaryBrush, 2.0F);
            rectBoundary = new Rectangle(GameConfig.X_COORD - 1, GameConfig.Y_COORD - 1, GameConfig.X_WIDTH + 2, GameConfig.Y_WIDTH + 2);
            outerRectBoundary = new Rectangle(GameConfig.X_COORD - 6, GameConfig.Y_COORD - 6, GameConfig.X_WIDTH + 12, GameConfig.Y_WIDTH + 12);

            nextPiecePenBoundary = new Pen(boundaryBrush, 4.0F);
            nextPieceRectBoundary = new Rectangle(GameConfig.X_COORD + GameConfig.X_WIDTH + width * 1 + 2,
                GameConfig.Y_COORD + 2,
                width * 6 - 4, 
                height * 6 - 4);
        }

        private void OnTimer(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int width = GameConfig.X_WIDTH / GameConfig.X_DIVS;
            int height = GameConfig.Y_WIDTH / GameConfig.Y_DIVS;

            // Draw game's grid
            for (int x = GameConfig.X_COORD, p = 0; x < GameConfig.X_COORD + GameConfig.X_WIDTH; x += width, p++)
                for (int y = GameConfig.Y_COORD, q = 0; y < GameConfig.Y_COORD + GameConfig.Y_WIDTH; y += height, q++)
                    Utility.DrawCell(e, x, y, width, height, colorOfCells[p][q]);

            e.Graphics.DrawRectangle(penBoundary, rectBoundary);
            e.Graphics.DrawRectangle(nextPiecePenBoundary, outerRectBoundary);

            // Draw current piece
            piece.Draw(e, GameConfig.X_COORD + gameConfig.Xcoord * width, GameConfig.Y_COORD + gameConfig.Ycoord * height,
                width, height);



            //Draw next piece's grid
            for (int x = GameConfig.X_COORD + GameConfig.X_WIDTH + width * 1; x < GameConfig.X_COORD + GameConfig.X_WIDTH + width * 7; x += width)
                for (int y = GameConfig.Y_COORD; y < GameConfig.Y_COORD + height * 6; y += height)
                    Utility.DrawCell(e, x, y, width, height, Color.DeepSkyBlue);

            e.Graphics.DrawRectangle(nextPiecePenBoundary, nextPieceRectBoundary);

            // Draw next piece
            nextPiece.Draw(e, GameConfig.X_COORD + GameConfig.X_WIDTH + width * 3, GameConfig.Y_COORD + height * 4,
                width, height);

            // Draw score
            String drawScore = "Score : " + gameConfig.Score.ToString();
            e.Graphics.DrawString(drawScore, scoreFont, scoreBrush, scorePoint);

            // Increase level of game (and consequently speed) dynamically based on score
            if (gameConfig.Score <= 10000)
            {
                gameConfig.Level = gameConfig.Score / 1000;
                timer1.Interval = 350 - gameConfig.Level * 15;
            }

            String drawLevel = "Level : " + gameConfig.Level.ToString();
            e.Graphics.DrawString(drawLevel, scoreFont, scoreBrush, levelPoint);

            gameConfig.Ycoord++;

            if (gameConfig.Ycoord > piece.bottomBound || piece.IsObstructedForBottomMovement(filledCells, gameConfig.Xcoord, gameConfig.Ycoord - 1))
            {
                piece.MarkFinalPosition(filledCells, colorOfCells, gameConfig.Xcoord, gameConfig.Ycoord - 1);

                for (int k = 0; k < GameConfig.Y_DIVS; k++)
                {
                    // Check if any row is filled
                    bool anyRowFilled = true;
                    for (int x = 0; x < GameConfig.X_DIVS; x++)
                    {
                        if (filledCells[x][k] == false)
                        {
                            anyRowFilled = false;
                            break;
                        }
                    }

                    if (anyRowFilled)
                    {
                        // Remove the filled line by shifting everything one row down
                        for (int x = 0; x < GameConfig.X_DIVS; x++)
                        {
                            for (int y = k; y > 0; y--)
                            {
                                if (y == 0)
                                {
                                    colorOfCells[x][y] = Color.DeepSkyBlue;
                                    filledCells[x][y] = false;
                                }
                                else
                                {
                                    colorOfCells[x][y] = colorOfCells[x][y - 1];
                                    filledCells[x][y] = filledCells[x][y - 1];
                                }
                            }
                        }

                        // Increase the score by 10
                        gameConfig.Score += 10;
                    }

                }


                // Check if any stack has touched till top row
                bool topRowTouched = false;
                for (int x = 0; x < GameConfig.X_DIVS; x++)
                {
                    if (filledCells[x][0] == true)
                    {
                        topRowTouched = true;
                        break;
                    }
                }
                if (topRowTouched)
                {
                    // GAME OVER. Stop the painting timer.
                    timer1.Stop();

                    String drawString = "GAME OVER !!";
                    Font drawFont = new Font("Arial", 40, FontStyle.Bold);
                    SolidBrush drawBrush = new SolidBrush(Color.DarkRed);
                    PointF drawPoint = new PointF(0.0F, 85.0F);
                    e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);
                }

                piece = nextPiece;
                Type nextPieceType = (Tetris_basic.Type)Utility.GetRandomNumber(0, GameConfig.PIECE_COUNT);
                Orientation nextPieceOrientation = (Tetris_basic.Orientation)Utility.GetRandomNumber(0, GameConfig.ORIENTATION_COUNT);
                nextPiece = CreatePiece(nextPieceType, nextPieceOrientation);

                gameConfig.Xcoord = GameConfig.START_X;
                gameConfig.Ycoord = GameConfig.START_Y;
            }

        }

        public void DropPieceToBottom()
        {
            while (true)
            {
                gameConfig.Ycoord++;

                if (gameConfig.Ycoord > piece.bottomBound || piece.IsObstructedForBottomMovement(filledCells, gameConfig.Xcoord, gameConfig.Ycoord - 1))
                {
                    piece.MarkFinalPosition(filledCells, colorOfCells, gameConfig.Xcoord, gameConfig.Ycoord - 1);

                    for (int k = 0; k < GameConfig.Y_DIVS; k++)
                    {
                        // Check if any row is filled
                        bool anyRowFilled = true;
                        for (int x = 0; x < GameConfig.X_DIVS; x++)
                        {
                            if (filledCells[x][k] == false)
                            {
                                anyRowFilled = false;
                                break;
                            }
                        }

                        if (anyRowFilled)
                        {
                            // Remove the filled line by shifting everything one row down
                            for (int x = 0; x < GameConfig.X_DIVS; x++)
                            {
                                for (int y = k; y > 0; y--)
                                {
                                    if (y == 0)
                                    {
                                        colorOfCells[x][y] = Color.DeepSkyBlue;
                                        filledCells[x][y] = false;
                                    }
                                    else
                                    {
                                        colorOfCells[x][y] = colorOfCells[x][y - 1];
                                        filledCells[x][y] = filledCells[x][y - 1];
                                    }
                                }
                            }

                            // Increase the score by 10
                            gameConfig.Score += 10;
                        }

                    }

                    piece = nextPiece;
                    Type nextPieceType = (Tetris_basic.Type)Utility.GetRandomNumber(0, GameConfig.PIECE_COUNT);
                    Orientation nextPieceOrientation = (Tetris_basic.Orientation)Utility.GetRandomNumber(0, GameConfig.ORIENTATION_COUNT);
                    nextPiece = CreatePiece(nextPieceType, nextPieceOrientation);

                    gameConfig.Xcoord = GameConfig.START_X;
                    gameConfig.Ycoord = GameConfig.START_Y;

                    break; // break from while loop
                }
            }// while loop
        }

        public bool IsLeftPossible()
        {
            return !(piece.IsObstructedForLeftMovement(filledCells, gameConfig.Xcoord, gameConfig.Ycoord - 1));
        }

        public bool IsRightPossible()
        {
            return !(piece.IsObstructedForRightMovement(filledCells, gameConfig.Xcoord, gameConfig.Ycoord - 1));
        }

        public void ResetGameBoard()
        {
            Type type = (Tetris_basic.Type)Utility.GetRandomNumber(0, GameConfig.PIECE_COUNT);
            Orientation orientation = (Tetris_basic.Orientation)Utility.GetRandomNumber(0, GameConfig.ORIENTATION_COUNT);
            piece = CreatePiece(type, orientation);

            Type nextPieceType = (Tetris_basic.Type)Utility.GetRandomNumber(0, GameConfig.PIECE_COUNT);
            Orientation nextPieceOrientation = (Tetris_basic.Orientation)Utility.GetRandomNumber(0, GameConfig.ORIENTATION_COUNT);
            nextPiece = CreatePiece(nextPieceType, nextPieceOrientation);

            for (int x = 0; x < GameConfig.X_DIVS; x++)
            {
                colorOfCells[x] = new Color[GameConfig.Y_DIVS];
                filledCells[x] = new bool[GameConfig.Y_DIVS];
            }

            for (int x = 0; x < GameConfig.X_DIVS; x++)
            {
                for (int y = 0; y < GameConfig.Y_DIVS; y++)
                {
                    colorOfCells[x][y] = Color.DeepSkyBlue;
                    filledCells[x][y] = false;
                }
            }
        }

        private Piece CreatePiece(Type type, Orientation orientation)
        {
            Piece piece = null;
            switch (type)
            {
                case Type.I:
                    piece = new I_Piece(orientation);
                    break;

                case Type.O:
                    piece = new O_Piece(orientation);
                    break;

                case Type.L:
                    piece = new L_Piece(orientation);
                    break;

                case Type.J:
                    piece = new J_Piece(orientation);
                    break;

                case Type.T:
                    piece = new T_Piece(orientation);
                    break;

                default:
                    //Error
                    break;
            }

            return piece;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

    }
}
