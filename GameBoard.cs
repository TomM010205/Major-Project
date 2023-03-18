using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WORKINGPROJECT_V3
{
    class GameBoard
    {
        private char[,] Board = new char[15, 15];
        public GameBoard()
        {
            Newboard();
        }
        public void PrintBoard(Team Team1, Team Team2)
        {
            Console.WriteLine();
            Console.WriteLine("The board looks like this: ");
            Console.WriteLine();
            Console.Write("  ");
            for (int Column = 0; Column < 15; Column++)//Column Formatting
            {
                if (Column < 10)
                {
                    Console.Write(" ");
                }
                Console.Write(" " + (Column + 1) + " ");
            }
            Console.WriteLine();
            for (int Row = 0; Row < 15; Row++)
            {
                if (Row < 9)//Row Formatting
                {
                    Console.Write(" ");
                }
                Console.Write((Row + 1) + " ");
                for (int Column = 0; Column < 15; Column++)
                {
                    if (Board[Row, Column] == '-')// No marble placed
                    {
                        Console.Write("   ");
                    }
                    else if (Board[Row, Column] == Team1.teamname()[0])//Team 1 Marble
                    {
                        Console.BackgroundColor = Team1.teamcolour();
                        Console.Write("   ", Team1.teamname()[0]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else if (Board[Row, Column] == Team2.teamname()[0])//Team 2 Marble
                    {
                        Console.BackgroundColor = Team2.teamcolour();
                        Console.Write("   ", Team2.teamname()[0]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write(Board[Row, Column]);
                    }
                    if (Column != 16)
                    {
                        Console.Write("|");
                    }
                }
                Console.WriteLine();
            }
        }
        public void PlaceMarble(Team Team, int row, int col)
        {
            Board[row, col] = Team.teamname()[0];//Changes the row col to the first char of a teams name
        }
        public bool PlaceMarblecheck(int row, int col)
        {
            if (row >= 0 && row < 15 && col >= 0 && col < 15 && Board[row, col] == '-')// Chechls if the marble is being placed out of bounds
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public char Returnvalue(int row, int col)
        {
            return Board[row, col];// returns the value at the row and col
        }
        public bool CheckWin(Team team, int Row, int Col)
        {
            int count = 1;//To Line 185 - Checks all possible directions and calls Count in the direction
            if (Row != 0 && Col != 14)
            {
                if (Board[Row - 1, Col + 1] == team.teamname()[0])
                {
                    count = 2;
                    if (Count(Row - 1, Col + 1, count, -1, +1, team.teamname()[0]) >= 5)
                    {
                        return true;
                    }

                }
            }
            if (Row != 0)
            {
                if (Board[Row - 1, Col] == team.teamname()[0])
                {
                    count = 2;
                    if (Count(Row - 1, Col, count, -1, 0, team.teamname()[0]) >= 5)
                    {
                        return true;
                    }
                }

            }
            if (Row != 0 && Col != 0)
            {
                if (Board[Row - 1, Col - 1] == team.teamname()[0])
                {
                    count = 2;
                    if (Count(Row - 1, Col - 1, count, -1, -1, team.teamname()[0]) >= 5)
                    {
                        return true;
                    }
                }

            }
            if (Row != 14 && Col != 14)
            {
                if (Board[Row + 1, Col + 1] == team.teamname()[0])
                {
                    count = 2;
                    if (Count(Row + 1, Col + 1, count, 1, 1, team.teamname()[0]) >= 5)
                    {
                        return true;
                    }
                }

            }
            if (Row != 14)
            {
                if (Board[Row + 1, Col] == team.teamname()[0])
                {
                    count = 2;
                    if (Count(Row + 1, Col, count, 1, 0, team.teamname()[0]) >= 5)
                    {
                        return true;
                    }
                }
            }
            if (Row != 14 && Col != 0)
            {
                if (Board[Row + 1, Col - 1] == team.teamname()[0])
                {
                    count = 2;
                    if (Count(Row + 1, Col - 1, count, 1, -1, team.teamname()[0]) >= 5)
                    {
                        return true;
                    }
                }
            }
            if (Col != 14)
            {
                if (Board[Row, Col + 1] == team.teamname()[0])
                {
                    count = 2;
                    if (Count(Row, Col + 1, count, 0, 1, team.teamname()[0]) >= 5)
                    {
                        return true;
                    }
                }
            }
            if (Col != 0)
            {
                if (Board[Row, Col - 1] == team.teamname()[0])
                {
                    count = 2;
                    if (Count(Row, Col - 1, count, 0, -1, team.teamname()[0]) >= 5)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public int neighbourSquares(int row, int col)
        {
            int count = 0;// Method was used to determine which position to play if there were multliple moves with the highest score. Not used is silly.
            for (int d = -1; d < 2; d++)
            {
                for (int e = -1; e < 2; e++)
                {
                    if (d != 0 || e != 0)
                    {
                        if (row + d >= 0 && row + d < 15 && col + e >= 0 && col + e < 15 && (Board[row + d, col + e] != '-'))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        public int Count(int Row, int Col, int count, int i, int j, char s)// Counts in forward and backwards direction, as if a marble is placed in the middle of a 5 line the method must count in both directions
        {
            int temprow = Row + i;
            int tempcol = Col + j;
            {
                if (neighbourSquares(Row, Col) != 0)
                {
                    while (temprow >= 0 && temprow < 15 && tempcol >= 0 && tempcol < 15 && Returnvalue(temprow, tempcol) == s)
                    {
                        temprow = temprow + i;
                        tempcol = tempcol + j;
                        count++;
                    }
                    if (temprow < 0 || temprow > 14 || tempcol < 0 || tempcol > 14 || Returnvalue(temprow, tempcol) != s)
                    {
                        temprow = temprow - (count + 1) * i;
                        tempcol = tempcol - (count + 1) * j;
                        while (temprow >= 0 && temprow < 15 && tempcol >= 0 && tempcol < 15 && Returnvalue(temprow, tempcol) == s)
                        {
                            temprow = temprow - i;
                            tempcol = tempcol - j;
                            count++;
                        }
                    }
                }
            }
            return count;
        }
        public void Newboard()//resets the board
        {
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 15; col++)
                {
                    Board[row, col] = '-';
                }
            }
        }
        public void Copy(GameBoard Copyboard)//This method is used to get around a bug in Visual Studios. The Boards are held as a single memory Location. If you use Board1 = Board2 and then edit Board1, Board2 will also change.
        {
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 15; col++)
                {
                    Board[row, col] = Copyboard.Returnvalue(row, col);
                }
            }
        }
    }
}