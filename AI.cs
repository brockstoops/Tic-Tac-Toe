/*
 * Christina Leichtenschlag
 * Brock Stoops
 * COP 4020 - Programming Lanugages
 * Rick Leinecker
 * Fall 2014
 * Tic Tac Toe AI Program - Visual C#
 */

using System;

namespace TicTacToe
{
    public class TTTAI
    {

        /**
         * This function returns the computer's move on the board, 0-8
         * difficulty determines what order the 8 step algorithm will be executed in.
         */
        static public int getMove(char[] board, char piece, char opiece, int difficulty) {

            int ret;

            switch(difficulty) {
                case 1:
                    ret = easyMode(board, piece, opiece);
                    break;
                case 2:
                    ret = intermediateMode(board, piece, opiece);
                    break;
                default:
                    ret = hardMode(board, piece, opiece);
                    break;
            }

            if(ret != -1)
                return ret;
            else {
                // Somehow algorithm failed; make computer go to next empty space.
                for(int i=0; i<9; i++) {
                    if(board[i] == '\0') return i;
                }
            }
            return ret; // Should never get here, cause if you do there were no empty spaces on the board to begin with.
        }
        
        static int easyMode(char[] board, char piece, char opiece) {
            int ret = -1;
			// 1
            ret = checkComputerWin(board, piece, opiece);
            if(ret != -1) return ret;
			// 8
            ret = playEmptySide(board, piece, opiece);
            if(ret != -1) return ret;
            // 7
            ret = playEmptyCorner(board, piece, opiece);
            if(ret != -1) return ret;
			// 2
			ret = checkUserWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 4
            ret = blockUsers2WayWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 5
            ret = playCenter(board, piece, opiece);
            if(ret != -1) return ret;
            // 6
            ret = playOppositeCorner(board, piece, opiece);
            if(ret != -1) return ret;
            // 3
            ret = setFor2WayWin(board, piece, opiece);
            
            return ret;
        }

        static int intermediateMode(char[] board, char piece, char opiece) {
            int ret = -1;
            // 1
            ret = checkComputerWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 2
            ret = checkUserWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 4
            ret = blockUsers2WayWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 6
            ret = playOppositeCorner(board, piece, opiece);
            if(ret != -1) return ret;
            // 5
            ret = playCenter(board, piece, opiece);
            if(ret != -1) return ret;
            // 3
            ret = setFor2WayWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 7
            ret = playEmptyCorner(board, piece, opiece);
            if(ret != -1) return ret;
            // 8
            ret = playEmptySide(board, piece, opiece);

            return ret;
        }

