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
        
        static string enemy;   //the current enemy being fought
        static int menuCursor;
        static int potionHeal = 10;
        static bool playerTurn;

        static string[] menu = new string[]
        {
            "Attack",
            "Use Healing Potion",
            "Run"
        };


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
        // ≡ = chest dark yellow
        // = = mimic print as a chest, dark yellow
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

        static Dictionary<string, int> PlayerStats = new Dictionary<string, int>
        {
            {"HP", 10},
            {"Max HP", 10},
            {"ATK", 2},
            {"Potions", 0},
            {"Coins", 0},
            //{"", },
            //{"", },
            //{"", },
            //{"", },
        };

        static Dictionary<string, string[]> sprites = new Dictionary<string, string[]>
        {
            {"Slime", slimeSprite},
            {"Goblin", goblinSprite},
            {"Imp", impSprite},
            {"Mimic", mimicSprite}
        };
        static Dictionary<string, int> enemyMaxHealths = new Dictionary<string, int>
        {
            {"Slime", 3},
            {"Goblin", 4},
            {"Imp", 5},
            {"Mimic", 3}
        };
        static Dictionary<string, int> enemyAtks = new Dictionary<string, int>
        {
            {"Slime", 3},
            {"Goblin", 4},
            {"Imp", 5},
            {"Mimic", 3}
        };
        static Dictionary<string, int> runDifficulties = new Dictionary<string, int>
        {
            {"Slime", 3},
            {"Goblin", 4},
            {"Imp", 5},
            {"Mimic", 3}
        };
        static Dictionary<string, string> colors = new Dictionary<string, string>
        {
            {"Slime", "Blue"},
            {"Goblin", "Green"},
            {"Imp", "Red"},
            {"Mimic", "DarkYellow"}
        };
        static int enemyHP;

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
                    if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && storeOutY > 0)                                                //      //
                    {                                                                                                                                   //      //
                        MoveTo(storeOutX, storeOutY - 1);                                                                                               //      //
                    }                                                                                                                                   //      //
                    else if ((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && storeOutY < map.GetLength(0) * globalScale - 1)        //      //
                    {                                                                                                                                   //      //
                        MoveTo(storeOutX, storeOutY + 1);                                                                                               //      //
                    }                                                                                                                                   //      //Movement if overworld
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
                else
                {
                    if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && menuCursor > 0)                              
                    {
                        menuCursor--;
                        DrawBattle();
                    } else if((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && menuCursor < menu.Length - 1)
                    {
                        menuCursor++;
                        DrawBattle();
                    }else if(input.Key == ConsoleKey.Enter || input.Key == ConsoleKey.Spacebar)
                    {
                        switch (menuCursor)
                        {
                            case 0:
                                AttackEnemy();
                                break;
                            case 1:
                                UsePotion();
                                break;
                            case 2:
                                Run();
                                break;
                        }
                    }
                }
            }                                                                                                                                       //
        }


        static void DisplayMap(int scale = 1)
        {

            Console.CursorVisible = false;
            globalScale = scale;
            Console.Clear();

            Console.WriteLine("┌─────────────────┬─────────────┬─────────────┬─────────────┬─────────────┬───────────────┐");   //Draw Legend Top Border
            Console.Write("│ ");                                                                                            //
            Console.BackgroundColor = ConsoleColor.DarkGray;                                                                //
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" ^ = Mountains ");                                                                               //
            Console.ResetColor();                                                                                           //
            Console.Write(" │ ");                                                                                           //
            Console.BackgroundColor = ConsoleColor.Green;                                                                   //
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" ` = Grass ");                                                                                   //
            Console.ResetColor();                                                                                           //
            Console.Write(" │ ");                                                                                           //
            Console.BackgroundColor = ConsoleColor.Cyan;                                                                    //  Draw Legend
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" ~ = Water ");                                                                                   //
            Console.ResetColor();                                                                                           //
            Console.Write(" │ ");                                                                                           //
            Console.BackgroundColor = ConsoleColor.DarkGreen;                                                               //
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" * = Trees ");                                                                                   //
            Console.ResetColor();                                                                                           //
            Console.WriteLine(" │  Scale = " + scale + "  │  Coins = " + PlayerStats["Coins"].ToString("000") + "  │ ");    //
            Console.WriteLine("└─────────────────┴─────────────┴─────────────┴─────────────┴─────────────┴───────────────┘");   //Draw Legend Bottom Border
            

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

            Console.ResetColor();

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

                if (dontMove == false)                                                              //
                {                                                                                   //
                    switch (map[storeOutY / globalScale, storeOutX / globalScale])                  //
                    {                                                                               //
                        case '^':                                                                   //
                            Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);                //
                            Console.BackgroundColor = ConsoleColor.DarkGray;                        //
                            Console.Write(map[storeOutY / globalScale, storeOutX / globalScale]);   //
                            storeOutX = x;                                                          //
                            storeOutY = y;                                                          //
                            break;                                                                  //
                        case '`':                                                                   //
                            Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);                //
                            Console.BackgroundColor = ConsoleColor.Green;                           //
                            Console.Write(map[storeOutY / globalScale, storeOutX / globalScale]);   //
                            storeOutX = x;                                                          //  Replace the obsolete player sprite
                            storeOutY = y;                                                          //
                            break;                                                                  //
                        case '*':                                                                   //
                            Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);                //
                            Console.BackgroundColor = ConsoleColor.DarkGreen;                       //
                            Console.Write(map[storeOutY / globalScale, storeOutX / globalScale]);   //
                            storeOutX = x;                                                          //
                            storeOutY = y;                                                          //
                            break;                                                                  //
                        default:                                                                    //
                            break;                                                                  //
                    }                                                                               //
                }                                                                                   //

            }
        }

        static void DrawBattle()
        {
            Console.ResetColor();
            Console.Clear();

            switch (enemy)
            {
                case "Imp":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "Goblin":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Slime":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "Mimic":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
            }

            foreach (string line in sprites[enemy])
            {
                Console.WriteLine(line);
            }

            Console.ResetColor();

            Console.SetCursorPosition(0, 17);
            Console.WriteLine("Player");
            Console.WriteLine("HP: " + PlayerStats["HP"]);
            Console.WriteLine("Potions: " + PlayerStats["Potions"]);

            Console.SetCursorPosition(20, 17);
            Console.WriteLine("Enemy");
            Console.SetCursorPosition(20, 18);
            Console.WriteLine("HP: " + enemyHP);

            Console.SetCursorPosition(5, 21);
            Console.WriteLine(menu[0]);
            Console.SetCursorPosition(5, 23);
            Console.WriteLine(menu[1]);
            Console.SetCursorPosition(5, 25);
            Console.WriteLine(menu[2]);

            if (playerTurn)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(3, 21 + (menuCursor * 2));
                Console.Write(">");
                Console.SetCursorPosition(24, 21 + (menuCursor * 2));
                Console.Write("<");
            }

            Console.ResetColor();

        }

        static void EndBattle()
        {
            battling = false;
            Console.Clear();
            DisplayMap(globalScale);
            Console.ForegroundColor = ConsoleColor.Black;
            switch (map[storeOutY / globalScale, storeOutX / globalScale])              //
            {                                                                           //
                case '^':                                                               //
                    Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);            //
                    Console.BackgroundColor = ConsoleColor.DarkGray;                    //
                    Console.Write("O");                                                 //
                    dontMove = false;                                                   //
                    break;                                                              //
                case '`':                                                               //
                    Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);            //
                    Console.BackgroundColor = ConsoleColor.Green;                       //
                    Console.Write("O");                                                 //
                    dontMove = false;                                                   //  Draw Player, matching the background color to the map
                    break;                                                              //
                case '*':                                                               //
                    Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);            //
                    Console.BackgroundColor = ConsoleColor.DarkGreen;                   //
                    Console.Write("O");                                                 //
                    dontMove = false;                                                   //
                    break;                                                              //
                case '~':                                                               //
                    dontMove = true;                                                    //
                    break;                                                              //
            }                                                                           //
        }

        static void StartBattle(int select)
        {
            menuCursor = 0;
            battling = true;
            playerTurn = true;
            if (select == 0)
            {
                enemy = "Imp";
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (select >= 1 && select <= 4)
            {
                enemy = "Goblin";
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                enemy = "Slime";
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            enemyHP = enemyMaxHealths[enemy];
            DrawBattle();
                        
        }

        static void AttackEnemy()
        {
            enemyHP -= PlayerStats["ATK"];
            if(enemyHP < 0)
            {
                enemyHP = 0;
            }
            playerTurn = false;
            DrawBattle();
            Console.SetCursorPosition(5, 20);
            Console.Write("You Dealt " + PlayerStats["ATK"] + " DMG!");
            Console.ReadKey(true);
            if(enemyHP > 0)
            {
                EnemyTurn();
            }
            else
            {
                DetermineLoot();
                EndBattle();
            }
        }

        static void UsePotion()
        {
            if (PlayerStats["Potions"] > 0)
            {
                PlayerStats["Potions"]--;
                PlayerStats["HP"] += potionHeal;
                if (PlayerStats["HP"] > PlayerStats["Max HP"])
                {
                    PlayerStats["HP"] = PlayerStats["Max HP"];
                }
                playerTurn = false;
                DrawBattle();
                Console.SetCursorPosition(5, 20);
                Console.Write("You Drank A Health Potion!");
                Console.ReadKey(true);
                EnemyTurn();
            }
            else
            {
                DrawBattle();
                Console.SetCursorPosition(5, 20);
                Console.Write("You Don't Have Any Potions To Use!");
            }
        }

        static void Run()
        {
            var Rand = new Random();
            playerTurn = false;
            if (Rand.Next(1, 16) >= runDifficulties[enemy])
            {
                DrawBattle();
                Console.SetCursorPosition(5, 20);
                Console.Write("You Ran Away Successfully!");
                Console.ReadKey(true);
                EndBattle();
            }
            else
            {
                DrawBattle();
                Console.SetCursorPosition(5, 20);
                Console.Write("The " + enemy + " Blocks Your Path!");
                Console.ReadKey(true);
                EnemyTurn();
            }
        }

        static void EnemyTurn()
        {
            PlayerStats["HP"] -= enemyAtks[enemy];
            if (PlayerStats["HP"] < 0)
            {
                PlayerStats["HP"] = 0;
            }
            DrawBattle();
            Console.SetCursorPosition(5, 20);
            Console.Write("The " + enemy + " Dealt " + enemyAtks[enemy] + " DMG");
            Console.ReadKey(true);
            if (PlayerStats["HP"] == 0)
            {
                GameOver();
            }
            else
            {
                playerTurn = true;
                DrawBattle();
            }
        }

        static void GameOver()
        {
            gameOver = true;
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("You Died!");
            Console.ReadKey(true);
        }

        static void DetermineLoot()
        {
            var rand = new Random();
            int potionToGain = 0;
            int coinToGain = 0;
            for(int i = 0; i < enemyAtks[enemy]; i++)
            {
                if(rand.Next(0, 5) == 0)
                {
                    potionToGain++;
                }
                else
                {
                    coinToGain += rand.Next(2, 6);
                }
            }
            PlayerStats["Potions"] += potionToGain;
            PlayerStats["Coins"] += coinToGain;
            DrawBattle();
            Console.SetCursorPosition(5, 20);
            Console.Write("You Gained " + potionToGain + " Potions And " + coinToGain + " Coins!");
            Console.ReadKey(true);

        }





    }
}
