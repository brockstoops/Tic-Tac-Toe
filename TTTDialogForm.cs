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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class TTTDialogForm : Form
    {
        private static string imagedir = ""; // filepath to image directory (for VS)
        
        public TTTDialogForm(string gameStatusStr)
        {
            InitializeComponent();
            this.label1.Text = gameStatusStr;
            this.button1.Image = Image.FromFile(imagedir + "playagaingray.png");
        }

        private void TTTDialogForm_Load(object sender, EventArgs e)
        {
        }

        // This is the onClickListener for button 1.
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }// end class
}// end namespace
