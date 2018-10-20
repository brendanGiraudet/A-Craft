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
        for (int i = 0; i < map.Cells.Count; i++)
        {
            for (int j = 0; j < map.Cells[i].Count; j++)
            {
                System.Console.Error.WriteLine("i:" + i + " j:"+ j + " " + map.Cells[i][j]);    
            }
        }
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
        // try go to the right
        if(map.Cells[myRobot.Y][myRobot.X+1] == CellEmpty)
        {
            myRobot.Direction = "R";
            myRobot.X++;
        }
        // try go to the down
        else if(map.Cells[myRobot.Y+1][myRobot.X] == CellEmpty)
        {
            myRobot.Direction = "D";
            myRobot.Y++;
        }
        // try go to the left
        else if(map.Cells[myRobot.Y][myRobot.X-1] == CellEmpty)
        {
            myRobot.Direction = "L";
            myRobot.X--;
        }
        // try go to the up
        else if(map.Cells[myRobot.Y-1][myRobot.X] == CellEmpty)
        {
            myRobot.Direction = "U";
            myRobot.Y--;
        }
        
        Console.WriteLine(myRobot.DisplayDirection());
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
        public string DisplayDirection()
        {
            return X + " " + Y + " " + Direction;
        }
    }
}