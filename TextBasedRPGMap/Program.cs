﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPGMap
{
    internal class Program
    {
        static bool gameOver = false;

        static int storeX;
        static int storeY;
        static int nextX;
        static int nextY;

        static char[,] map = new char[,]
        {
        {'^','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'^','^','`','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`'},
        {'^','^','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`','`','`'},
        {'^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','`','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`','`','`'},
        {'`','`','`','`','`','~','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','^','^','^','^','`','`','`'},
        {'`','`','`','`','`','`','`','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        };
        // map legend:
        // ^ = mountain gray
        // ` = grass dark green background and black text
        // ~ = water cyan
        // * = trees green


        static void Main(string[] args)
        {
            Console.CursorVisible = false;


            Console.WriteLine("Displaying map with no scale imput");        //
            Console.ReadKey(true);                                          //
            DisplayMap();                                                   //
            Console.ReadKey(true);                                          //
            Console.Clear();                                                //
            Console.WriteLine("Displaying map with a scale input of 2");    //
            Console.ReadKey(true);                                          //
            DisplayMap(2);                                                  //
            Console.ReadKey(true);                                          //
            Console.Clear();                                                //
            Console.WriteLine("Displaying map with a scale input of 3");    //
            Console.ReadKey(true);                                          //
            DisplayMap(3);                                                  //
            Console.ReadKey(true);                                          //

            Console.Clear();
            Console.WriteLine("Now to walk around the map.");
            Console.ReadKey(true);
            DisplayMap(2);

            Console.ForegroundColor = ConsoleColor.Black;

            storeX = 12;
            storeY = 15;
            MoveTo(15, 12);
            Console.ReadKey(true);
        }


        static void DisplayMap(int scale = 1)
        {
            Console.Clear();

            Console.WriteLine("┌─────────────────┬─────────────┬─────────────┬─────────────┬─────────────┐"); //Draw Legend Top Border
            Console.Write("│ ");                                //
            Console.BackgroundColor = ConsoleColor.DarkGray;    //
            Console.ForegroundColor = ConsoleColor.Black;       //
            Console.Write(" ^ = Mountains ");                   //
            Console.ResetColor();                               //
            Console.Write(" │ ");                               //
            Console.BackgroundColor = ConsoleColor.Green;       //
            Console.ForegroundColor = ConsoleColor.Black;       //
            Console.Write(" ` = Grass ");                       //
            Console.ResetColor();                               //
            Console.Write(" │ ");                               //
            Console.BackgroundColor = ConsoleColor.Cyan;        //  Draw Legend
            Console.ForegroundColor = ConsoleColor.Black;       //
            Console.Write(" ~ = Water ");                       //
            Console.ResetColor();                               //
            Console.Write(" │ ");                               //
            Console.BackgroundColor = ConsoleColor.DarkGreen;   //
            Console.ForegroundColor = ConsoleColor.Black;       //
            Console.Write(" * = Trees ");                       //
            Console.ResetColor();                               //
            Console.WriteLine(" │  Scale = " + scale + "  │ "); //
            Console.WriteLine("└─────────────────┴─────────────┴─────────────┴─────────────┴─────────────┘"); //Draw Legend Bottom Border
            

            Console.Write("┌");                                 //
            for(int i = 0; i < map.GetLength(1) * scale; i++)   //
            {                                                   //  Draw Top Border
                Console.Write("─");                             //
            }                                                   //
            Console.WriteLine("┐");                             //

            for(int i = 0; i < map.GetLength(0); i++)                                   //
            {                                                                           //
                for(int j = 0; j < scale; j++)                                          //
                {                                                                       //
                    Console.Write("│");                                                 //
                    for (int k = 0; k < map.GetLength(1); k++)                          //
                    {                                                                   //
                        for(int l = 0; l < scale; l++)                                  //
                        {                                                               //  Draw Map to Scale
                            switch (map[i, k])                                          //
                            {                                                           //
                                case '^':                                               //
                                    Console.BackgroundColor = ConsoleColor.DarkGray;    //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    break;                                              //
                                case '`':                                               //
                                    Console.BackgroundColor = ConsoleColor.Green;       //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    break;                                              //
                                case '~':                                               //
                                    Console.BackgroundColor = ConsoleColor.Cyan;        //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    break;                                              //
                                case '*':                                               //
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;   //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    break;                                              //
                                default:                                                //
                                    Console.ResetColor();                               //
                                    break;                                              //
                            }                                                           //
                            Console.Write(map[i, k]);                                   //
                            Console.ResetColor();                                       //
                        }                                                               //
                    }                                                                   //
                    Console.WriteLine("│");                                             //
                }                                                                       //
                                                                                        //
            }                                                                           //


            Console.Write("└");                                 //
            for(int i = 0; i < map.GetLength(1) * scale; i++)   //
            {                                                   //  Draw Bottom Border
                Console.Write("─");                             //
            }                                                   //
            Console.WriteLine("┘");                             //

        }

        static void MoveTo(int x, int y)
        {
            if(0 <= x && x <= map.GetLength(0) && 0 <= y && y <= map.GetLength(1))
            {
                switch (map[storeX, storeY])
                {
                    case '^':
                        Console.SetCursorPosition(storeX + 1, storeY + 4);
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        break;
                    case '`':
                        Console.SetCursorPosition(storeX + 1, storeY + 4);
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;
                    case '*':
                        Console.SetCursorPosition(storeX+1, storeY+4);
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        break;
                    default:
                        break;
                }
                Console.Write(map[storeX, storeY]);

                switch (map[x, y])
                {
                    case '^':
                        Console.SetCursorPosition(x + 1, y + 4);
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.Write("O");
                        break;
                    case '`':
                        Console.SetCursorPosition(x + 1, y + 4);
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write("O");
                        break;
                    case '*':
                        Console.SetCursorPosition(x + 1, y + 4);
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write("0");
                        break;
                    default:
                        break;
                }
                storeX = x;
                storeY = y;
            }
        }


    }
}
