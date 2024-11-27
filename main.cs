// C# program to find the 
// next optimal move for a player 
using System;
using System.Collections.Generic;

class GFG
{
    class Move
    {
        public int row, col;
    };

    static char player = 'x', opponent = 'o';

    // This function returns true if there are moves 
    // remaining on the board. It returns false if 
    // there are no moves left to play. 
    static Boolean isMovesLeft(char[,] board)
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (board[i, j] == '_')
                    return true;
        return false;
    }

    static Boolean haswon(char p, char[,] board)
    {
        //3x Horizontálně
        if (p == board[0, 0] && p == board[0, 1] && p == board[0, 2]) return true;
        if (p == board[1, 0] && p == board[1, 1] && p == board[1, 2]) return true;
        if (p == board[2, 0] && p == board[2, 1] && p == board[2, 2]) return true;
        //3x Vertikálně
        if (p == board[0, 0] && p == board[1, 0] && p == board[2, 0]) return true;
        if (p == board[0, 1] && p == board[1, 1] && p == board[2, 1]) return true;
        if (p == board[0, 2] && p == board[1, 2] && p == board[2, 2]) return true;
        //3x Diagonálně
        if (p == board[0, 0] && p == board[1, 1] && p == board[2, 2]) return true;
        if (p == board[0, 2] && p == board[1, 1] && p == board[2, 0]) return true;
        //Else
        return false;
    }

    // This is the evaluation function as discussed 
    // in the previous article ( http://goo.gl/sJgv68 ) 
    static int evaluate(char[,] b)
    {
        // Checking for Rows for X or O victory. 
        for (int row = 0; row < 3; row++)
        {
            if (b[row, 0] == b[row, 1] &&
              b[row, 1] == b[row, 2])
            {
                if (b[row, 0] == player)
                    return +10;
                else if (b[row, 0] == opponent)
                    return -10;
            }
        }

        // Checking for Columns for X or O victory. 
        for (int col = 0; col < 3; col++)
        {
            if (b[0, col] == b[1, col] &&
              b[1, col] == b[2, col])
            {
                if (b[0, col] == player)
                    return +10;

                else if (b[0, col] == opponent)
                    return -10;
            }
        }

        // Checking for Diagonals for X or O victory. 
        if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
        {
            if (b[0, 0] == player)
                return +10;
            else if (b[0, 0] == opponent)
                return -10;
        }

        if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
        {
            if (b[0, 2] == player)
                return +10;
            else if (b[0, 2] == opponent)
                return -10;
        }

        // Else if none of them have won then return 0 
        return 0;
    }

    // This is the minimax function. It considers all 
    // the possible ways the game can go and returns 
    // the value of the board 
    static int minimax(char[,] board,
            int depth, Boolean isMax)
    {
        int score = evaluate(board);

        // If Maximizer has won the game 
        // return his/her evaluated score 
        if (score == 10)
            return score;

        // If Minimizer has won the game 
        // return his/her evaluated score 
        if (score == -10)
            return score;

        // If there are no more moves and 
        // no winner then it is a tie 
        if (isMovesLeft(board) == false)
            return 0;

        // If this maximizer's move 
        if (isMax)
        {
            int best = -1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = player;

                        // Call minimax recursively and choose 
                        // the maximum value 
                        best = Math.Max(best, minimax(board,
                                depth + 1, !isMax));

                        // Undo the move 
                        board[i, j] = '_';
                    }
                }
            }
            return best;
        }

        // If this minimizer's move 
        else
        {
            int best = 1000;

            // Traverse all cells 
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty 
                    if (board[i, j] == '_')
                    {
                        // Make the move 
                        board[i, j] = opponent;

                        // Call minimax recursively and choose 
                        // the minimum value 
                        best = Math.Min(best, minimax(board,
                                depth + 1, !isMax));

                        // Undo the move 
                        board[i, j] = '_';
                    }
                }
            }
            return best;
        }
    }

    // This will return the best possible 
    // move for the player 
    static Move findBestMove(char[,] board)
    {
        int bestVal = -1000;
        Move bestMove = new Move();
        bestMove.row = -1;
        bestMove.col = -1;

        // Traverse all cells, evaluate minimax function 
        // for all empty cells. And return the cell 
        // with optimal value. 
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                // Check if cell is empty 
                if (board[i, j] == '_')
                {
                    // Make the move 
                    board[i, j] = player;

                    // compute evaluation function for this 
                    // move. 
                    int moveVal = minimax(board, 0, false);

                    // Undo the move 
                    board[i, j] = '_';

                    // If the value of the current move is 
                    // more than the best value, then update 
                    // best/ 
                    if (moveVal > bestVal)
                    {
                        bestMove.row = i;
                        bestMove.col = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        Console.Write("The value of the best Move " +
                  "is : {0}\n\n", bestVal);

        return bestMove;
    }

    // Driver code 
    public static void Main(String[] args)
    {
        //Format: row,col (-,|)
        Console.Write("Tic Tac Toe\n\nAfter each round you will be asked to enter the row or column of your move.\nIf you want to abort the game enter 0 for row or column.\n\n");
        int inptcol = 0, inptrow = 0;
        char[,] board = {{ '_', '_', '_' },
          { '_', '_', '_' },
          { '_', '_', '_' }};
        while (inptcol != -1 & inptrow != -1)
        {
            Move bestMove = findBestMove(board);

            Console.Write("My move is: ");
            Console.Write("ROW: {0} COL: {1}\n\n", bestMove.row, bestMove.col);
            board[bestMove.row, bestMove.col] = 'x';
            Console.Write("\n{0}|{1}|{2}", board[0, 0], board[0, 1], board[0, 2]);
            Console.Write("\n{0}|{1}|{2}", board[1, 0], board[1, 1], board[1, 2]);
            Console.Write("\n{0}|{1}|{2}", board[2, 0], board[2, 1], board[2, 2]);
            //Have the bot won?
            if (haswon(player, board)) {
                Console.WriteLine("\n I won!\n\n(Exit by pressing any button)");
                Console.ReadKey();
                Environment.Exit(0);
            }


                Console.Write("\nYour move: Row: ");
            inptrow = int.Parse(Console.ReadLine()) - 1;
            Console.Write(" Col: ");
            inptcol = int.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("{0},{1}", inptrow, inptcol);
            //Is the move invalid?
            if (board[inptrow, inptcol] != '_') {
                Console.WriteLine("\nInvalid move. Please try again.\n(If you do another invalid move your round will be skipped)\n\n");
                Console.Write("\nYour move: Row: ");
                inptrow = int.Parse(Console.ReadLine()) - 1;
                Console.Write(" Col: ");
                inptcol = int.Parse(Console.ReadLine()) - 1;
                Console.WriteLine("Your move is {0},{1}", inptrow, inptcol);
                if (board[inptrow, inptcol] == '_') {
                    board[inptrow, inptcol] = 'o';
                }
            } else {
                board[inptrow, inptcol] = 'o';
            }
            
            //Have the player won?
            if (haswon(opponent, board)) {
                Console.Write("\n{0}|{1}|{2}", board[0, 0], board[0, 1], board[0, 2]);
                Console.Write("\n{0}|{1}|{2}", board[1, 0], board[1, 1], board[1, 2]);
                Console.Write("\n{0}|{1}|{2}", board[2, 0], board[2, 1], board[2, 2]);
                Console.WriteLine("\n\n Look, you won! Congrats on beating me!\n\n(Exit by pressing any button)");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}