        static int hardMode(char[] board, char piece, char opiece) {
            int ret = -1;
            // 1
			ret = checkDiags(board, piece, opiece);
			if(ret != -1) return ret;
			//1
            ret = checkComputerWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 2
            ret = checkUserWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 3
            ret = setFor2WayWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 4
            ret = blockUsers2WayWin(board, piece, opiece);
            if(ret != -1) return ret;
            // 5
            ret = playCenter(board, piece, opiece);
            if(ret != -1) return ret;
            // 6
            ret = playOppositeCorner(board, piece, opiece);
            if(ret != -1) return ret;
            // 7
            ret = playEmptyCorner(board, piece, opiece);
            if(ret != -1) return ret;
            // 8
            ret = playEmptySide(board, piece, opiece);
            return ret;
        }
		static int checkDiags (char[] board, char piece, char opiece) {
			if (board[4] == piece){
				if (board[0] == opiece && board[8] == opiece)
					if(board[1] == '\0' && board[2] == '\0' && board[3] == '\0' && board[5] == '\0' && board[6] == '\0' && board[7] == '\0')
						return 1;
				if (board[2] == opiece && board[6] == opiece)
					if(board[0] == '\0' && board[1] == '\0' && board[3] == '\0' && board[5] == '\0' && board[8] == '\0' && board[7] == '\0')
						return 1;
			}
			return -1;
		}
        static int checkComputerWin(char[] board, char piece, char opiece) {
            //Check if computer can win through rows (1)
            for (int i = 0; i < 3; i++) {
                if (board [i * 3] == piece && board [i * 3 + 1] == piece && board [i * 3 + 2] == '\0')
                    return i * 3 + 2;
                else if (board [i * 3 + 1] == piece && board [i * 3 + 2] == piece && board [i * 3] == '\0')
                    return i * 3;
                else if (board [i * 3] == piece && board [i * 3 + 2] == piece && board [i * 3 + 1] == '\0')
                    return i * 3 + 1;
            }
            //Check if computer can win through columns (1)
            for (int i = 0; i < 3; i++) {
                if (board [i] == piece && board [i + 3] == piece && board [i + 6] == '\0')
                    return i + 6;
                else if (board [i + 3] == piece && board [i + 6] == piece && board [i] == '\0')
                    return i;
                else if (board [i + 6] == piece && board [i] == piece && board [i + 6] == '\0')
                    return i + 6;
            }
			//Check if computer can win through diagonals (1)
            for (int i = 0; i < 3; i+=2) {
                if (board [i] == piece && board [4] == piece && board [8-i] == '\0')
                    return 8-i;
                else if (board [8-i] == piece && board [4] == piece && board [i] == '\0')
                    return i;
                else if (board [8-i] == piece && board [i] == piece && board [4] == '\0')
                    return 4;
            }
            return -1;
        }
        static int checkUserWin(char[] board, char piece, char opiece) {
            //Check if opponent can win through rows (2)
            for (int i = 0; i < 3; i++) {
                if (board [i * 3] == opiece && board [i * 3 + 1] == opiece && board [i * 3 + 2] == '\0')
                    return i * 3 + 2;
                else if (board [i * 3 + 1] == opiece && board [i * 3 + 2] == opiece && board [i * 3] == '\0')
                    return i * 3;
                else if (board [i * 3] == opiece && board [i * 3 + 2] == opiece && board [i * 3 + 1] == '\0')
                    return i * 3 + 1;
            }

            //Check if opponent can win through columns (2)
            for (int i = 0; i < 3; i++) {
                if (board [i] == opiece && board [i + 3] == opiece && board [i + 6] == '\0')
                    return i + 6;
                else if (board [i + 3] == opiece && board [i + 6] == opiece && board [i] == '\0')
                    return i;
                else if (board [i + 6] == opiece && board [i] == opiece && board [i + 3] == '\0')
                    return i + 3;
            }

            //Check if opponent can win through diagonals (2)
            for (int i = 0; i < 3; i+=2) {
                if (board [i] == opiece && board [4] == opiece && board [8-i] == '\0')
                    return 8-i;
                else if (board [8-i] == opiece && board [4] == opiece && board [i] == '\0')
                    return i;
                else if (board [8-i] == opiece && board [i] == opiece && board [4] == '\0')
                    return 4;
            }

            return -1;
        }


