/*
 * Christina Leichtenschlag
 * Brock Stoops
 * COP 4020 - Programming Lanugages
 * Rick Leinecker
 * Fall 2014
 * Tic Tac Toe AI Program - Visual C#
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TicTacToeForm());
        }
    }
}
