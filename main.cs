using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        var map = new Map();
        // plateau
        for (int i = 0; i < 10; i++)
        {
            string line = Console.ReadLine();
            map.Cells.Add(line.ToList());
        }
        // affichage map
        map.Cells.ForEach(l => 
        {
            System.Console.Error.WriteLine(string.Join("",l));
        });
        //robot
        int robotCount = int.Parse(Console.ReadLine());
        for (int i = 0; i < robotCount; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            map.Robots.Add(new Robot
            {
                ID = i,
                X = int.Parse(inputs[0]),
                Y = int.Parse(inputs[1]),
                Direction = inputs[2]
            });
        }
        // affichage robots
        map.Robots.ForEach(r => 
        {
            System.Console.Error.WriteLine(r);
        });

        var myRobot = map.Robots.FirstOrDefault();
        
        string direction = "";
        int nbEmptyCells = map.Cells.Select(c => c.Equals(CellEmpty)).ToList().Count();

        for (int i = 0; i < nbEmptyCells; i++)
        {
            // try go to the right
            if(myRobot.PossibleToGoToRight(map))
            {
                myRobot.Direction = "R";
            }
            // try go to the down
            else if(myRobot.PossibleToGoToDown(map))
            {
                myRobot.Direction = "D";
            }
            // try go to the left
            else if(myRobot.PossibleToGoToLeft(map))
            {
                myRobot.Direction = "L";
            }
            // try go to the up
            else if(myRobot.PossibleToGoToUp(map))
            {
                myRobot.Direction = "U";
            }    
            direction += myRobot.DisplayDirection() + " ";
            myRobot.Move();
        }
        Console.WriteLine(direction.Trim());    
    }
    const char CellVoid = '#';
    const char CellEmpty = '.';
    
    class Map
    {
        // Tableau des cellules
        public List<List<char>> Cells { get; set; } = new List<List<char>>();
        // Liste des robots
        public List<Robot> Robots { get; set; } = new List<Robot>();
    }
    class Robot
    {
        public int ID { get; set; }
        // Coordonnee X du robot
        public int X { get; set; }
        // Coordonnee Y du robot
        public int Y { get; set; }
        // Direction du robot
        public string Direction { get; set; }

        public override string ToString()
        {
            return "ID :"+ ID + " X :" + X + " Y :" + Y + " Direction :" + Direction;
        }
        // return direction string with coord
        public void Move()
        {
            switch (Direction)
            {
                case "R":
                    X++;
                break;
                case "L":
                    X--;
                break;
                case "U":
                    Y--;
                break;
                case "D":
                    Y++;
                break;
                default:
                break;
            }
        }
        public string DisplayDirection()
        {
            return X + " " + Y + " " + Direction;
        }
        // possible to go to the right
        public bool PossibleToGoToRight(Map map)
        {
            return map.Cells[Y][X+1] == CellEmpty;
        }
        // possible to go to the left
        public bool PossibleToGoToLeft(Map map)
        {
            return map.Cells[Y][X-1] == CellEmpty;
        }
        // possible to go to the up
        public bool PossibleToGoToUp(Map map)
        {
            return map.Cells[Y-1][X] == CellEmpty;
        }
        // possible to go to the down
        public bool PossibleToGoToDown(Map map)
        {
            return map.Cells[Y+1][X] == CellEmpty;
        }
    }
}