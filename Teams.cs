using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WORKINGPROJECT_V3
{

    internal class Team
    {
        private ConsoleColor _teamcolour;//Colour of Team on Board
        private string _teamname = "";//Name of Team
        private int _teamnum;//Team Number
        private int _teamcolournum;//Number corresponding to Colour - Used to prevent the 2 teams from being the same colour
        private int _teamAI;//AI Level - 0 is no AI, Then AI level increases
        public ConsoleColor teamcolour()
        {
            return _teamcolour;
        }
        public string teamname()
        {
            return _teamname;
        }
        public int teamnum()
        {
            return _teamnum;
        }
        public int teamcolournum()
        {
            return _teamcolournum;
        }
        public int teamAI()
        {
            return _teamAI;
        }
        public Team(int teamnumber, string teamname, int teamcolour, int teamAI)
        {
            if (teamcolour == 0)//To Line 66 - Assigns the correspondong colour to the number entered
            {
                _teamcolour = ConsoleColor.Blue;
            }
            if (teamcolour == 1)
            {
                _teamcolour = ConsoleColor.Red;
            }
            if (teamcolour == 2)
            {
                _teamcolour = ConsoleColor.Green;
            }
            if (teamcolour == 3)
            {
                _teamcolour = ConsoleColor.Yellow;
            }
            if (teamcolour == 4)
            {
                _teamcolour = ConsoleColor.Magenta;
            }
            if (teamcolour == 5)
            {
                _teamcolour = ConsoleColor.Cyan;
            }
            if (teamcolour == 6)
            {
                _teamcolour = ConsoleColor.White;
            }
            _teamnum = teamnumber;
            _teamname = teamname;
            _teamcolournum = teamcolour;
            _teamAI = teamAI;
        }
    }
}