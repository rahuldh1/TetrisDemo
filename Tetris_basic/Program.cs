using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_basic
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GameConfig gameconfig = new GameConfig();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GameBoard gameBoard = new GameBoard(gameconfig);
            GameController gameController = new GameController(gameBoard, gameconfig);

            Application.Run(gameBoard);
        }
    }
}
