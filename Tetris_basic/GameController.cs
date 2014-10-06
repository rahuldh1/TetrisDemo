﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace Tetris_basic
{
    public class GameController
    {
        private GameBoard gameBoard;
        private GameConfig gameConfig;

        public GameController(GameBoard viewParam, GameConfig gameconfigParam)
        {
            gameBoard = viewParam;
            gameConfig = gameconfigParam;
            gameBoard.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }
          
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                        if ((gameConfig.Xcoord > gameBoard.gamePiece.leftBound) &&
                            (gameBoard.IsLeftPossible()))
                        {
                            gameConfig.Xcoord--;
                        }
                        break;

                case Keys.Right:
                        if ((gameConfig.Xcoord < gameBoard.gamePiece.rightBound) &&
                            (gameBoard.IsRightPossible()))
                        {
                            gameConfig.Xcoord++;
                        }
                        break;

                case Keys.Up:
                        if (gameBoard.gamePiece.orientation == Orientation.VERTICAL_RIGHT)
                        {
                            gameBoard.gamePiece.orientation = Orientation.HORIZONTAL_UP;
                        }
                        else
                        {
                            gameBoard.gamePiece.orientation++;
                        }
                        break;

                case Keys.Down:
                        gameBoard.DropPieceToBottom();
                      break;

                case Keys.P:
                //case Keys.Space:
                      if (gameBoard.timer1.Enabled)
                          gameBoard.timer1.Enabled = false;
                      else
                          gameBoard.timer1.Enabled = true;
                      break;

                case Keys.R:
                      gameBoard.timer1.Enabled = false;
                      gameBoard.ResetGameBoard();
                      gameConfig.ResetGameConfig();
                      gameBoard.timer1.Enabled = true;
                      break;
            }
        }

    }
}