using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WORKINGPROJECT_V3
{
    class Program
    {
        static public void Main(string[] args)
        {
            Team[] Teams = new Team[2];//2 Teams
            GameBoard Board = new GameBoard();//Creates Board
            int MenuOption = 0;
            while (MenuOption != 9)
            {
                DisplayMainMenu();
                MenuOption = GetMenuChoice();
                if (MenuOption == 1)
                {
                    Console.Clear();
                    DisplayModeMenu();
                    MenuOption = GetMenuChoice();
                    while(MenuOption != 9)
                    {
                        Console.Clear();
                        ModeMenuLoop(ref MenuOption, Teams[0], Teams[1], ref Board);                        
                    }
                    
                }
                if (MenuOption == 2)
                {
                    Console.WriteLine("Rule 1: First Marble must always be centre of the board (8,8) \nRule 2: Get 5 in a row to win \nRule 3: Have Fun");
                }   
                
            }
            Console.WriteLine("Thank You for Playing");
        }
        private static void DisplayMainMenu()
        {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine();
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Rules");
            Console.WriteLine("9. Quit");
            Console.WriteLine();
        }
        private static void DisplayModeMenu()
        {
            Console.WriteLine("MODE MENU");
            Console.WriteLine();
            Console.WriteLine("1. Player VS Player");
            Console.WriteLine("2. Player VS AI");
            Console.WriteLine("3. AI VS AI");
            Console.WriteLine();
        }
        private static int GetAILevel()
        {
            int Choice = 0;
            Console.WriteLine("Select AI Level");
            Console.WriteLine();
            Console.WriteLine("1. Easy");
            Console.WriteLine("2. Medium");
            Console.WriteLine("3. Hard");
            Choice = Convert.ToInt32(Console.ReadLine());
            return Choice;
        }
        public static void ModeMenuLoop(ref int MenuOption, Team Team1, Team Team2, ref GameBoard Board)
        {
            if (MenuOption == 1)//Player VS Player
            {
                Console.Clear();
                Console.WriteLine("Team 1");
                Team1 = new Team(1, GetTeamName('*'), GetTeamColour(8), 0);
                Console.Clear();
                Console.WriteLine("Team 2");
                Team2 = new Team(2, GetTeamName(Team1.teamname()[0]), GetTeamColour(Team1.teamcolournum()), 0);
                GameCall(ref Board, Team1, Team2 , ref MenuOption);
            }
            else if (MenuOption == 2)//Player VS AI
            {
                Console.Clear();
                Console.WriteLine("Team 1");
                Team1 = new Team(1, GetTeamName('*'), GetTeamColour(8), 0);
                Console.Clear();
                Console.WriteLine("Team 2");
                Team2 = new Team(2, GetTeamName(Team1.teamname()[0]), GetTeamColour(Team1.teamcolournum()), GetAILevel());
                GameCall(ref Board, Team1, Team2, ref MenuOption);
            }
            else if (MenuOption == 3)//AI VS AI
            {
                Console.Clear();
                Console.WriteLine("Team 1");
                Team1 = new Team(1, GetTeamName('*'), GetTeamColour(8), GetAILevel());
                Console.Clear();
                Console.WriteLine("Team 2");
                Team2 = new Team(2, GetTeamName(Team1.teamname()[0]), GetTeamColour(Team1.teamcolournum()), GetAILevel());
                GameCall(ref Board, Team1, Team2, ref MenuOption);
            }
            else
            {
                Console.WriteLine("Not a given Option");
                DisplayModeMenu();
                MenuOption = GetMenuChoice();
            }
        }
        public static int GetMenuChoice()
        {
            int Choice = 0;
            bool intcheck;
            do
            {
                intcheck = true;
                Console.Write("Please enter your choice: ");
                try
                {
                    Choice = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Please Enter an Integer");
                    intcheck = false;
                }
            }
            while (!intcheck);            
            //Choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            return Choice;
        }
        public static string GetTeamName(char a)
        {
            string tempteamname = " ";
            do
            {
                Console.WriteLine("Enter Team Name");
                tempteamname = Console.ReadLine() + tempteamname;
                if (tempteamname[0] == a)
                {
                    Console.Write("Enter a Username with a different first letter \n");// Prevents the two teams from having the same first character as the Board wouldn't be able to differentiate between the two
                    tempteamname = GetTeamName(a);
                }
            }
            while (tempteamname == null);
            return tempteamname;
        }
        public static int GetTeamColour(int n)
        {
            Console.WriteLine("0. Blue");
            Console.WriteLine("1. Red");
            Console.WriteLine("2. Green");
            Console.WriteLine("3. Yellow");
            Console.WriteLine("4. Magenta");
            Console.WriteLine("5. Cyan");
            Console.WriteLine("6. White");
            int tempteamcolor = 7;
            do
            {
                Console.WriteLine("Enter the number of Team Colour");
                try
                {
                    tempteamcolor = int.Parse(Console.ReadLine() + "");
                }
                catch
                {
                    Console.WriteLine("Enter a Number between 0 and 6");
                    tempteamcolor = 7;
                }
                if (tempteamcolor == n && tempteamcolor != 8)//Prevents User from entering the same colour as opponent
                {
                    Console.WriteLine("Enter a colour different to Team 1");
                    tempteamcolor = 7;
                }
            }
            while (tempteamcolor < 0 || tempteamcolor > 6);
            return tempteamcolor;

        }
        public static void AnotherGame()
        {
            Console.WriteLine("0: Play Again \n9: Exit");
            
        }
        public static void GameCall(ref GameBoard Board, Team Team1, Team Team2, ref int MenuOption)
        {
            int gamecount = 0;
            PlayGame(ref Board, Team1, Team2);
            gamecount++;
            while (MenuOption != 9)
            {
                AnotherGame();
                MenuOption = GetMenuChoice();
                if (MenuOption == 0)
                {
                    if(gamecount %2 == 1)
                    {
                        PlayGame(ref Board, Team2, Team1);
                    }
                    if (gamecount % 2 == 0)
                    {
                        PlayGame(ref Board, Team1, Team2);
                    }
                    gamecount++;
                }
                else if (MenuOption != 9)
                {
                    Console.WriteLine("Not a given option");
                }
            }
        }
        public static void PlayGame(ref GameBoard Board, Team Team1, Team Team2)
        {
            bool GameWon1 = false;
            bool GameWon2 = false;
            bool movecheck = false;
            int row = 0;
            int col = 0;
            int teamturn = 1;
            while (GameWon1 == false && GameWon2 == false)
            {
                Console.Clear();
                Console.WriteLine("Marble placed at ({0},{1})", row, col);//Mainly for AI moves but is useful
                movecheck = false;
                Board.PrintBoard(Team1, Team2);
                if (teamturn % 2 == 1)//Team1 Turn
                {
                    Console.WriteLine("{0}'s turn", Team1.teamname());
                    //Thread.Sleep(1000);
                    do
                    {
                        if (Team1.teamAI() != 0)//AI Move
                        {
                            MiniMaxcall(ref row, ref col, Board, Team1, Team2);
                            if (row == -100)
                            {
                                GameWon1 = true;
                                GameWon2 = true;
                            }
                            movecheck = true;
                        }
                        else
                        {
                            GetRowCol(ref row, ref col);
                            movecheck = Board.PlaceMarblecheck(row - 1, col - 1);//because board is 0-14 x 0-14 but printboard is 1-15x1-15, the user will enter the row and column one number higher *
                            if (!movecheck)
                            {
                                if (row <= 0 || row > 14 || col <= 0 || col > 14)
                                {
                                    Console.WriteLine("Outside of Board");
                                }
                                else if (Board.Returnvalue(row - 1, col - 1) != '-')
                                {
                                    Console.WriteLine("Marble Already played here");
                                }
                            }
                            if((row != 8 || col != 8) && teamturn == 1)
                            {
                                Console.Write("1st Move Rule \n Must play (8,8)");
                                movecheck = false;
                            }
                        }
                    }
                    while (movecheck == false);
                    if (Team1.teamAI() != 0 && (GameWon1 == false || GameWon2 == false))
                    {
                        Board.PlaceMarble(Team1, row, col);
                        GameWon1 = Board.CheckWin(Team1, row, col);
                        row += 1;
                        col += 1;
                    }
                    else if (Team1.teamAI() == 0)
                    {
                        Board.PlaceMarble(Team1, row - 1, col - 1);//*
                        GameWon1 = Board.CheckWin(Team1, row - 1, col - 1);
                    }
                }
                else//Team2 turn
                {
                    Console.WriteLine("{0}'s turn", Team2.teamname());
                    //Thread.Sleep(1000); at a low AI level it plays very fast
                    do
                    {
                        if (Team2.teamAI() != 0)
                        {
                            MiniMaxcall(ref row, ref col, Board, Team2, Team1);
                            movecheck = Board.PlaceMarblecheck(row, col);
                            if (Board.PlaceMarblecheck(row, col) == false)
                            {
                                movecheck = true;
                                GameWon1 = true;
                                GameWon2 = true;
                            }
                        }
                        else
                        {
                            GetRowCol(ref row, ref col);
                            movecheck = Board.PlaceMarblecheck(row - 1, col - 1);//because board is 0-14 x 0-14 but printboard is 1-15x1-15, the user will enter the row and column one number higher *
                            if (!movecheck)
                            {
                                if (row <= 0 || row > 14 || col <= 0 || col > 14)
                                {
                                    Console.WriteLine("Outside of Board");
                                }
                                else if (Board.Returnvalue(row - 1, col - 1) != '-')
                                {
                                    Console.WriteLine("Marble Already played here");
                                }
                            }
                        }
                    }
                    while (movecheck == false);
                    if (Team2.teamAI() != 0)
                    {
                        Board.PlaceMarble(Team2, row, col);
                        GameWon2 = Board.CheckWin(Team2, row, col);
                        row += 1;
                        col += 1;
                    }
                    else
                    {
                        Board.PlaceMarble(Team2, row - 1, col - 1);;
                        GameWon2 = Board.CheckWin(Team2, row - 1, col - 1);
                    }
                }
                teamturn++;
            }
            Board.PrintBoard(Team1, Team2);
            if (GameWon1 && !GameWon2)//If team1 won
            {
                Console.WriteLine("{0} Wins", Team1.teamname());
            }
            else if (GameWon2 && !GameWon1)//If team2 won
            {
                Console.WriteLine("{0} Wins", Team2.teamname());
            }
            else// if board fills up
            {
                Console.WriteLine("Draw");
            }
            Board.Newboard();//resets board
        }
        public static void GetRowCol(ref int row, ref int col)//This receives user input
        {
            bool rowcheck = true;
            bool colcheck = true;
            Console.WriteLine();
            do
            {
                try
                {
                    Console.WriteLine("Please enter row: ");
                    row = int.Parse(Console.ReadLine() + "");
                }
                catch
                {
                    Console.WriteLine("Please Enter an Integer value");
                    row = int.Parse(Console.ReadLine() + "");
                    rowcheck = false;
                }
                try
                {
                    Console.WriteLine("Please enter column: ");
                    col = int.Parse(Console.ReadLine() + "");
                }
                catch
                {
                    Console.WriteLine("Please Enter an Integer value");
                    col = int.Parse(Console.ReadLine() + "");
                    colcheck = false;
                }
            }
            while (!rowcheck && !colcheck);
        }



        public static void MiniMaxcall(ref int row, ref int col, GameBoard Board, Team team1, Team team2)//Sets up variables to be used in the MiniMax AI and calls MiniMax()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            GameBoard AIBoard = new GameBoard();
            int bestvalue = 0;
            AIBoard.Copy(Board);
            Console.WriteLine("Call");
            MiniMax(ref row, ref col, AIBoard, scoreBoard, 0, team1, team2, team1.teamAI(), ref bestvalue);
            scoreBoard.Newboard();
        }
        public static void MiniMax(ref int row, ref int col, GameBoard Board, ScoreBoard scoreBoard, int c, Team team1, Team team2, int capvalue, ref int bestvalue)
        {
            GameBoard tempBoard = new GameBoard();
            ScoreBoard currentScoreBoard = new ScoreBoard();
            List<int> randomrow = new List<int>();//Used for random move selection when multpile moves have the highscore
            List<int> randomcol = new List<int>();
            Random RNG = new Random();
            int temprow;
            int tempcol;
            int random;
            int highscore = 0;
            int highscorerow = -100;
            int highscorecol = -100;
            c += 1;
            currentScoreBoard.Newboard();
            for (row = 0; row < 15; row++)//Checks possible win for the playing AI, more priority than opponent win
            {
                for (col = 0; col < 15; col++)
                {
                    if (Board.PlaceMarblecheck(row, col) && CheckSurround(Board, row, col) != 0)
                    {
                        if (Board.CheckWin(team1, row, col))
                        {
                            currentScoreBoard.UpdateTile(row, col, 5000000);
                            return;
                        }
                    }
                }
            }
            for (row = 0; row < 15; row++)//Checks possible win for the opponent more piority than the open ended 4 in a row
            {

                for (col = 0; col < 15; col++)
                {
                    if (Board.PlaceMarblecheck(row, col) && CheckSurround(Board, row, col) != 0)
                    {
                        if (Board.CheckWin(team2, row, col))
                        {
                            currentScoreBoard.UpdateTile(row, col, 2500000);
                            return;
                        }
                    }
                }
            }
            for (row = 0; row < 15; row++)//Checks open ended 4 in a row for AI, more priority than the opponents open ended row
            {
                for (col = 0; col < 15; col++)
                {
                    if (Board.PlaceMarblecheck(row, col) && CheckSurround(Board, row, col) != 0)
                    {
                        if (QuadLineWinCheck(Board, row, col, team1))
                        {
                            currentScoreBoard.UpdateTile(row, col, 1000000);
                            return;
                        }
                    }
                }
            }
            for (row = 0; row < 15; row++)//Checks open ended 4 in a row for opponent
            {
                for (col = 0; col < 15; col++)
                {
                    if (Board.PlaceMarblecheck(row, col) && CheckSurround(Board, row, col) != 0)
                    {
                        if (QuadLineWinCheck(Board, row, col, team2))
                        {
                            currentScoreBoard.UpdateTile(row, col, 500000);
                            return;
                        }
                    }
                }
            }
            for (row = 0; row < 15; row++)
            {
                for (col = 0; col < 15; col++)
                {
                    temprow = row;
                    tempcol = col;
                    if (CheckSurround(Board, row, col) == 0 && Board.PlaceMarblecheck(row, col))//Ignores any position that a marble has been placed in or outised of the playing area **
                    {
                        currentScoreBoard.UpdateTile(row, col, -1000000000);
                    }
                    else if (CheckSurround(Board, row, col) != 0 && Board.PlaceMarblecheck(row, col))
                    {
                        tempBoard.Copy(Board);
                        tempBoard.PlaceMarble(team1, row, col);// another bug in Visual Studios - the Boards are passed as reference parameters into any method so a tempboard copied must be used
                        if (c < capvalue)
                        {
                            MiniMax(ref temprow, ref tempcol, tempBoard, currentScoreBoard, c, team2, team1, capvalue, ref bestvalue);//REcursively calls the MiniMax until it reaches the depth
                            currentScoreBoard.UpdateTile(temprow, tempcol, bestvalue);
                        }
                        else
                        {
                            currentScoreBoard.UpdateTile(row, col, TLineCount(Board, row, col, team1) + TLineCount(Board, row, col, team2));//at the correct depth the Value of position is caculated
                                                                                                                                            //Console.WriteLine("{0}", currentScoreBoard.Returnvalue(row, col)); - ERROR CHECKING
                        }
                    }
                    if (currentScoreBoard.Returnvalue(row, col) > highscore && Board.PlaceMarblecheck(row, col) && CheckSurround(Board, row, col) != 0)
                    {
                        highscore = currentScoreBoard.Returnvalue(row, col);//if the current value is greater than the bestvalue the random list is reset and the new row and col added to the random lists
                        highscorerow = row;
                        highscorecol = col;
                        randomcol.Clear();
                        randomrow.Clear();
                        randomcol.Add(tempcol);
                        randomrow.Add(temprow);
                    }
                    else if (currentScoreBoard.Returnvalue(row, col) == highscore && Board.PlaceMarblecheck(row, col) && CheckSurround(Board, row, col) != 0)//If the current value is equal to highscore the row and col are added to reandom lists
                    {
                        randomrow.Add(row);
                        randomcol.Add(col);
                    }

                }
            }
            if (randomrow.Count() == 1)//if there is only one move with the best score, that move will be played
            {
                row = highscorerow;
                col = highscorecol;
            }
            else if (randomrow.Count() > 1)//if there is more than one move with the best score, one of the moves will be chosen at random
            {
                random = RNG.Next(randomrow.Count());
                row = randomrow[random];
                col = randomcol[random];
            }
            else//if there is no moves with any score, the AI will play in the centre
            {
                row = 7;
                col = 7;
            }
            scoreBoard.UpdateTile(row, col, highscore);
            Thread.Sleep(5000/capvalue);
            return;

        }
        static bool QuadLineWinCheck(GameBoard Board, int row, int col, Team team1)
        {
            GameBoard AIBoard = new GameBoard();
            AIBoard.Copy(Board);
            AIBoard.PlaceMarble(team1, row, col);
            int count;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    count = 1;
                    if (i != 0 || j != 0)
                    {
                        if (i + row >= 0 && i + row < 15 && col + j >= 0 && col + j < 15)
                        {
                            count = AIBoard.Count(row, col, count, i, j, team1.teamname()[0]);
                            if (count == 4)
                            {
                                while (i + row >= 0 && i + row < 15 && col + j >= 0 && col + j < 15 && AIBoard.Returnvalue(row, col) == team1.teamname()[0])
                                {
                                    row = row + i;
                                    col = col + j;
                                }
                                if (i + row >= 0 && i + row < 15 && col + j >= 0 && col + j < 15 && AIBoard.Returnvalue(row, col) == '-')
                                {
                                    row = row - i;
                                    col = col - j;
                                    while (row - i >= 0 && row - i < 15 && col - j >= 0 && col - j < 15 && AIBoard.Returnvalue(row, col) == team1.teamname()[0])
                                    {
                                        row = row - i;
                                        col = col - j;
                                    }
                                    if (row >= 0 && row < 15 && col >= 0 && col < 15 && AIBoard.Returnvalue(row, col) == '-')
                                    {
                                        return true;
                                    }

                                }
                            }
                        }
                    }
                }

            }
            return false;
        }
        static int TLineCount(GameBoard Board, int row, int col, Team team)
        {
            GameBoard AIBoard = new GameBoard();
            AIBoard.Copy(Board);
            AIBoard.PlaceMarble(team, row, col);
            int tcount = 0;
            int count;
            int count4 = 0;
            if (AIBoard.neighbourSquares(row, col) != 0)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        count = 1;
                        if (i != 0 || j != 0)
                        {
                            count = AIBoard.Count(row, col, count, i, j, team.teamname()[0]);
                            tcount = tcount + (3 * count * count) + (13 * count) - (count * count * count) - 15;
                            if (count == 4)
                            {
                                count4++;
                            }
                        }
                    }
                }
                count4 /= 2;
                if (count4 > 1)
                {
                    tcount = tcount * count4;
                }
                return tcount / 2;
            }
            else return 0;
        }

        static int CheckSurround(GameBoard AIBoard, int row, int col) //After watching the few professional Renju games accessible I realised they do not play a move outside of this range
        {
            int surround = 0;
            for (int i = -2; i < 3; i++)
            {
                for (int j = -2; j < 3; j++)
                {
                    if (i != 0 || j != 0)
                    {

                        if (row + i <= 14 && col + j <= 14 && row + i >= 0 && col + j >= 0 && AIBoard.Returnvalue(row + i, col + j) != '-')
                        {
                            surround = 1;
                        }
                    }
                }
            }
            return surround;
        }
    }
}