        static int setFor2WayWin(char[] board, char piece, char opiece) {
            //Set up 2 way win (3)
            for (int i = 0; i < 9; i++) {
                if(board[i] == '\0') {
                    if(board[4] == piece || board[8-i] == piece)
						if(board[4] != opiece && board[8-1] != opiece){
	                        if(board[(i+3)%9] == piece || board[(i+6)%9] == piece) 
								if((i+3)%9 != 4 && (i+6)%9 != 4)
	                            	return i;
	                        else if(board[(i/3)*3] == piece || board[(i/3)*3 +1] == piece || board[(i/3)*3 +2] == piece)
								if((i/3)*3 != 4 && (i/3)*3 +1 != 4 && (i/3)*3 +2 !=4)
	                            	return i;
						}
                    if(board[(i+3)%9] == piece || board[(i+6)%9] == piece)  
                        if(board[(i/3)*3] == piece || board[(i/3)*3 +1] == piece || board[(i/3)*3 +2] == piece)
                            return i;
                }
            }

            return -1;
        }
        static int blockUsers2WayWin(char[] board, char piece, char opiece) {
            //Block 2 way win (4)
            for (int i = 0; i < 9; i++) {
                if(board[i] == '\0') {
                    if(i%3 != 1){
                        if(board[4] == opiece || board[8-i] == opiece)
							if(board[4] != piece && board[8-1] != piece){
	                            if(board[(i+3)%9] == opiece || board[(i+6)%9] == opiece)
									if(board[(i+3)%9] != piece && board[(i+6)%9] != piece)
	    								    return i;
	                            else if(board[(i/3)*3] == opiece || board[(i/3)*3 +1] == opiece || board[(i/3)*3 +2] == opiece)
	                            	if(board[(i/3)*3] != piece && board[(i/3)*3 +1] != piece && board[(i/3)*3 +2] != piece)
	    						            return i;    
                         	}
						else if(board[(i+3)%9] == opiece || board[(i+6)%9] == opiece){
							if(board[(i+3)%9] != piece && board[(i+6)%9] != piece)
    							if(board[(i/3)*3] == opiece || board[(i/3)*3 +1] == opiece || board[(i/3)*3 +2] == opiece)
	                            	if(board[(i/3)*3] != piece && board[(i/3)*3 +1] != piece && board[(i/3)*3 +2] != piece)
	    						            return i; 
						}
					}
					if(i%3 == 1)
						if(board[4] == opiece || board[8-i] == opiece)
							if(board[4] != piece && board[8-1] != piece){
	                            if(board[(i/3)*3] == opiece || board[(i/3)*3 +1] == opiece || board[(i/3)*3 +2] == opiece)
	                            	if(board[(i/3)*3] != piece && board[(i/3)*3 +1] != piece && board[(i/3)*3 +2] != piece)
	    						            return i;    
                         }
					/*if((i+3%9) != 4 && (i+6%9) != 4)
                    	if(board[(i+3)%9] == opiece || board[(i+6)%9] == opiece)
                        	if(board[(i+3)%9] != piece && board[(i+6)%9] != piece){
								if(board[4] == opiece && board[8-i] == '\0')
									return i;
						   		else if(board[(i/3)*3] == opiece || board[(i/3)*3 +1] == opiece || board[(i/3)*3 +2] == opiece)
                              		if(board[(i/3)*3] != piece && board[(i/3)*3 +1] != piece && board[(i/3)*3 +2] != piece)
							     	   return i;
							}*/
                }
            }

            return -1;
        }
        static int playCenter(char[] board, char piece, char opiece) {
            //Center (5)
            if(board[4] == '\0')
                return 4;

            return -1;
        }

        static int playOppositeCorner(char[] board, char piece, char opiece) {
            //Play in an opposite corner (6)
            if(board[0] == '\0' && board[8] == opiece)
                return 0;
            if(board[2] == '\0' && board[6] == opiece)
                return 2;
            if(board[6] == '\0' && board[2] == opiece)
                return 6;
            if(board[8] == '\0' && board[0] == opiece)
                return 8;

            return -1;
        }

        static int playEmptyCorner(char[] board, char piece, char opiece) {
            //Play in empty corner (7)

            if(board[0] == '\0')
                return 0;
            if(board[2] == '\0')
                return 2;
            if(board[6] == '\0')
                return 6;
            if(board[8] == '\0')
                return 8;

            return -1;
        }

        static int playEmptySide(char[] board, char piece, char opiece) {
            //Play in empty side (8)
            if(board[1] == '\0')
                return 1;
            else if(board[3] == '\0')
                return 3;
            else if(board[5] == '\0')
                return 5;
            else if(board[7] == '\0')
                return 7;

            return -1;
        }

        // Return a character indicating the winner. If '\0' is returned there is no winner.
        public static char checkForAWinner(char[] b) {
            
            char winner = '\0'; // Return the null character if there is no winner.
            
            if(b[0] == b[1] && b[1] == b[2]) {
                // Win along the first row
                return b[0];
            }
            else if(b[3] == b[4] && b[4] == b[5]) {
                // Win along the second row
                return b[3];
            }
            else if(b[6] == b[7] && b[7] == b[8]) {
                // Win along the third row
                return b[6];
            }
            else if(b[0] == b[3] && b[3] == b[6]) {
                // Win along the first column
                return b[0];
            }
            else if(b[1] == b[4] && b[4] == b[7]) {
                // Win along the second column
                return b[1];
            }
            else if(b[2] == b[5] && b[5] == b[8]) {
                // Win along the third column
                return b[2];
            }
            else if(b[0] == b[4] && b[4] == b[8]) {
                // Win along the top-left to bottom-right diagonal
                return b[0];
            }
            else if(b[2] == b[4] && b[4] == b[6]) {
                // Win along the top-right to bottom-left diagonal
                return b[2];
            }
            else {
                return winner;
            }
        }


    }
}