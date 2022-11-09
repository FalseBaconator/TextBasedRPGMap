﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPGMap
{
    internal class Program
    {
        static bool gameOver;
        static ConsoleKeyInfo input;

        static int globalScale;
        static int storeOutX;
        static int storeOutY;
        static int storeDungeonX;
        static int storeDungeonY;
        static bool dontMove = false;
        static bool battling = false;


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
        // ^ = mountain dark gray
        // ` = grass dark green background and black text
        // ~ = water cyan
        // * = trees green
        // ≡ = chest yellow
        // = = mimic print as a chest, yellow
        // ° = stone gray
        // ═║╣ etc = stone wall dark gray


        static string[] slimeSprite = new string[15]
        {
            "                                            ",
            "                                            ",
            "           ┌────────────────────┐           ",
            "          ┌┘▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓└┐          ",
            "         ┌┘▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓└┐         ",
            "        ┌┘▓▓▓▓▓▓▒▒▓▓▓▓▓▓▓▓▒▒▓▓▓▓▓▓└┐        ",
            "       ┌┘▓▓▓▓▓▓▓▒▒▓▓▓▓▓▓▓▓▒▒▓▓▓▓▓▓▓└┐       ",
            "      ┌┘▓▓▓▓▓▓▓▓▒▒▓▓▓▓▓▓▓▓▒▒▓▓▓▓▓▓▓▓└┐      ",
            "      │▓▓▓▓▓▓▓▓▓▒▒▓▓▓▓▓▓▓▓▒▒▓▓▓▓▓▓▓▓▓│      ",
            "      │▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓│      ",
            "      │▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓│      ",
            "      │▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓│      ",
            "      │▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓│      ",
            "      └┐┌┐▓▓▓▓┌┐┌┐▓▓▓▓▓▓┌┐▓▓▓▓┌┐▓▓┌┐┌┘      ",
            "       └┘└────┘└┘└──────┘└────┘└──┘└┘       "
        };

        static string[] goblinSprite = new string[15]
        {
            "               ┌────────────┐               ",
            "              ┌┘            └┐              ",
            "           ┬──┘    │    │    └──┬           ",
            "           └──┐    │    │    ┌──┘           ",
            "              └┐            ┌┘              ",
            "               └─────┬┬─────┘               ",
            "                 ┌┬─┬┴┴┬─┬┐                 ",
            "                 ││ │  │ ││                 ",
            "                 ││ │  │ ││                 ",
            "              ───┴┤ │  │ ├┘                 ",
            "                  └┬┴──┴┬┘                  ",
            "                   │ ┌┐ │                   ",
            "                   │ ││ │                   ",
            "                  ┌┴─┤├─┴┐                  ",
            "                  └──┘└──┘                  "
        };

        static string[] impSprite = new string[15]
        {
            "                 ├┐      ┌┤                 ",
            "     ├┴┐        ┌┴┴──────┴┴┐      ├┴┐       ",
            "    ├┘▓│       ┌┘          └┐    ├┘▓│       ",
            "    └──┘       │    │  │    │    └──┘       ",
            "               └┐          ┌┘               ",
            "                └────┬┬────┘                ",
            "              ┬────┬┬┴┴┬┬────┬     ├┴┐      ",
            "              └┐   ││  ││   ┌┘    ├┘▓│      ",
            "         ├┴┐   └───┤│  │├───┘     └──┘      ",
            "        ├┘▓│       │├──┤│    ├┴┐            ",
            "        └──┘       └┤┌┐├┘   ├┘▓│            ",
            "                    ││││    └──┘            ",
            "                    └┘└┘                    ",
            "                                            ",
            "                                            "
        };

        static string[] mimicSprite = new string[15]
        {
            "                                            ",
            "                                            ",
            "                                            ",
            "    ╔══════════════════════════════════╗    ",
            "    ║      ┌┬┐                         ║    ",
            "    ║      └┴┘                         ║    ",
            "    ╚══════════════════════════════════╝    ",
            "    ├┘└┘└┘└┘└┘└┘└┤└┘└┘└┘└┘└┘└┘├┘└┘└┤└┘└┤    ",
            "    ├┐┌┐┌┐├┐┌──┐┌┐┌┐├┐┌┐┌┐┌┤┌┐┌┐┌┐┌┐┌┐┌┤    ",
            "    ╔═══════╡  ╞══╤══════╤═════════════╗    ",
            "    ║       │  │  │██████│             ║    ",
            "    ║       └┐ │  └──────┘    ┌┬┐      ║    ",
            "    ║        └┬┘              └┴┘      ║    ",
            "    ║                                  ║    ",
            "    ╚══════════════════════════════════╝    ",
        };

        static Dictionary<string, string[]> sprites = new Dictionary<string, string[]>
        {
            {"Slime", slimeSprite},
            {"Goblin", goblinSprite},
            {"Imp", impSprite},
            {"Mimic", mimicSprite}
        };
        static Dictionary<string, int> healths;
        static Dictionary<string, int> atk;
        static Dictionary<string, string> colors = new Dictionary<string, string>
        {
            {"Slime", "Blue"},
            {"Goblin", "Green"},
            {"Imp", "Red"},
            {"Mimic", "Yellow"}
        };


        static void Main(string[] args)
        {
            Console.Title = "Map Project";

            Console.CursorVisible = false;


            Console.WriteLine("Displaying map with no scale imput");        //
            Console.ReadKey(true);                                          //
            DisplayMap();                                                   //
            Console.ReadKey(true);                                          //
            Console.Clear();                                                //
            Console.WriteLine("Displaying map with a scale input of 2");    //
            Console.ReadKey(true);                                          //
            DisplayMap(2);                                                  //Showcase
            Console.ReadKey(true);                                          //
            Console.Clear();                                                //
            Console.WriteLine("Displaying map with a scale input of 3");    //
            Console.ReadKey(true);                                          //
            DisplayMap(3);                                                  //
            Console.ReadKey(true);                                          //

            Console.Clear();
            Console.WriteLine("Now to walk around the map.");

            try                                                                                                 //
            {                                                                                                   //
                Console.Write("What scale fo you want your map to be while exploring?: ");                      //
                DisplayMap(Convert.ToInt32(Console.ReadLine()));                                                //
            }                                                                                                   //
            catch (Exception)                                                                                   //Get Input
            {                                                                                                   //
                Console.WriteLine("Whoops! something went wrong with that. We'll just use the default scale");  //
                Console.ReadKey(true);                                                                          //
                DisplayMap();                                                                                   //
            }                                                                                                   //

            while((map.GetLength(1) * globalScale) + 2 >= Console.WindowWidth)                                                                  //
            {                                                                                                                                   //
                Console.Clear();                                                                                                                //Prevent width based display errors
                Console.WriteLine("It seems like that scale is too wide to fit in the window, which will cause it to display incorrectly.");    //
                Console.WriteLine("Please either maximize your window and try again, or choose a smaller scale");                               //
                try                                                                                                                             //  //
                {                                                                                                                               //  //
                    Console.Write("What scale fo you want your map to be while exploring?: ");                                                  //  //
                    DisplayMap(Convert.ToInt32(Console.ReadLine()));                                                                            //  //
                }                                                                                                                               //  //
                catch (Exception)                                                                                                               //  //Get Input
                {                                                                                                                               //  //
                    Console.WriteLine("Whoops! something went wrong with that. We'll just use the default scale");                              //  //
                    Console.ReadKey(true);                                                                                                      //  //
                    DisplayMap();                                                                                                               //  //
                }                                                                                                                               //  //
            }                                                                                                                                   //


            StartGameLoop();    //Start the game
            while(gameOver == false)                                                                                                            //Loop
            {                                                                                                                                   //
                Console.CursorVisible = false;                                                                                                  //
                input = Console.ReadKey(true);                                                                                                  //
                if (input.Key == ConsoleKey.Escape)                                                                                             //  //
                {                                                                                                                               //  //End Loop
                    gameOver = true;                                                                                                            //  //
                }                                                                                                                               //  //
                if (battling == false)
                {
                    if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && storeOutY > 0)                                           //      //
                    {                                                                                                                                   //      //
                        MoveTo(storeOutX, storeOutY - 1);                                                                                               //      //
                    }                                                                                                                                   //      //
                    else if ((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && storeOutY < map.GetLength(0) * globalScale - 1)        //      //
                    {                                                                                                                                   //      //
                        MoveTo(storeOutX, storeOutY + 1);                                                                                               //      //
                    }                                                                                                                                   //      //Movement
                    else if ((input.Key == ConsoleKey.LeftArrow || input.Key == ConsoleKey.A) && storeOutX > 0)                                         //      //
                    {                                                                                                                                   //      //
                        MoveTo(storeOutX - 1, storeOutY);                                                                                               //      //
                    }                                                                                                                                   //      //
                    else if ((input.Key == ConsoleKey.RightArrow || input.Key == ConsoleKey.D) && storeOutX < map.GetLength(1) * globalScale - 1)       //      //
                    {                                                                                                                                   //      //
                        MoveTo(storeOutX + 1, storeOutY);                                                                                               //      //
                    }                                                                                                                                   //
                    Console.ResetColor();
                    var rand = new Random();
                    if (rand.Next(0, 10) == 1)
                    {
                        StartBattle(rand.Next(0, 10));
                    }                                                                                                                               //
                }
            }                                                                                                                                       //
        }


        static void DisplayMap(int scale = 1)
        {

            Console.CursorVisible = false;
            globalScale = scale;
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


        static void StartGameLoop()
        {
            storeOutX = (map.GetLength(1) * globalScale) / 2;
            storeOutY = (map.GetLength(0) * globalScale) / 2;
            MoveTo(storeOutX, storeOutY-1);
        }


        static void MoveTo(int x, int y)
        {
            if(0 <= x/globalScale && x/globalScale <= map.GetLength(1) && 0 <= y/globalScale && y/globalScale <= map.GetLength(0))
            {

                Console.ForegroundColor = ConsoleColor.Black;

                switch (map[y / globalScale, x / globalScale])              //
                {                                                           //
                    case '^':                                               //
                        Console.SetCursorPosition(x + 1, y + 4);            //
                        Console.BackgroundColor = ConsoleColor.DarkGray;    //
                        Console.Write("O");                                 //
                        dontMove = false;                                   //
                        break;                                              //
                    case '`':                                               //
                        Console.SetCursorPosition(x + 1, y + 4);            //
                        Console.BackgroundColor = ConsoleColor.Green;       //
                        Console.Write("O");                                 //
                        dontMove = false;                                   //  Draw Player, matching the background color to the map
                        break;                                              //
                    case '*':                                               //
                        Console.SetCursorPosition(x + 1, y + 4);            //
                        Console.BackgroundColor = ConsoleColor.DarkGreen;   //
                        Console.Write("O");                                 //
                        dontMove = false;                                   //
                        break;                                              //
                    case '~':                                               //
                        dontMove = true;                                    //
                        break;                                              //
                }                                                           //

                if (dontMove == false)                                                      //
                {                                                                           //
                    switch (map[storeOutY / globalScale, storeOutX / globalScale])                //
                    {                                                                       //
                        case '^':                                                           //
                            Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);              //
                            Console.BackgroundColor = ConsoleColor.DarkGray;                //
                            Console.Write(map[storeOutY / globalScale, storeOutX / globalScale]); //
                            storeOutX = x;                                                     //
                            storeOutY = y;                                                     //
                            break;                                                          //
                        case '`':                                                           //
                            Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);              //
                            Console.BackgroundColor = ConsoleColor.Green;                   //
                            Console.Write(map[storeOutY / globalScale, storeOutX / globalScale]); //
                            storeOutX = x;                                                     //  Replace the obsolete player sprite
                            storeOutY = y;                                                     //
                            break;                                                          //
                        case '*':                                                           //
                            Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);              //
                            Console.BackgroundColor = ConsoleColor.DarkGreen;               //
                            Console.Write(map[storeOutY / globalScale, storeOutX / globalScale]); //
                            storeOutX = x;                                                     //
                            storeOutY = y;                                                     //
                            break;                                                          //
                        default:                                                            //
                            break;                                                          //
                    }                                                                       //
                }                                                                           //

            }
        }


        static void StartBattle(int select)
        {
            battling = true;
            Console.ResetColor();
            string picker;
            if (select == 0)
            {
                picker = "Imp";
                Console.ForegroundColor = ConsoleColor.Red;
            }else if (select >= 1 && select <= 4)
            {
                picker = "Goblin";
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                picker = "Slime";
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.Clear();
            foreach(string line in sprites[picker])
            {
                Console.WriteLine(line);
            }
        }



    }
}
