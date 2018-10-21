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
        //Recuperation de mon robot
        var myRobot = map.Robots.FirstOrDefault();
        var direction = "";        
        System.Console.Error.WriteLine(myRobot);
        var path = map.SearchBestPath(myRobot,new List<Robot>());
        path.ForEach( r => 
        {
            direction += r.DisplayDirection() + " ";
        });

        Console.WriteLine(direction.Trim());    
    }
    const char CellVoid = '#';
    const char CellEmpty = '.';
    const char CellUp = 'U';
    const char CellDown = 'D';
    const char CellLeft = 'L';
    const char CellRight = 'R';
    
    class Map
    {
        // Tableau des cellules
        public List<List<char>> Cells { get; set; } = new List<List<char>>();
        // Liste des robots
        public List<Robot> Robots { get; set; } = new List<Robot>();
        // check if need to change direction
        public bool NeedToChangeDirection(Robot robot)
        {
            var newRobot = robot.Clone() as Robot;
            newRobot.Move();
            return !IsPossibleToMove(newRobot.Coordonates);
        }
        // oriente robot
        public void ChangeToGoodDirection(Robot robot)
        {
            if(PossibleToGoToRight(robot))
            {
                robot.Direction = "R";
            }
            else if(PossibleToGoToLeft(robot))
            {
                robot.Direction = "L";
            }
            else if(PossibleToGoToUp(robot))
            {
                robot.Direction = "U";
            }
            else if(PossibleToGoToDown(robot))
            {
                robot.Direction = "D";
            }
        }
        // Check if cell is empty
        public bool IsCellEmpty(Coordonates coordonates)
        {
            return Cells[coordonates.Y][coordonates.X] == CellEmpty;
        }
        // check if cell is void
        public bool IsCellVoid(Coordonates coordonates)
        {
            return Cells[coordonates.Y][coordonates.X] == CellVoid;
        }
        // check if cell right
        public bool IsCellRight(Coordonates coordonates)
        {
            return Cells[coordonates.Y][coordonates.X] == CellRight;
        }
        // check if cell left
        public bool IsCellLeft(Coordonates coordonates)
        {
            return Cells[coordonates.Y][coordonates.X] == CellLeft;
        }
        // check if cell up
        public bool IsCellUp(Coordonates coordonates)
        {
            return Cells[coordonates.Y][coordonates.X] == CellUp;
        }
        // check if cell down
        public bool IsCellDown(Coordonates coordonates)
        {
            return Cells[coordonates.Y][coordonates.X] == CellDown;
        }
        // Check if Coordonate is into the map
        public bool IsInTheMap(Coordonates coordonates)
        {
            return coordonates.Y < Cells.Count() 
                && coordonates.Y > -1 
                && coordonates.X > -1 
                && coordonates.X < Cells[coordonates.Y].Count();
        }
        // Search path
        public List<Robot> SearchBestPath(Robot robot, List<Robot> path)
        {
            if(!PossibleToGoToRight(robot) 
                && !PossibleToGoToLeft(robot)
                && !PossibleToGoToDown(robot)
                && !PossibleToGoToUp(robot))
            {
                return path;
            }
            // variable du meileur chemin
            var bestPath = path;
            // possible d'aller a droite ? ou case fleche droite
            if(PossibleToGoToRight(robot) || IsCellRight(robot.Coordonates))
            {
                robot.Direction = "R";
            }
            // possible d'aller en haut ? ou fleche haut
            else if(PossibleToGoToUp(robot) || IsCellUp(robot.Coordonates))
            {
                robot.Direction = "U";
            }
            // possible d'aller a gauche ? ou flech gauche
            else if(PossibleToGoToLeft(robot) || IsCellLeft(robot.Coordonates))
            {
                robot.Direction = "L";
            }
            // possible d'aller en bas ? ou fleche bas
            else if(PossibleToGoToDown(robot) || IsCellDown(robot.Coordonates))
            {
                robot.Direction = "D";
            }
            System.Console.Error.WriteLine("debug :" + robot);
            var clone = robot.Clone() as Robot;
            // simulate right move
            clone.Move();
            path.Add(robot);
            System.Console.Error.WriteLine("move :" + clone);
            path = SearchBestPath(clone, path);

            if(bestPath.Count() < path.Count())
            {
                bestPath = path;
            }
            return bestPath;
        }
        // check if possible to move on
        public bool IsPossibleToMove(Coordonates newCoordinates)
        {
            return IsInTheMap(newCoordinates)
                && !IsCellVoid(newCoordinates);
        }
        // possible to go to the right
        public bool PossibleToGoToRight(Robot robot)
        {
            var newCoordinates = new Coordonates{Y = robot.Coordonates.Y, X=robot.Coordonates.X+1};
            return IsPossibleToMove(newCoordinates) && !robot.LastCoordinates.Contains(newCoordinates);
        }
        // possible to go to the left
        public bool PossibleToGoToLeft(Robot robot)
        {
            var newCoordinates = new Coordonates{Y = robot.Coordonates.Y, X=robot.Coordonates.X-1};
            return IsPossibleToMove(newCoordinates) && !robot.LastCoordinates.Contains(newCoordinates);
        }
        // possible to go to the up
        public bool PossibleToGoToUp(Robot robot)
        {
            var newCoordinates = new Coordonates{Y = robot.Coordonates.Y-1, X=robot.Coordonates.X};
            return IsPossibleToMove(newCoordinates) && !robot.LastCoordinates.Contains(newCoordinates);
        }
        // possible to go to the down
        public bool PossibleToGoToDown(Robot robot)
        {
            var newCoordinates = new Coordonates{Y = robot.Coordonates.Y+1, X=robot.Coordonates.X};
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
    class Robot : ICloneable
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
        // methodes iclonable
        public object Clone()
        {
            return new Robot
            {
                ID = this.ID,
                Direction = this.Direction, 
                Coordonates = this.Coordonates,
                LastCoordinates = this.LastCoordinates
            };
        }
    }
}