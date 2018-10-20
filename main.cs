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
class Solution
{
    static void Main(string[] args)
    {
        // opponent
        string opBowls = Console.ReadLine();
        var opList = opBowls.Split(' ').ToList().Select(val => Convert.ToInt32(val)).ToList();
        // me
        string myBowls = Console.ReadLine();
        var myList = myBowls.Split(' ').ToList().Select(val => Convert.ToInt32(val)).ToList();
        // index of choosen bowl
        int num = int.Parse(Console.ReadLine());
        // get grains
        int nbGrains = myList[num];
        myList[num] = 0;
        num++;
        var needReplay = false;
        do
        {
            // my foreach
            for (int i = num; i < 7 && nbGrains != 0; i++)
            {
                nbGrains--;
                myList[i]++;
                needReplay = (i + 1 == 7 && nbGrains == 0);
            }
            num = 0;
            // opponent foreach
            for (int j = 0; j < 6 && nbGrains != 0; j++)
            {
                nbGrains--;
                opList[j]++;
            }
        } while (nbGrains != 0);
        // display opponent bowls
        DisplayBowls(opList);
        // display my bowls
        DisplayBowls(myList);
        // replay
        if(needReplay)
        {
            System.Console.WriteLine("REPLAY");
        }
    }

    static void DisplayBowls(List<int> bowlList)
    {
        var line = "";
        int index = 0;
        bowlList.ForEach( b => {
            // if last bowl => reserve
            if(index == bowlList.Count -1)
            {
                line += " [" + b + "]";
            }
            else
            {
                line += " " + b;
            }
            index++;
        });
        System.Console.WriteLine(line.Trim());
    }
}