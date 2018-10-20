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
            var robot = new Robot
            {
                ID = i,
                Direction = inputs[2]
            };
            robot.Coordonates.X = int.Parse(inputs[0]);
            robot.Coordonates.Y = int.Parse(inputs[1]);
            map.Robots.Add(robot);
        }
        // affichage robots
        map.Robots.ForEach(r => 
        {
            System.Console.Error.WriteLine(r);
        });
        //Recuperation de mon robot
        var myRobot = map.Robots.FirstOrDefault();
        
        string direction = "";
        int nbEmptyCells = map.Cells.Select(c => c.Equals(CellEmpty)).ToList().Count();

        for (int i = 0; i < nbEmptyCells; i++)
        {
            // try go to the right
            if(map.PossibleToGoToRight(myRobot))
            {
                myRobot.Direction = "R";
            }
            // try go to the down
            else if(map.PossibleToGoToDown(myRobot))
            {
                myRobot.Direction = "D";
            }
            // try go to the left
            else if(map.PossibleToGoToLeft(myRobot))
            {
                myRobot.Direction = "L";
            }
            // try go to the up
            else if(map.PossibleToGoToUp(myRobot))
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
        // Check if cell is empty
        public bool IsCellEmpty(Coordonates coordonates)
        {
            return Cells[coordonates.Y][coordonates.X] == CellEmpty;
        }
        // Check if Coordonate is into the map
        public bool IsInTheMap(Coordonates coordonates)
        {
            return coordonates.Y < Cells.Count() 
                && coordonates.Y > -1 
                && coordonates.X > -1 
                && coordonates.X < Cells[coordonates.Y].Count();
        }
        // check if possible to move on
        public bool IsPossibleToMove(Coordonates newCoordinates)
        {
            return IsInTheMap(newCoordinates)
                && IsCellEmpty(newCoordinates);
        }
        // possible to go to the right
        public bool PossibleToGoToRight(Robot robot)
        {
            var newCoordinates = new Coordonates{Y = robot.Coordonates.Y, X=robot.Coordonates.X+1};
            System.Console.Error.WriteLine("ToRight " + newCoordinates);
            System.Console.Error.WriteLine(IsPossibleToMove(newCoordinates));
            return IsPossibleToMove(newCoordinates) && !robot.LastCoordinates.Contains(newCoordinates);
        }
        // possible to go to the left
        public bool PossibleToGoToLeft(Robot robot)
        {
            var newCoordinates = new Coordonates{Y = robot.Coordonates.Y, X=robot.Coordonates.X-1};
            System.Console.Error.WriteLine("ToLeft " + newCoordinates);
            System.Console.Error.WriteLine(IsPossibleToMove(newCoordinates));
            return IsPossibleToMove(newCoordinates) && !robot.LastCoordinates.Contains(newCoordinates);
        }
        // possible to go to the up
        public bool PossibleToGoToUp(Robot robot)
        {
            var newCoordinates = new Coordonates{Y = robot.Coordonates.Y-1, X=robot.Coordonates.X};
            System.Console.Error.WriteLine("ToUp " + newCoordinates);
            System.Console.Error.WriteLine(IsPossibleToMove(newCoordinates));
            return IsPossibleToMove(newCoordinates) && !robot.LastCoordinates.Contains(newCoordinates);
        }
        // possible to go to the down
        public bool PossibleToGoToDown(Robot robot)
        {
            var newCoordinates = new Coordonates{Y = robot.Coordonates.Y+1, X=robot.Coordonates.X};
            System.Console.Error.WriteLine("ToDown " + newCoordinates);
            System.Console.Error.WriteLine(IsPossibleToMove(newCoordinates));
            return IsPossibleToMove(newCoordinates) && !robot.LastCoordinates.Contains(newCoordinates);
        }
    }
    class Coordonates : IEquatable<Coordonates>
    {
        public string SSN { get; set; } = Guid.NewGuid().ToString();
        // Coordonnee X
        public int X { get; set; }
        // Coordonnee Y
        public int Y { get; set; }
        // Methods of IEquatable
        public bool Equals(Coordonates other)
        {
            if (other == null) return false;
            return (this.X.Equals(other.X) && this.Y.Equals(other.Y));
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var objAsPart = obj as Coordonates;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return SSN.GetHashCode();
        }
        public static bool operator == (Coordonates coordonates1, Coordonates coordonates2)
        {
            if (((object)coordonates1) == null || ((object)coordonates2) == null)
                return Object.Equals(coordonates1, coordonates2);

            return coordonates1.Equals(coordonates2);
        }
        public static bool operator != (Coordonates coordonates1, Coordonates coordonates2)
        {
            if (((object)coordonates1) == null || ((object)coordonates2) == null)
                return ! Object.Equals(coordonates1, coordonates2);

            return ! coordonates1.Equals(coordonates2);
        }
        public override string ToString()
        {
            return " X :" + X + " Y :" + Y;
        }
    }
    class Robot
    {
        public int ID { get; set; }
        // Direction du robot
        public string Direction { get; set; }
        // Coordonates X and Y 
        public Coordonates Coordonates { get; set; } = new Coordonates();
        // List of last Coordonates
        public List<Coordonates> LastCoordinates { get; set; } = new List<Coordonates>();

        public override string ToString()
        {
            return "ID :"+ ID + " X :" + Coordonates.X + " Y :" + Coordonates.Y
                + " Direction :" + Direction;
        }
        // MaJ coordonates
        public void Move()
        {
            var newCoordinates = new Coordonates{X = this.Coordonates.X, Y= this.Coordonates.Y};
            LastCoordinates.Add(Coordonates);
            Coordonates = newCoordinates;
            switch (Direction)
            {
                case "R":
                    newCoordinates.X++;
                break;
                case "L":
                    newCoordinates.X--;
                break;
                case "U":
                    newCoordinates.Y--;
                break;
                case "D":
                    newCoordinates.Y++;
                break;
                default:
                break;
            }
        }
        // display for the Console writeline
        public string DisplayDirection()
        {
            return Coordonates.X + " " + Coordonates.Y + " " + Direction;
        }
    }
}