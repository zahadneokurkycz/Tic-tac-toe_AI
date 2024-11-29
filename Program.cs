// C# program to find the 
// next optimal move for a player 
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Nodes;

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

#pragma warning disable CS8600 // Literál s hodnotou null nebo s možnou hodnotou null se převádí na typ, který nemůže mít hodnotu null.
        JsonDocument langdoc = JsonSerializer.Deserialize<JsonDocument>(File.ReadAllText("./lang.json"));
#pragma warning restore CS8600 // Literál s hodnotou null nebo s možnou hodnotou null se převádí na typ, který nemůže mít hodnotu null.
#pragma warning disable CS8602 // Přístup přes ukazatel k možnému odkazu s hodnotou null
        JsonElement lang = langdoc.RootElement;
#pragma warning restore CS8602 // Přístup přes ukazatel k možnému odkazu s hodnotou null
        JsonElement activelang = lang.GetProperty(lang.GetProperty("Selected").ToString());


        Console.Write(activelang.GetProperty("valOfBestMove").ToString(), bestVal);

        return bestMove;
    }

    // Driver code 
    public static void Main(String[] args)
    {
        if(!File.Exists("./lang.json"))
        {
            Console.Write("Lang was not found\n\nInstalling now...");
            string[] defaultjson = { "" };
            Array.Resize(ref defaultjson, 31);
            
            //Soubor lang.json
            defaultjson[0] = "{";
            defaultjson[1] = "    \"Selected\": \"en\",";
            defaultjson[2] = "    \"cz\":{";
            defaultjson[3] = "        \"valOfBestMove\": \"Hodnota nejlepšího možného tahu: {0}\\n\\n\",";
            defaultjson[4] = "        \"uvod\": \"Piškvorky\\n\\nPo každém kole budete muset zadat řádek a sloupec vašeho tahu.\\nPokud chcete ukončit hru, zadejte pro řádek nebo sloupec 0.\\n\\n\\n\",";
            defaultjson[5] = "        \"playerSelect\": \"Jako kdo chcete hrát?\\nZadejte X nebo O: \",";
            defaultjson[6] = "        \"tabulka\": \"\\n{0}|{1}|{2}\",";
            defaultjson[7] = "        \"enterRow\": \"\\nVáš tah: Řádek: \",";
            defaultjson[8] = "        \"enterCol\": \" Sloupec: \","; 
            defaultjson[9] = "        \"invalidMove\": \"\\nNeplatný tah. Zkuste to znovu.\\n(Při dalším nepltném tahu bude váš taah přeskočen)\\n\\n\",";
            defaultjson[10] = "        \"playerMove\": \"Váš tah je {0},{1}\",";
            defaultjson[11] = "        \"aiMove\": \"Můj tah je: \",";
            defaultjson[12] = "        \"aiMoveFormate\": \"Řádek: {0} Sloupec: {1}\\n\\n\",";
            defaultjson[13] = "        \"aiWon\": \"\\n Vyhrál jsem!\\n\\n(Ukončete stisknutím jakéhokoliv tlačítka)\",";
            defaultjson[14] = "        \"playerWon\": \"\\n\\n Vyhrál jsi! GRatuluji!\\n\\n(Ukončíte stisknutím jakéhokoliv tlačítka)\"";
            defaultjson[15] = "    },";
            defaultjson[16] = "    \"en\":{";
            defaultjson[17] = "        \"valOfBestMove\": \"The value of the best Move is : {0}\\n\\n\",";
            defaultjson[18] = "        \"uvod\": \"Tic Tac Toe\\n\\nAfter each round you will be asked to enter the row or column of your move.\\nIf you want to abort the game enter 0 for row or column.\\n\\n\\n\","; 
            defaultjson[19] = "        \"playerSelect\": \"Who do you wanna play as?\\nType x or o: \",";
            defaultjson[20] = "        \"tabulka\": \"\\n{0}|{1}|{2}\","; 
            defaultjson[21] = "        \"enterRow\": \"\\nYour move: Row: \",";
            defaultjson[22] = "        \"enterCol\": \" Col: \",";
            defaultjson[23] = "        \"invalidMove\": \"\\nInvalid move. Please try again.\\n(If you do another invalid move your round will be skipped)\\n\\n\",";
            defaultjson[24] = "        \"playerMove\": \"Your move is {0},{1}\","; 
            defaultjson[25] = "        \"aiMove\": \"My move is: \",";
            defaultjson[26] = "        \"aiMoveFormate\": \"ROW: {0} COL: {1}\\n\\n\",";
            defaultjson[27] = "        \"aiWon\": \"\\n I won!\\n\\n(Exit by pressing any button)\",";
            defaultjson[28] = "        \"playerWon\": \"\\n\\n Look, you won! Congrats on beating me!\\n\\n(Exit by pressing any button)\"";
            defaultjson[29] = "    }";
            defaultjson[30] = "}";
            File.WriteAllLines("./lang.json", defaultjson);

            Console.Write("Installed now. Press any key to terminate");
            Console.ReadKey();
            Environment.Exit(0);
            
        }
#pragma warning disable CS8600 // Literál s hodnotou null nebo s možnou hodnotou null se převádí na typ, který nemůže mít hodnotu null.
        JsonDocument langdoc = JsonSerializer.Deserialize<JsonDocument>(File.ReadAllText("./lang.json"));
#pragma warning restore CS8600 // Literál s hodnotou null nebo s možnou hodnotou null se převádí na typ, který nemůže mít hodnotu null.
#pragma warning disable CS8602 // Přístup přes ukazatel k možnému odkazu s hodnotou null
        JsonElement lang = langdoc.RootElement;
#pragma warning restore CS8602 // Přístup přes ukazatel k možnému odkazu s hodnotou null
        JsonElement activelang = lang.GetProperty(lang.GetProperty("Selected").ToString());


        //Format: row,col (-,|)
        Console.Write(activelang.GetProperty("uvod"));
        Console.WriteLine(activelang.GetProperty("playerSelect"));
        opponent = Console.ReadKey().KeyChar;
        if (opponent == 'x') player = 'o';

        int inptcol = 0, inptrow = 0;
        bool successinptcol,successinptrow;
        char[,] board = {{ '_', '_', '_' },
          { '_', '_', '_' },
          { '_', '_', '_' }};

        
        //If the player starts
        if (opponent == 'x') {
            Console.Write(activelang.GetProperty("tabulka").ToString(), board[0, 0], board[0, 1], board[0, 2]);
            Console.Write(activelang.GetProperty("tabulka").ToString(), board[1, 0], board[1, 1], board[1, 2]);
            Console.Write(activelang.GetProperty("tabulka").ToString(), board[2, 0], board[2, 1], board[2, 2]);
    
            Console.Write(activelang.GetProperty("enterRow"));
            successinptrow = int.TryParse(Console.ReadLine(), out inptrow);
            inptrow--;
    
            Console.Write(activelang.GetProperty("enterCol"));
            successinptcol = int.TryParse(Console.ReadLine(), out inptcol);
            inptcol--;
            Console.Clear();

            //Is the move invalid?
            if (board[inptrow, inptcol] != '_' || inptrow > 3 || inptcol > 3)
            {
                Console.WriteLine(activelang.GetProperty("invalidMove"));
                Console.Write(activelang.GetProperty("enterRow"));
                successinptrow = int.TryParse(Console.ReadLine(), out inptrow);
                inptrow--;

                Console.Write(activelang.GetProperty("enterCol"));
                successinptcol = int.TryParse(Console.ReadLine(), out inptcol);
                inptcol--;

                Console.WriteLine(activelang.GetProperty("playerMove").ToString(), inptrow, inptcol);
                if (board[inptrow, inptcol] == '_' && inptrow < 3 && inptcol < 3)
                {
                    board[inptrow, inptcol] = opponent;
                }
            }
            else
            {
                board[inptrow, inptcol] = opponent;
            }
        }

        //Standard game thread (AI starts)
        while (inptcol != -1 & inptrow != -1)
        {
            Move bestMove = findBestMove(board);

            Console.Write(activelang.GetProperty("aiMove"));
            Console.Write(activelang.GetProperty("aiMoveFormate").ToString(), bestMove.row, bestMove.col);
            board[bestMove.row, bestMove.col] = player;
            Console.Write(activelang.GetProperty("tabulka").ToString(), board[0, 0], board[0, 1], board[0, 2]);
            Console.Write(activelang.GetProperty("tabulka").ToString(), board[1, 0], board[1, 1], board[1, 2]);
            Console.Write(activelang.GetProperty("tabulka").ToString(), board[2, 0], board[2, 1], board[2, 2]);
            //Have the bot won?
            if (haswon(player, board))
            {
                Console.WriteLine(activelang.GetProperty("aiWon"));
                Console.ReadKey();
                Environment.Exit(0);
            }


            Console.Write(activelang.GetProperty("enterRow"));
            successinptrow = int.TryParse(Console.ReadLine(), out inptrow);
            inptrow--;

            Console.Write(activelang.GetProperty("enterCol"));
            successinptcol = int.TryParse(Console.ReadLine(), out inptcol);
            inptcol--;

            Console.WriteLine(activelang.GetProperty("playerMove").ToString(), inptrow, inptcol);
            
            //Is the move invalid?
            if (board[inptrow, inptcol] != '_' || inptrow > 3 || inptcol > 3)
            {
                Console.WriteLine(activelang.GetProperty("invalidMove"));
                Console.Write(activelang.GetProperty("enterRow"));
                successinptrow = int.TryParse(Console.ReadLine(), out inptrow);
                inptrow--;

                Console.Write(activelang.GetProperty("enterCol"));
                successinptcol = int.TryParse(Console.ReadLine(), out inptcol);
                inptcol--;

                Console.WriteLine(activelang.GetProperty("playerMove").ToString(), inptrow, inptcol);
                if (board[inptrow, inptcol] == '_' && inptrow < 3 && inptcol < 3)
                {
                    board[inptrow, inptcol] = opponent;
                }
            }
            else
            {
                board[inptrow, inptcol] = opponent;
            }

            //Have the player won?
            if (haswon(opponent, board))
            {
                Console.Write(activelang.GetProperty("tabulka").ToString(), board[0, 0], board[0, 1], board[0, 2]);
                Console.Write(activelang.GetProperty("tabulka").ToString(), board[1, 0], board[1, 1], board[1, 2]);
                Console.Write(activelang.GetProperty("tabulka").ToString(), board[2, 0], board[2, 1], board[2, 2]);
                Console.WriteLine(activelang.GetProperty("playerWon"));
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.Clear();
        }
    }
}