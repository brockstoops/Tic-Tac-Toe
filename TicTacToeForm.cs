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
    public partial class TicTacToeForm : Form
    {
        private static string imagedir = ""; // filepath to image directory (for VS)
		private char[] buttons = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };
        
        // Images
        private static Image unclickedSquare = Image.FromFile(imagedir + "graysquare.png");
        private static Image classicX = Image.FromFile(imagedir + "X.png");
        private static Image classicO = Image.FromFile(imagedir + "O.png");
        private static Image penguin = Image.FromFile(imagedir + "penguin.png");
        private static Image batman = Image.FromFile(imagedir + "batman.png");
        private static Image bulls = Image.FromFile(imagedir + "bulls.png");
        private static Image knightro = Image.FromFile(imagedir + "knightro.png");
        private Image computerImage = Image.FromFile(imagedir + "O.png");
        private Image userImage = Image.FromFile(imagedir + "X.png");

        // Variables
        bool gameInProgress = false;
        bool computersMove = false;
        bool AI = true;
        bool p2Turn = false;
        bool defaultUserPiece = true;
        char winner = '\0';
        int wins;
        int ties;
        int losses;
        int difficulty = 3;

        /**
         * Constructor
         */
        public TicTacToeForm()
        {
            InitializeComponent();
            this.wins = 0;
            this.ties = 0;
            this.losses = 0;
            reinitializeBoard();
            this.button10.Image = Image.FromFile(imagedir + "grayrectangle.png");
        }

        private void TicTacToeForm_Load(object sender, EventArgs e)
        {
        }

        /**
         * Simple function; refreshes the board to be all brand new!
         */ 
        private void reinitializeBoard()
        {
            // Begin by resetting the buttons array.
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = '\0';
            }

            // Then reset the actual buttons.
            this.button1.Enabled = true; // reenable the button
            this.button1.Image = unclickedSquare; // reset image (i.e. remove it)
            this.button2.Enabled = true;
            this.button2.Image = unclickedSquare;
            this.button3.Enabled = true;
            this.button3.Image = unclickedSquare;
            this.button4.Enabled = true;
            this.button4.Image = unclickedSquare;
            this.button5.Enabled = true;
            this.button5.Image = unclickedSquare;
            this.button6.Enabled = true;
            this.button6.Image = unclickedSquare;
            this.button7.Enabled = true;
            this.button7.Image = unclickedSquare;
            this.button8.Enabled = true;
            this.button8.Image = unclickedSquare;
            this.button9.Enabled = true;
            this.button9.Image = unclickedSquare;

            // Penalize the leavers!
            if(this.gameInProgress && this.AI) // If you're in the middle of the game with the computer & you leave, you forefit!
            {
                this.losses++;
            }

            // Update W/T/L section
            updateStatistics();

            this.winner = '\0';
            this.p2Turn = false;

            if(this.radioButton10.Checked && this.AI) // AI mode, & user wants the computer to make the first move.
            {
                this.gameInProgress = true;
                this.computersMove = true;
                makeComputerMove();
            }
            else // Wait for the user or a player to make a move.
            {
                this.gameInProgress = false;
                this.computersMove = false;
            }
        }

        /**
         * @param style: the style the user wants.
         * 
         * This function will change the board style between Classic, Batman, and UCF icons.
         */
        private void changeImageStyle(string style)
        {
            // First reset the user and computer images.
            if(style.Equals("classic"))
            {
                if(this.defaultUserPiece)
                {
                    // Computer is O and user is X
                    this.userImage = classicX;
                    this.computerImage = classicO;
                }
                else
                {
                    // Computer is X and user is O
                    this.userImage = classicO;
                    this.computerImage = classicX;
                }
            }
            else if(style.Equals("batman"))
            {
                if (this.defaultUserPiece)
                {
                    // Computer is the penguin and user is batman
                    this.userImage = batman;
                    this.computerImage = penguin;
                }
                else
                {
                    // Computer is batman and user is the penguin
                    this.userImage = penguin;
                    this.computerImage = batman;
                }
            }
            else
            {
                // UCF style;
                if (this.defaultUserPiece)
                {
                    // Computer is bulls logo and user is knightro
                    this.userImage = knightro;
                    this.computerImage = bulls;
                }
                else
                {
                    // Computer is knightro and user is bulls logo
                    this.userImage = bulls;
                    this.computerImage = knightro;
                }
            }

            // Next, replace all images on the board with the new images.
            if(this.buttons[0] == 'X') {
                this.button1.Image = this.userImage;
            }
            else if(buttons[0] == 'O') {
                this.button1.Image = this.computerImage;
            }
            if(this.buttons[1] == 'X') {
                this.button2.Image = this.userImage;
            }
            else if(this.buttons[1] == 'O') {
                this.button2.Image = this.computerImage;
            }
            if(this.buttons[2] == 'X') {
                this.button3.Image = this.userImage;
            }
            else if(this.buttons[2] == 'O') {
                this.button3.Image = this.computerImage;
            }
            if(this.buttons[3] == 'X') {
                this.button4.Image = this.userImage;
            }
            else if(this.buttons[3] == 'O') {
                this.button4.Image = this.computerImage;
            }
            if(this.buttons[4] == 'X') {
                this.button5.Image = this.userImage;
            }
            else if(this.buttons[4] == 'O') {
                this.button5.Image = this.computerImage;
            }
            if(this.buttons[5] == 'X') {
                this.button6.Image = this.userImage;
            }
            else if(this.buttons[5] == 'O') {
                this.button6.Image = this.computerImage;
            }
            if(this.buttons[6] == 'X') {
                this.button7.Image = this.userImage;
            }
            else if(this.buttons[6] == 'O') {
                this.button7.Image = this.computerImage;
            }
            if(this.buttons[7] == 'X') {
                this.button8.Image = this.userImage;
            }
            else if(this.buttons[7] == 'O') {
                this.button8.Image = this.computerImage;
            }
            if(this.buttons[8] == 'X') {
                this.button9.Image = this.userImage;
            }
            else if(this.buttons[8] == 'O') {
                this.button9.Image = this.computerImage;
            }
        }


        /**
         * @param butt: the button to be updated
         * @whichButt: the index of the button for the buttons array
         * @return: true is the move was successful, false if it was not
         *
         * This function disables the input button. Also changes the button's image.
         */
        bool killButton(Button butt, int whichButt)
        {
            this.gameInProgress = true;
            if (!isSpaceOccupied(whichButt))
            {
                // Space is empty; make a move, then turn off clicking.
                if(this.computersMove) 
                {
                    // Computer's move; use the computer image
                    butt.Image = this.computerImage;
                    this.computersMove = false;
                    this.buttons[whichButt] = 'O';
                }
                else 
                {
                    // It's a player's move.
                    if (!this.AI && this.p2Turn)
                    {
                        // It's PVP, so use the computer's image for player 2.
                        butt.Image = this.computerImage;
                        this.buttons[whichButt] = 'O';
                    }
                    else
                    {
                        // It's PVP and player 1's turn.
                        butt.Image = this.userImage;
                        this.buttons[whichButt] = 'X';
                    }
                }
                
                butt.Enabled = false; // disable the button.
                return true;
            }
            else
            {
                return false;
            }

        }

        /**
         * @param location: the location in the buttons char array that the button clicked corresponds to
         * @return bool: true if the location is occupied; false otherwise
         * 
         * Simply return true if the specified location on the board is occupied and false if the location is unoccupied.
         */
        bool isSpaceOccupied(int location)
        {
            if (this.buttons[location] == '\0')
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /**
         * Use this function to update the statistics when the user starts a new game.
         * Updates the visual board.
         */
        void updateStatistics()
        {
            this.numWins.Text = this.wins.ToString();
            this.numTies.Text = this.ties.ToString();
            this.numLosses.Text = this.losses.ToString();
        }

        /**
         * @return: true if the game is over, false if it is not.
         * 
         * This function is used to determine if the game is over.
         * First it uses a function in the AI to determine if there is a winner.
         * If there is, set the winner & return true. If there's not, check to see if more moves
         * can be made. If the board is filled up return true, else return false.
         */
        bool isGameOver()
        {
            char temp = TTTAI.checkForAWinner(buttons);
            if(temp != '\0')
            {
                this.winner = temp;
                return true;
            }

            // There isn't a winner, but the game is over if all spaces on the board are occupied.
            for(int i=0; i<this.buttons.Length; i++)
            {
                if(this.buttons[i] == '\0')
                {
                    return false; // A move can still be made; thus game goes on!
                }
            }
            return true; // No more moves can be made; the game is over!
        }

        /**
         * @param userJustMoved: true if the user just made a move, false if the computer just made a move.
         * 
         * This method is called each time a move is made. If the game is over as a result, show a dialog and
         * then reset the board. If the game is not over, then it's either the computer or the user's turn.
         */
        void determineEndgame(bool userJustMoved) {

            if(isGameOver())
            {
                // Game is over
                // Determine Winner.
                string message = "";
                if(this.winner == 'X') 
                {
                    // User wins.
                    if(this.AI)
                    {
                        this.wins++;
                        message = "You Won!";
                    }
                    else
                    {
                        message = "P1 Wins!";
                    }
                } 
                else if(this.winner == 'O')
                {
                    // Computer wins; user loses.
                    if(this.AI)
                    {
                        this.losses++;
                        message = "You Lost!";
                    }
                    else
                    {
                        message = "P2 Wins!";
                    }
                }
                else
                {
                    // There is no winner.
                    if(this.AI)
                    {
                        this.ties++;
                    }
                    message = "You Tied!";
                }

                // Show dialog
                Form endgameDialog = new TTTDialogForm(message); // Message is either "You Won!" "You Tied!" or "You Lost!"
                endgameDialog.ShowDialog();

                // Reset the board
                this.gameInProgress = false;
                reinitializeBoard();
            }
            else 
            {
                if(userJustMoved && this.AI)
                {
                    // Now the computer gets to make a move.
                    makeComputerMove();
                }
                else if (!this.AI)
                {
                    if (!this.p2Turn)
                    {
                        this.p2Turn = true;
                    }
                    else
                    {
                        this.p2Turn = false;
                    }
                }
            }
        }


        /**
         * This function first calls the AI to get a space to move to.
         * Next it updates the gui
         * Finally makes a call to determineEndgame to check if the game is over or not.
         */
        void makeComputerMove()
        {
            computersMove = true;
            int compMove = TTTAI.getMove(buttons, 'O', 'X', this.difficulty); // Get computer's move.
            bool error = true;
            switch(compMove) // Update the board to reflect computer's move.
            {
                case 0: 
                    error = killButton(this.button1, 0);
                    break;
                case 1: 
                    error = killButton(this.button2, 1);
                    break;
                case 2: 
                    error = killButton(this.button3, 2);
                    break;
                case 3: 
                    error = killButton(this.button4, 3);
                    break;
                case 4: 
                    error = killButton(this.button5, 4);
                    break;
                case 5: 
                    error = killButton(this.button6, 5);
                    break;
                case 6: 
                    error = killButton(this.button7, 6);
                    break;
                case 7: 
                    error = killButton(this.button8, 7);
                    break;
                case 8: 
                    error = killButton(this.button9, 8);
                    break;
            }

            if( !error )  // An error occurred, the computer did not make a move.!!!
            {
                Form dd = new TTTDialogForm("ERROR");
                dd.ShowDialog();
            }
            determineEndgame(false);
        }


        /** Begin neverending list on onClickListener functions. */

        // This is the onClickListener for button 1. (top left)
        private void button1_Click(object sender, EventArgs e)
        {
            if( killButton(button1, 0) )
            {
                determineEndgame(true);
            }
        }
        // This is the onClickListener for button 2. (top middle)
        private void button2_Click(object sender, EventArgs e)
        {
            if( killButton(button2, 1) )
            {
                determineEndgame(true);
            }
        }
        // This is the onClickListener for button 3. (top right)
        private void button3_Click(object sender, EventArgs e)
        {
            if( killButton(button3, 2) )
            {
                determineEndgame(true);
            }
        }
        // This is the onClickListener for button 4. (middle left)
        private void button4_Click(object sender, EventArgs e)
        {
            if( killButton(button4, 3) )
            {
                determineEndgame(true);
            }
        }
        // This is the onClickListener for button 5. (middle middle)
        private void button5_Click(object sender, EventArgs e)
        {
            if( killButton(button5, 4) )
            {
                determineEndgame(true);
            }
        }
        // This is the onClickListener for button 6. (middle right)
        private void button6_Click(object sender, EventArgs e)
        {
            if( killButton(button6, 5) )
            {
                determineEndgame(true);
            }
        }
        // This is the onClickListener for button 7. (bottom left)
        private void button7_Click(object sender, EventArgs e)
        {
            if( killButton(button7, 6) )
            {
                determineEndgame(true);
            }
        }
        // This is the onClickListener for button 8. (bottom middle)
        private void button8_Click(object sender, EventArgs e)
        {
            if( killButton(button8, 7) )
            {
                determineEndgame(true);
            }
        }
        // This is the onClickListener for button 9. (bottom right)
        private void button9_Click(object sender, EventArgs e)
        {
            if( killButton(button9, 8) )
            {
                determineEndgame(true);
            }
        }

        // Button for those who choose to give up. 
        private void button10_Click(object sender, EventArgs e)
        {
            reinitializeBoard();
        }

        // OnClick for when user selects "easy mode" ... wimp
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked) // Do this so reinitialize doesn't get called twice.
            {
                this.difficulty = 1;
                reinitializeBoard();
            }
        }

        // OnClick for when the user selects intermediate mode.
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) // Do this so reinitialize doesn't get called twice.
            {
                this.difficulty = 2;
                reinitializeBoard();
            }
        }

        // OnClick to enable the tic tac toe AI. >:)
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) // Do this so reinitialize doesn't get called twice.
            {
                this.difficulty = 3;
                reinitializeBoard();
            }
        }

        // OnClick to change the images in the game.
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked) // Change the board to Classic images!
            {
                changeImageStyle("classic");
                this.radioButton12.Text = "X";
                this.radioButton11.Text = "O";
            }
        }

        // OnClick to change the images in the game.
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked) // Change the board to Batman images!
            {
                changeImageStyle("batman");
                this.radioButton12.Text = "Batman";
                this.radioButton11.Text = "Penguin";
            }
        }

        // OnClick to change the images in the game.
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked) // Change the board to UCF images!
            {
                changeImageStyle("ucf");
                this.radioButton12.Text = "Knights";
                this.radioButton11.Text = "Bulls";
            }
        }

        // OnClick for changing game mode to AI
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked)
            {
                this.AI = true;
                // reinitializeBoard will inc losses even though it was a switch from pvp. So decrement to zero it out.
                if(this.gameInProgress) 
                {
                    this.losses--;
                }
                reinitializeBoard();
            }
        }

        // OnClick for changing game mode to PVP
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                this.AI = false;
                if(this.gameInProgress)
                {
                    this.losses++; // User quit AI in the middle of a game! Mark loss!
                }
                reinitializeBoard();
            }
        }

        // OnClick for changing the user piece to the default user piece
        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton12.Checked)
            {
                this.defaultUserPiece = true;
                getAndChangeImageStyle();
            }
        }

        // OnClick for changnig the user piece to the default computer piece
        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton11.Checked)
            {
                this.defaultUserPiece = false;
                getAndChangeImageStyle();
            }
        }

        /**
         * When the user changes his/her piece, need to redraw all images. Helper function to do so:
         */
        private void getAndChangeImageStyle()
        {
            if (radioButton4.Checked) // Change the board to Classic images!
            {
                changeImageStyle("classic");
            }
            else if (radioButton5.Checked) // Change the board to Batman images!
            {
                changeImageStyle("batman");
            }
            else
            {
                changeImageStyle("ucf"); // Change the board to UCF/USF images!
            }
        }
        
        // OnClick, user wants the computer to make the first move.
        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButton10.Checked)
            {
                if(!this.gameInProgress)
                {
                    // Start the game by having the computer make the first move.
                    reinitializeBoard();
                }
            }
        }


    }// end class
}// end namespace
