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
using System.Windows.Input;
using System.Windows.Forms;

namespace Tetris_basic
{
    public class GameController : IGameController
    {
        private GameBoard gameBoard;
        private GameConfig gameConfig;

        public GameController(GameBoard viewParam, GameConfig gameconfigParam)
        {
            gameBoard = viewParam;
            gameConfig = gameconfigParam;            
        }
          
        public override void OnKeyDown(object sender, KeyEventArgs e)
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
