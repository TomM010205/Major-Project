using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WORKINGPROJECT_V3
{
    internal class ScoreBoard
    {
        private int[,] Board = new int[15, 15];
        public ScoreBoard()//Creates Board
        {
            Newboard();
        }
        public void UpdateTile(int row, int col, int value)//Updates a tile with the value of the games Position/Move
        {
            Board[row, col] = Board[row, col] + value;
        }
        public int Returnvalue(int row, int col)// Returns the value of a position on the board
        {
            return Board[row, col];
        }
        public void Newboard()//Resets Board
        {
            for (int Row = 0; Row < 15; Row++)
            {
                for (int Column = 0; Column < 15; Column++)
                {
                    Board[Row, Column] = 0;
                }
            }
        }
        public void Printboard()//Prints values, isnt necessary but I used for error checking
        {
            for (int Row = 0; Row < 15; Row++)
            {
                for (int Column = 0; Column < 15; Column++)
                {
                    if (Board[Row, Column] != -1000000000)
                    {
                        Console.Write(" {0} ", Board[Row, Column]);
                    }
                    else
                    {
                        Console.Write('a');
                    }

                }
                Console.WriteLine("");
            }
        }
    }
}

