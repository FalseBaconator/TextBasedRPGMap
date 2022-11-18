using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedRPGMap
{
    internal class Program
    {
        static bool gameOver;           //  Game Loop Info
        static ConsoleKeyInfo input;    //

        static int globalScale;         //

        static int storeOutX;           //
        static int storeOutY;           //
        static int storeDungeonX;       //  Movement Info
        static int storeDungeonY;       //
        static bool dontMove = false;   //


        static bool inShop = false;             //
        static bool selectingWeapon = false;    //  Gamestate Info
        static bool inDungeon = false;          //
        static int currentDungeon;              //
        
        static bool battling = false;   //
        static string enemy;            //
        static int menuCursor;          //  Battle Info
        static int potionHeal = 10;     //
        static bool playerTurn;         //

        static string[] battleMenu = new string[]       //
        {                                               //
            "Attack",                                   //
            "Use Healing Potion",                       //
            "Run"                                       //
        };                                              //
                                                        //
        static string[] shopMenu = new string[]         //
        {                                               //
            "Armor",                                    //
            "Weapons",                                  //  Menus
            "Swap Weapon",                              //
            "Exit",                                     //
        };                                              //
                                                        //
        static string[] weaponMenu = new string[]       //
        {                                               //
            "Sword",                                    //
            "Flail",                                    //
            "Spear",                                    //
            "Axe"                                       //
        };                                              //

        static string[] ranks = new string[]                                        //
        {                                                                           //
            "Wood",                                                                 //
            "Iron",                                                                 //
            "Mithril"                                                               //
        };                                                                          //
        static Dictionary<string, int> PlayerStats = new Dictionary<string, int>    //
        {                                                                           //
            {"HP", 10},                                                             //
            {"Max HP", 10}, //wood armor                                            //
            {"ATK", 2}, //wood sword                                                //  Player Stats
            {"Potions", 0},                                                         //
            {"Coins", 0},                                                           //
        };                                                                          //
        static int[] ArmorStats = new int[]                                         //
        {                                                                           //
            10, 20, 30 //wood, iron, mithril                                        //
        };                                                                          //
        static int[] WeaponStats = new int[]                                        //
        {                                                                           //
            2, 4, 6 //wood, iron, mithril                                           //
        };                                                                          //
        static int currentWeaponType = 0;                                           //
        static int currentWeaponRank = 0;                                           //
        static int currentArmorRank = 0;                                            //


        ///////////////////////////MAPS//////////////////////////////////
        static char[,] map = new char[,]
        {
        {'^','^','^','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'^','^','`','`','`','`','*','*','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`'},
        {'^','^','`','`','`','*','*','1','`','`','`','`','`','`','`','`','`','`','`','`','`','`','~','~','~','`','`','`','`','`'},
        {'^','0','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','$','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','`','~','~','~','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`','`'},
        {'`','`','`','~','~','~','~','2','`','`','`','`','`','`','`','`','`','`','`','`','`','3','^','^','`','`','`','`','`','`'},
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
        // $ = shop dark yellow

        //Dungeon Legend
        // = = chest dark yellow
        // ≈ = Open chest dark yellow
        // ! = mimic print as a chest, dark yellow
        // , = stone floor gray
        // ═║╣ etc = stone wall dark gray
        // █ = Entrance/Exit dark gray

        static char[,] dungeon0 = new char[,]
        {
            {'╔', '═', '═', '╦', '═', '═', '═', '╗', ' ', ' ', '╔', '═', '═', '═', '╗'},
            {'║', ',', ',', '║', ',', '=', ',', '║', ' ', ' ', '║', ',', ',', ',', '║'},
            {'║', ',', ',', '║', ',', ',', ',', '║', ' ', ' ', '║', ',', ',', ',', '║'},
            {'║', ',', ',', '║', ',', ',', ',', '╚', '═', '═', '╝', ',', ',', ',', '║'},
            {'║', ',', ',', '║', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', '║'},
            {'║', ',', '╔', '╩', '═', '═', '═', '╗', ',', '╔', '═', '═', '═', '═', '╣'},
            {'║', ',', '╚', '═', '═', '═', '═', '╝', ',', '╚', '═', '═', '═', '═', '╣'},
            {'║', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', '!', '║'},
            {'╚', '█', '═', '═', '═', '═', '═', '═', '═', '═', '═', '═', '═', '═', '╝'}
        };

        static char[,] dungeon1 = new char[,]
        {
            {'╔', '═', '╦', '═', '╦', '═', '╦', '═', '═', '═', '╦', '═', '═', '═', '╗'},
            {'║', '!', '║', '=', '║', '=', '║', ',', ',', '!', '║', '=', ',', ',', '║'},
            {'║', ',', '║', ',', '║', ',', '║', ',', '═', '═', '╬', '═', '═', ',', '║'},
            {'║', ',', '║', ',', '║', ',', '║', ',', ',', ',', '║', ',', ',', ',', '║'},
            {'║', ',', ',', ',', ',', ',', ',', ',', '║', ',', ',', ',', '╔', '═', '╣'},
            {'╠', '═', '═', '═', ',', '═', '╦', '═', '╣', ',', '═', '═', '╣', '=', '║'},
            {'║', ',', ',', ',', ',', ',', '║', '!', '║', ',', ',', ',', '║', ',', '║'},
            {'║', '!', '║', ',', '║', ',', '║', ',', ',', ',', '║', ',', ',', ',', '║'},
            {'╚', '═', '╩', '█', '╩', '═', '╩', '═', '═', '═', '╩', '═', '═', '═', '╝'}
        };

        static char[,] dungeon2 = new char[,]
        {
            {'╔', '═', '═', '═', '╦', '═', '═', '═', '═', '═', '═', '═', '═', '═', '╗'},
            {'║', ',', ',', ',', '║', ',', ',', ',', ',', ',', ',', ',', ',', ',', '║'},
            {'║', ',', '║', ',', '║', ',', '╔', '═', '═', '═', '═', '═', '╗', ',', '║'},
            {'║', ',', '║', ',', '║', ',', '║', ',', ',', ',', ',', ',', '║', ',', '║'},
            {'║', ',', '║', ',', '║', ',', '║', ',', '═', '═', '╗', ',', '║', ',', '║'},
            {'║', ',', '║', '!', '║', ',', '║', ',', ',', '=', '║', ',', '║', ',', '║'},
            {'║', ',', '╚', '═', '╝', ',', '╚', '═', '═', '═', '╝', ',', '║', ',', '║'},
            {'║', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', '║', ',', '║'},
            {'╚', '═', '═', '═', '═', '═', '═', '═', '═', '═', '═', '═', '╩', '█', '╝'}
        };

        static char[,] dungeon3 = new char[,]
        {
            {'╔', '═', '═', '═', '╗', ' ', ' ', ' ', '╔', '═', '═', '═', '═', '═', '╗'},
            {'║', ',', '=', ',', '╚', '═', '═', '═', '╝', ',', ',', ',', ',', ',', '║'},
            {'║', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', ',', '!', '║'},
            {'║', ',', ',', ',', '╔', '═', ',', '═', '╗', ',', ',', ',', ',', ',', '║'},
            {'║', ',', ',', ',', '║', ',', ',', ',', '╠', '═', '╦', '═', ',', '═', '╣'},
            {'║', ',', ',', ',', '║', ',', ',', ',', '║', ' ', '║', ',', ',', ',', '║'},
            {'║', ',', ',', ',', '║', ',', ',', ',', '║', ' ', '║', ',', ',', ',', '║'},
            {'║', ',', ',', ',', '║', ',', '!', ',', '║', ' ', '║', ',', '=', ',', '║'},
            {'╚', '═', '█', '═', '╩', '═', '═', '═', '╝', ' ', '╚', '═', '═', '═', '╝'}
        };

        static char[][,] dungeons = new char[][,]
        {
            dungeon0, dungeon1, dungeon2, dungeon3
        };

        ///////////////////////////MAPS//////////////////////////////////

        ///////////////////////////ENEMY SPRITES//////////////////////////////////
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
        ///////////////////////////ENEMY SPRITES//////////////////////////////////

        ///////////////////////////ENEMY STATS//////////////////////////////////
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
        static int enemyHP;
        ///////////////////////////ENEMY STATS//////////////////////////////////

        ///////////////////////PLAYER SPRITES//////////////////////////////
        static string[] playerSprite = new string[15]
        {
            "                                            ",
            "                                            ",
            "             ┌──────────┐                   ",
            "            ┌┘          └┐                  ",
            "            │            │                  ",
            "            │         ┌┬┬┘                  ",
            "            │         │┌┘                   ",
            "        ┌┬──┤         ├┘                    ",
            "        └┤  └─────────┴──┬┐                 ",
            "         │               │└┐                ",
            "         │               ├┐└┐               ",
            "         │               ├┼┐└┐              ",
            "         │               │└┼┬┴┐             ",
            "         │               ├┬┴┼─┘             ",
            "         │               │└─┘               "
        };

        static string[] swordSprite = new string[15]
        {
            "                                            ",
            "                                            ",
            "                                            ",
            "                                            ",
            "                                     ┌──┐   ",
            "                                    ┌┘ ┌┘   ",
            "                                   ┌┘ ┌┘    ",
            "                                  ┌┘ ┌┘     ",
            "                                 ┌┘ ┌┘      ",
            "                             ┌─┐┌┘ ┌┘       ",
            "                             └┐└┤ ┌┘        ",
            "                              ├┐└┬┘         ",
            "                               ┴┐└┐         ",
            "                                └─┘         ",
            "                                            "
        };

        static string[] flailSprite = new string[15]
        {
            "                                            ",
            "                                            ",
            "                                            ",
            "                                            ",
            "                                     ┌─┐    ",
            "                                    ┌┘┌┤    ",
            "                                   ┌┘┌┘║    ",
            "                                  ┌┘┌┘ │    ",
            "                                 ┌┘┌┘  ║    ",
            "                                ┌┘┌┘   │    ",
            "                               ┌┘┌┘    ║    ",
            "                              ┌┘┌┘   ┼─┴─┼  ",
            "                               ─┘    ┤ ┼ ├  ",
            "                                     ┼─┬─┼  ",
            "                                            "
        };

        static string[] spearSprite = new string[15]
        {
            "                                         ┌─┐",
            "                                        ┌┴┬┘",
            "                                       ┌┘┌┘ ",
            "                                      ┌┘┌┘  ",
            "                                     ┌┘┌┘   ",
            "                                    ┌┘┌┘    ",
            "                                   ┌┘┌┘     ",
            "                                  ┌┘┌┘      ",
            "                                 ┌┘┌┘       ",
            "                                ┌┘┌┘        ",
            "                               ┌┘┌┘         ",
            "                              ┌┘┌┘          ",
            "                               ─┘           ",
            "                                            ",
            "                                            "
        };

        static string[] axeSprite = new string[15]
        {
            "                                            ",
            "                                            ",
            "                                            ",
            "                                      ┌─┐   ",
            "                                     ┌┘┌┤   ",
            "                                    ┌┘┌┘└──┐",
            "                                   ┌┘┌┴┐  ┌┘",
            "                                  ┌┘┌┘ │ ┌┘ ",
            "                                 ┌┘┌┘  └─┘  ",
            "                                ┌┘┌┘        ",
            "                               ┌┘┌┘         ",
            "                              ┌┘┌┘          ",
            "                               ─┘           ",
            "                                            ",
            "                                            "
        };

        static string[][] weapons = new string[][]
        {
            swordSprite, flailSprite, spearSprite, axeSprite
        };
        ///////////////////////PLAYER SPRITES//////////////////////////////

        ////////////////////////////////SHOP SPRITES///////////////////////////////////////
        static string[] shopSprite = new string[15]
        {
            "  ┌──────────────────────────────────────┐  ",
            "  │       ┌───  │  │  ┌──┐  ┌──┐         │  ",
            "  │       └──┐  ├──┤  │  │  ├──┘         │  ",
            "  │       ───┘  │  │  └──┘  │            │  ",
            "  └─┬──────────────────────────────────┬─┘  ",
            "    │                                  │    ",
            "    │   ╔═════╗    ┌────┐      ║       │    ",
            "    │   ║     ║    ││  ││      ║       │    ",
            "    │   ║     ║    └┬──┬┘      ║       │    ",
            "    │   ╚═╗ ╔═╝  ┌┬─┴──┴─┬┐   ═╬═      │    ",
            "    │     ╚═╝    ││      ││    ║       │    ",
            "  ┌─┴────────────┴┴──────┴┴────────────┴─┐  ",
            "  │                                      │  ",
            "  │                                      │  ",
            "  │                                      │  "
        };

        static char[] shopWoodChars = new char[]
        {
            '┌','─','│','┐','┘','└','┬','┴'
        };

        static char[] shopMetalChars = new char[]
        {
            '╔', '╗', '╚', '╝', '║', '═', '╬'
        };
        ////////////////////////////////SHOP SPRITES///////////////////////////////////////

        //Runs the game
        static void Main(string[] args)
        {

            Console.Title = "Map Project";

            Console.CursorVisible = false;


            Console.WriteLine("Displaying map with no scale input");        //
            Console.ReadKey(true);                                          //
            DisplayMap();                                                   //
            Console.ReadKey(true);                                          //
            Console.Clear();                                                //
            Console.WriteLine("Displaying map with a scale input of 2");    //
            Console.ReadKey(true);                                          //
            DisplayMap(2);                                                  //  Showcase
            Console.ReadKey(true);                                          //
            Console.Clear();                                                //
            Console.WriteLine("Displaying map with a scale input of 3");    //
            Console.ReadKey(true);                                          //
            DisplayMap(3);                                                  //
            Console.ReadKey(true);                                          //


            StartGameLoop();    //Start the game
            while(gameOver == false)                                                                                                            //  Loop
            {                                                                                                                                   
                Console.CursorVisible = false;                                                                                                  
                input = Console.ReadKey(true);                                                                                                  
                if (input.Key == ConsoleKey.Escape)                                                                                             //
                {                                                                                                                               //  End Loop
                    gameOver = true;                                                                                                            //
                }                                                                                                                               //
                if (battling == false && inShop == false)   //Map Check
                {
                    if(inDungeon == false)                                                                                                                  //  Overworld
                    {                                                                                                                                       //
                        if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && storeOutY > 0)                                                //      //
                        {                                                                                                                                   //      //
                            MoveToOut(storeOutX, storeOutY - 1);                                                                                            //      //
                        }                                                                                                                                   //      //
                        else if ((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && storeOutY < map.GetLength(0) * globalScale - 1)        //      //
                        {                                                                                                                                   //      //
                            MoveToOut(storeOutX, storeOutY + 1);                                                                                            //      //
                        }                                                                                                                                   //      //  Movement if overworld
                        else if ((input.Key == ConsoleKey.LeftArrow || input.Key == ConsoleKey.A) && storeOutX > 0)                                         //      //
                        {                                                                                                                                   //      //
                            MoveToOut(storeOutX - 1, storeOutY);                                                                                            //      //
                        }                                                                                                                                   //      //
                        else if ((input.Key == ConsoleKey.RightArrow || input.Key == ConsoleKey.D) && storeOutX < map.GetLength(1) * globalScale - 1)       //      //
                        {                                                                                                                                   //      //
                            MoveToOut(storeOutX + 1, storeOutY);                                                                                            //      //
                        }                                                                                                                                   //      //
                        Console.ResetColor();                                                                                                               //  //
                        var rand = new Random();                                                                                                            //  //
                        if (rand.Next(0, 10) == 1)                                                                                                          //  //  Chance to start a battle
                        {                                                                                                                                   //  //
                            StartBattle(rand.Next(0, 10));                                                                                                  //  //
                        }                                                                                                                                   //  //
                    }                                                                                                                                       //
                    else                                                                                                                                            //  Dungeon
                    {                                                                                                                                               //
                        if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && storeDungeonY > 0)                                                    //      //
                        {                                                                                                                                           //      //
                            MoveToDun(storeDungeonX, storeDungeonY - 1);                                                                                            //      //
                        }                                                                                                                                           //      //
                        else if ((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && storeDungeonY < dungeons[currentDungeon].GetLength(0))         //      //
                        {                                                                                                                                           //      //
                            MoveToDun(storeDungeonX, storeDungeonY + 1);                                                                                            //      //
                        }                                                                                                                                           //      //  Movement if dungeon
                        else if ((input.Key == ConsoleKey.LeftArrow || input.Key == ConsoleKey.A) && storeDungeonX > 0)                                             //      //
                        {                                                                                                                                           //      //
                            MoveToDun(storeDungeonX - 1, storeDungeonY);                                                                                            //      //
                        }                                                                                                                                           //      //
                        else if ((input.Key == ConsoleKey.RightArrow || input.Key == ConsoleKey.D) && storeDungeonX < dungeons[currentDungeon].GetLength(1))        //      //
                        {                                                                                                                                           //      //
                            MoveToDun(storeDungeonX + 1, storeDungeonY);                                                                                            //      //
                        }                                                                                                                                           //      //
                        Console.ResetColor();                                                                                                                       //  //
                        var rand = new Random();                                                                                                                    //  //
                        if (rand.Next(0, 7) == 1)                                                                                                                   //  //
                        {                                                                                                                                           //  //  Chance to start a battle
                            StartBattle(rand.Next(0, 7));                                                                                                           //  //
                        }                                                                                                                                           //  //
                    }                                                                                                                                               //
                }
                else if (battling == true)                                                                                              //  Battle Loop
                {                                                                                                                       //
                    if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && menuCursor > 0)                               //  //
                    {                                                                                                                   //  //
                        menuCursor--;                                                                                                   //  //
                        DrawBattle();                                                                                                   //  //  Move Cursor
                    } else if((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && menuCursor < battleMenu.Length - 1)   //  //
                    {                                                                                                                   //  //
                        menuCursor++;                                                                                                   //  //
                        DrawBattle();                                                                                                   //  //
                    }else if(input.Key == ConsoleKey.Enter || input.Key == ConsoleKey.Spacebar)                                         //      //
                    {                                                                                                                   //      //
                        switch (menuCursor)                                                                                             //      //
                        {                                                                                                               //      //
                            case 0:                                                                                                     //      //
                                AttackEnemy();                                                                                          //      //
                                break;                                                                                                  //      //
                            case 1:                                                                                                     //      //  Select Menu Option
                                UsePotion();                                                                                            //      //
                                break;                                                                                                  //      //
                            case 2:                                                                                                     //      //
                                Run();                                                                                                  //      //
                                break;                                                                                                  //      //
                        }                                                                                                               //      //
                    }                                                                                                                   //      //
                }                                                                                                                       //
                else if (inShop == true && selectingWeapon == false)                                                                //  In Shop Not Selecting Weapons
                {                                                                                                                   //
                    if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && menuCursor > 0)                           //  //
                    {                                                                                                               //  //
                        menuCursor--;                                                                                               //  //
                        DrawShop(shopMenu);                                                                                         //  //
                    }                                                                                                               //  //  Move Cursor
                    else if ((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && menuCursor < shopMenu.Length - 1)  //  //
                    {                                                                                                               //  //
                        menuCursor++;                                                                                               //  //
                        DrawShop(shopMenu);                                                                                         //  //
                    }                                                                                                               //  //
                    else if (input.Key == ConsoleKey.Enter || input.Key == ConsoleKey.Spacebar)                                     //      //
                    {                                                                                                               //      //
                        switch (menuCursor)                                                                                         //      //
                        {                                                                                                           //      //
                            case 0:                                                                                                 //      //
                                UpgradeGear(0);                                                                                     //      //
                                break;                                                                                              //      //
                            case 1:                                                                                                 //      //
                                UpgradeGear(1);                                                                                     //      //
                                break;                                                                                              //      //
                            case 2:                                                                                                 //      //  Choose Menu Option
                                selectingWeapon = true;                                                                             //      //
                                menuCursor = 0;                                                                                     //      //
                                DrawShop(weaponMenu);                                                                               //      //
                                break;                                                                                              //      //
                            case 3:                                                                                                 //      //
                                DisplayMap();                                                                                       //      //
                                storeOutY++;                                                                                        //      //
                                MoveToOut(storeOutX, storeOutY - 1);                                                                //      //
                                break;                                                                                              //      //
                        }                                                                                                           //      //
                    }                                                                                                               //
                }else if (inShop == true && selectingWeapon == true)                                                                    //  Selecting Weapon
                {                                                                                                                       //
                    if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && menuCursor > 0)                               //      //
                    {                                                                                                                   //      //
                        menuCursor--;                                                                                                   //      //
                        DrawShop(weaponMenu);                                                                                           //      //
                    }                                                                                                                   //      //  Move Cursor
                    else if ((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && menuCursor < shopMenu.Length - 1)      //      //
                    {                                                                                                                   //      //
                        menuCursor++;                                                                                                   //      //
                        DrawShop(weaponMenu);                                                                                           //      //
                    }                                                                                                                   //      //
                    else if (input.Key == ConsoleKey.Enter || input.Key == ConsoleKey.Spacebar)                                         //  //
                    {                                                                                                                   //  //
                        selectingWeapon = false;                                                                                        //  //
                        currentWeaponType = menuCursor;                                                                                 //  //  Select Weapon
                        menuCursor = 0;                                                                                                 //  //
                        DrawShop(shopMenu);                                                                                             //  //
                    }                                                                                                                   //  //
                }                                                                                                                       //
            }
        }

        //Displays the main map
        static void DisplayMap(int scale = 1)
        {
            inDungeon = false;
            inShop = false;

            Console.CursorVisible = false;
            globalScale = scale;
            Console.Clear();
            Console.ResetColor();

            Console.WriteLine("┌───────────────┬─────────────┬───────────┬───────────┬───────────┬──────────┬───────────┬─────────────┐");   //Draw Legend Top Border
            Console.Write("│");                                                                                             //
            Console.BackgroundColor = ConsoleColor.DarkGray;                                                                //
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" ^ = Mountains ");                                                                               //
            Console.ResetColor();                                                                                           //
            Console.Write("│");                                                                                             //
            Console.BackgroundColor = ConsoleColor.Green;                                                                   //
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" █ = Dungeon ");                                                                                 //
            Console.ResetColor();                                                                                           //
            Console.Write("│");                                                                                             //
            Console.BackgroundColor = ConsoleColor.Green;                                                                   //
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" ` = Grass ");                                                                                   //
            Console.ResetColor();                                                                                           //
            Console.Write("│");                                                                                             //
            Console.BackgroundColor = ConsoleColor.Cyan;                                                                    //  Draw Legend
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" ~ = Water ");                                                                                   //
            Console.ResetColor();                                                                                           //
            Console.Write("│");                                                                                             //
            Console.BackgroundColor = ConsoleColor.DarkGreen;                                                               //
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" * = Trees ");                                                                                   //
            Console.ResetColor();                                                                                           //
            Console.Write("│");                                                                                             //
            Console.BackgroundColor = ConsoleColor.DarkYellow;                                                              //
            Console.ForegroundColor = ConsoleColor.Black;                                                                   //
            Console.Write(" $ = Shop ");                                                                                    //
            Console.ResetColor();                                                                                           //
            Console.WriteLine("│ Scale = " + scale + " │ Coins = " + PlayerStats["Coins"].ToString("000") + " │");          //
            Console.WriteLine("└───────────────┴─────────────┴───────────┴───────────┴───────────┴──────────┴───────────┴─────────────┘");   //Draw Legend Bottom Border
            

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
                                    Console.Write(map[i, k]);                           //
                                    break;                                              //
                                case '`':                                               //
                                    Console.BackgroundColor = ConsoleColor.Green;       //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    Console.Write(map[i, k]);                           //
                                    break;                                              //
                                case '~':                                               //
                                    Console.BackgroundColor = ConsoleColor.Cyan;        //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    Console.Write(map[i, k]);                           //
                                    break;                                              //
                                case '*':                                               //
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;   //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    Console.Write(map[i, k]);                           //
                                    break;                                              //
                                case '$':                                               //
                                    Console.BackgroundColor = ConsoleColor.DarkYellow;  //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    Console.Write(map[i, k]);                           //
                                    break;                                              //
                                default:                                                //
                                    Console.BackgroundColor = ConsoleColor.Green;       //
                                    Console.ForegroundColor = ConsoleColor.Black;       //
                                    Console.Write('█');                                 //
                                    break;                                              //
                            }                                                           //
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

        //Displays the selected dungeon
        static void DisplayDungeon(int dungeon = 0)
        {
            Console.ResetColor();
            Console.CursorVisible = false;
            char[,] dunMap = dungeons[dungeon];
            currentDungeon = dungeon;
            inDungeon = true;

            Console.Clear();

            Console.WriteLine("┌───────────┬───────────┬──────────┬──────────────────┬────────────────┬─────────────┐");  //Draw Legend Top Border
            Console.Write("│");                                                                                                 //
            Console.BackgroundColor = ConsoleColor.DarkGray;                                                                    //
            Console.ForegroundColor = ConsoleColor.Black;                                                                       //
            Console.Write(" ║ = Walls ");                                                                                       //
            Console.ResetColor();                                                                                               //
            Console.Write("│");                                                                                                 //
            Console.BackgroundColor = ConsoleColor.Gray;                                                                        //
            Console.ForegroundColor = ConsoleColor.Black;                                                                       //
            Console.Write(" , = Floor ");                                                                                       //
            Console.ResetColor();                                                                                               //
            Console.Write("│");                                                                                                 //
            Console.BackgroundColor = ConsoleColor.DarkGray;                                                                    //  Draw Legend
            Console.ForegroundColor = ConsoleColor.Black;                                                                       //
            Console.Write(" █ = Exit ");                                                                                        //
            Console.ResetColor();                                                                                               //
            Console.Write("│");                                                                                                 //
            Console.BackgroundColor = ConsoleColor.Gray;                                                                        //
            Console.ForegroundColor = ConsoleColor.DarkYellow;                                                                  //
            Console.Write(" = = Closed Chest ");                                                                                //
            Console.ResetColor();                                                                                               //
            Console.Write("│");                                                                                                 //
            Console.BackgroundColor = ConsoleColor.Gray;                                                                        //
            Console.ForegroundColor = ConsoleColor.DarkYellow;                                                                  //
            Console.Write(" ≈ = Open Chest ");                                                                                  //
            Console.ResetColor();                                                                                               //
            Console.WriteLine("│ Coins = " + PlayerStats["Coins"].ToString("000") + " │");                                      //
            Console.WriteLine("└───────────┴───────────┴──────────┴──────────────────┴────────────────┴─────────────┘");  //Draw Legend Bottom Border


            Console.Write("┌");                                             //
            for (int i = 0; i < dunMap.GetLength(1); i++)                   //
            {                                                               //  Draw Top Border
                Console.Write("─");                                         //
            }                                                               //
            Console.WriteLine("┐");                                         //

            for (int i = 0; i < dunMap.GetLength(0); i++)                       //
            {                                                                   //
                Console.Write("│");                                             //
                for (int j = 0; j < dunMap.GetLength(1); j++)                   //
                {                                                               //Draw Map
                    switch (dunMap[i, j])                                       //
                    {                                                           //
                        case ',':                                               //  //
                            Console.BackgroundColor = ConsoleColor.Gray;        //  //
                            Console.ForegroundColor = ConsoleColor.Black;       //  //Floor
                            Console.Write(dunMap[i, j]);                        //  //
                            break;                                              //  //
                        case '=':                                               //      //
                            Console.BackgroundColor = ConsoleColor.Gray;        //      //
                            Console.ForegroundColor = ConsoleColor.DarkYellow;  //      //
                            Console.Write(dunMap[i, j]);                        //      //
                            break;                                              //      //
                        case '≈':                                               //      //Chests
                            Console.BackgroundColor = ConsoleColor.Gray;        //      //
                            Console.ForegroundColor = ConsoleColor.DarkYellow;  //      //
                            Console.Write(dunMap[i, j]);                        //      //
                            break;                                              //      //
                        case '!':                                               //          //
                            Console.BackgroundColor = ConsoleColor.Gray;        //          //
                            Console.ForegroundColor = ConsoleColor.DarkYellow;  //          //Mimic
                            Console.Write('=');                                 //          //
                            break;                                              //          //
                        case ' ':                                               //      //
                            Console.ResetColor();                               //      //Empty Space
                            Console.Write(dunMap[i, j]);                        //      //
                            break;                                              //      //
                        default:                                                //  //
                            Console.BackgroundColor = ConsoleColor.DarkGray;    //  //
                            Console.ForegroundColor = ConsoleColor.Black;       //  //Walls and Exit
                            Console.Write(dunMap[i, j]);                        //  //
                            break;                                              //  //
                    }                                                           //
                    Console.ResetColor();                                       //
                }                                                               //
                Console.WriteLine("│");                                         //
                                                                                //
            }                                                                   //


            Console.Write("└");                                         //
            for (int i = 0; i < dunMap.GetLength(1); i++)               //
            {                                                           //  Draw Bottom Border
                Console.Write("─");                                     //
            }                                                           //
            Console.WriteLine("┘");                                     //

            Console.ResetColor();

        }

        //Sets up the game
        static void StartGameLoop()
        {
            Console.Clear();                        //
            Console.WriteLine("Now to explore!");   //  Introductory Message and Display Map
            Console.ReadKey(true);                  //
            DisplayMap();                           //

            storeOutX = (map.GetLength(1) * globalScale) / 2;   //
            storeOutY = (map.GetLength(0) * globalScale) / 2;   //  Place Player In Map's Center
            MoveToOut(storeOutX, storeOutY-1);                  //
        }

        //Moves the player to the position on the overworld
        static void MoveToOut(int x, int y)
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
                    case '0':                   //
                        DisplayDungeon(0);      //
                        storeDungeonX = 1;      //
                        storeDungeonY = 6;      //
                        MoveToDun(1, 7);        //
                        return;                 //
                    case '1':                   //
                        DisplayDungeon(1);      //
                        storeDungeonX = 3;      //
                        storeDungeonY = 6;      //
                        MoveToDun(3, 7);        //
                        return;                 //
                    case '2':                   //  Place Player In Appropriate Dungeon
                        DisplayDungeon(2);      //
                        storeDungeonX = 13;     //
                        storeDungeonY = 6;      //
                        MoveToDun(13, 7);       //
                        return;                 //
                    case '3':                   //
                        DisplayDungeon(3);      //
                        storeDungeonX = 2;      //
                        storeDungeonY = 6;      //
                        MoveToDun(2, 7);        //
                        return;                 //
                    case '$':                       //
                        menuCursor = 0;             //  Place Player In Shop
                        DrawShop(shopMenu);         //
                        return;                     //
                }

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

        //Moves the player to the position in the current dungeon
        static void MoveToDun(int x, int y)
        {
            Console.ResetColor();
            
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            switch(dungeons[currentDungeon][y, x])                                                              //
            {                                                                                                   //
                case ',':                                                                                       //
                    Console.SetCursorPosition(x + 1, y + 4);                                                    //
                    Console.Write('O');                                                                         //
                    dontMove = false;                                                                           //
                    break;                                                                                      //Move Player
                case '≈':                                                                                       //
                    Console.SetCursorPosition(x + 1, y + 4);                                                    //
                    Console.Write('O');                                                                         //
                    dontMove = false;                                                                           //
                    break;                                                                                      //
                case '=':                                                                                       //  //
                    dungeons[currentDungeon][y, x] = '≈';                                                       //  //
                    var rand = new Random();                                                                    //  //
                    int potionToGain = 0;                                                                       //  //
                    int coinToGain = 0;                                                                         //  //
                    for (int i = 0; i < 10; i++)                                                                //  //
                    {                                                                                           //  //
                        if (rand.Next(0, 5) == 0)                                                               //  //
                        {                                                                                       //  //
                            potionToGain++;                                                                     //  //
                        }                                                                                       //  //
                        else                                                                                    //  //
                        {                                                                                       //  //
                            coinToGain += rand.Next(2, 11);                                                     //  //
                        }                                                                                       //  //Determine Loot from Chest
                    }                                                                                           //  //
                    PlayerStats["Potions"] += potionToGain;                                                     //  //
                    PlayerStats["Coins"] += coinToGain;                                                         //  //
                    Console.ResetColor();                                                                       //  //
                    Console.Clear();                                                                            //  //
                    DisplayDungeon(currentDungeon);                                                             //  //
                    Console.SetCursorPosition(x + 1, y + 4);                                                    //  //
                    Console.BackgroundColor = ConsoleColor.Gray;                                                //  //
                    Console.ForegroundColor = ConsoleColor.Black;                                               //  //
                    Console.Write('O');                                                                         //  //
                    Console.SetCursorPosition(5, 15);                                                           //  //
                    Console.ResetColor();                                                                       //  //
                    Console.Write("You Gained " + potionToGain + " Potions And " + coinToGain + " Coins!");     //  //
                    Console.ReadKey(true);                                                                      //  //
                    Console.Clear();                                                                            //  //
                    DisplayDungeon(currentDungeon);                                                             //  //
                    Console.SetCursorPosition(x + 1, y + 4);                                                    //  //
                    Console.BackgroundColor = ConsoleColor.Gray;                                                //  //
                    Console.ForegroundColor = ConsoleColor.Black;                                               //  //
                    Console.Write('O');                                                                         //  //
                    dontMove = false;                                                                           //  //
                    break;                                                                                      //  //
                case '!':                                                                                       //      //
                    dungeons[currentDungeon][y, x] = ',';                                                       //      //
                    enemy = "Mimic";                                                                            //      //
                    enemyHP = enemyMaxHealths[enemy];                                                           //      //Start Mimic Fight
                    menuCursor = 0;                                                                             //      //
                    battling = true;                                                                            //      //
                    playerTurn = true;                                                                          //      //
                    DrawBattle();                                                                               //      //
                    return;                                                                                     //      //
                case '█':                                                                                       //  //
                    Console.ResetColor();                                                                       //  //
                    DisplayMap(globalScale);                                                                    //  //
                    storeOutX++;                                                                                //  //  Exit Dungeon
                    storeOutY++;                                                                                //  //
                    MoveToOut(storeOutX - 1, storeOutY - 1);                                                    //  //
                    return;                                                                                     //  //
                default:                                                                                        //      //
                    dontMove = true;                                                                            //      //  There's a wall here
                    break;                                                                                      //      //
            }                                                                                                   //

            if (dontMove == false)                                                              //
            {                                                                                   //
                switch (dungeons[currentDungeon][storeDungeonY, storeDungeonX])                 //
                {                                                                               //
                    case ',':                                                                   //
                        Console.BackgroundColor = ConsoleColor.Gray;                            //
                        Console.ForegroundColor = ConsoleColor.Black;                           //
                        Console.SetCursorPosition(storeDungeonX + 1, storeDungeonY + 4);        //
                        Console.Write(dungeons[currentDungeon][storeDungeonY, storeDungeonX]);  //
                        storeDungeonX = x;                                                      //
                        storeDungeonY = y;                                                      //
                        break;                                                                  //
                    case '≈':                                                                   //
                        Console.BackgroundColor = ConsoleColor.Gray;                            //
                        Console.ForegroundColor = ConsoleColor.DarkYellow;                      //Replace old sprite
                        Console.SetCursorPosition(storeDungeonX + 1, storeDungeonY + 4);        //
                        Console.Write(dungeons[currentDungeon][storeDungeonY, storeDungeonX]);  //
                        storeDungeonX = x;                                                      //
                        storeDungeonY = y;                                                      //
                        break;                                                                  //
                    default:                                                                    //
                        Console.BackgroundColor = ConsoleColor.Gray;                            //
                        Console.ForegroundColor = ConsoleColor.Black;                           //
                        Console.SetCursorPosition(storeDungeonX + 1, storeDungeonY + 4);        //
                        Console.Write(dungeons[currentDungeon][storeDungeonY, storeDungeonX]);  //
                        storeDungeonX = x;                                                      //
                        storeDungeonY = y;                                                      //
                        break;                                                                  //
                }                                                                               //
            }                                                                                   //
            
        }

        //Draws the battle and battle menu
        static void DrawBattle()
        {
            Console.ResetColor();
            Console.Clear();

            switch (enemy)                                                  //
            {                                                               //
                case "Imp":                                                 //
                    Console.ForegroundColor = ConsoleColor.Red;             //
                    break;                                                  //
                case "Goblin":                                              //
                    Console.ForegroundColor = ConsoleColor.Green;           //
                    break;                                                  //  Pick Enemy Color
                case "Slime":                                               //
                    Console.ForegroundColor = ConsoleColor.Blue;            //
                    break;                                                  //
                case "Mimic":                                               //
                    Console.ForegroundColor = ConsoleColor.DarkYellow;      //
                    break;                                                  //
            }

            foreach (string line in sprites[enemy])     //
            {                                           //  Draw Enemy Sprite
                Console.WriteLine(line);                //
            }                                           //

            Console.ResetColor();

                                                                        //  Draw Hud
            Console.SetCursorPosition(0, 17);                           //  //
            Console.WriteLine("Player");                                //  //
            Console.WriteLine("HP: " + PlayerStats["HP"]);              //  //  Player Side
            Console.WriteLine("Potions: " + PlayerStats["Potions"]);    //  //
                                                                        //
            Console.SetCursorPosition(20, 17);                          //      //
            Console.WriteLine(enemy);                                   //      //  Enemy Side
            Console.SetCursorPosition(20, 18);                          //      //
            Console.WriteLine("HP: " + enemyHP);                        //      //

            Console.SetCursorPosition(5, 21);   //
            Console.WriteLine(battleMenu[0]);   //
            Console.SetCursorPosition(5, 23);   //
            Console.WriteLine(battleMenu[1]);   //Draw Menu
            Console.SetCursorPosition(5, 25);   //
            Console.WriteLine(battleMenu[2]);   //

            if (playerTurn)                                             //
            {                                                           //
                Console.ForegroundColor = ConsoleColor.Red;             //
                Console.SetCursorPosition(3, 21 + (menuCursor * 2));    //  Draw Cursor
                Console.Write(">");                                     //
                Console.SetCursorPosition(24, 21 + (menuCursor * 2));   //
                Console.Write("<");                                     //
            }                                                           //

            Console.ResetColor();

            DrawPlayer(26); // Draw Player

        }

        //Ends the battle and brings the player back to the map they were on at the start of the battle
        static void EndBattle()
        {
            battling = false;   //  Reset Game State
            Console.Clear();    //
            if (inDungeon == false)                                                         //
            {                                                                               //
                DisplayMap(globalScale);                                                    //
                Console.ForegroundColor = ConsoleColor.Black;                               //
                switch (map[storeOutY / globalScale, storeOutX / globalScale])              //
                {                                                                           //
                    case '^':                                                               //
                        Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);            //
                        Console.BackgroundColor = ConsoleColor.DarkGray;                    //
                        Console.Write("O");                                                 //
                        dontMove = false;                                                   //
                        break;                                                              //
                    case '`':                                                               //
                        Console.SetCursorPosition(storeOutX + 1, storeOutY + 4);            //  Draw Player On Overworld
                        Console.BackgroundColor = ConsoleColor.Green;                       //
                        Console.Write("O");                                                 //
                        dontMove = false;                                                   //
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
            else                                                                //
            {                                                                   //
                DisplayDungeon(currentDungeon);                                 //
                Console.SetCursorPosition(storeDungeonX+1, storeDungeonY+4);    //  Draw Player In Dungeon
                Console.BackgroundColor= ConsoleColor.Gray;                     //
                Console.ForegroundColor = ConsoleColor.Black;                   //
                Console.Write("O");                                             //
                dontMove=false;                                                 //
            }                                                                   //
        }

        //Starts the battle with the selected enemy
        static void StartBattle(int select)
        {
            menuCursor = 0;     //
            battling = true;    //  Set Game State
            playerTurn = true;  //
            if (select == 0)                                    //
            {                                                   //
                enemy = "Imp";                                  //
                Console.ForegroundColor = ConsoleColor.Red;     //
            }                                                   //
            else if (select >= 1 && select <= 4)                //
            {                                                   //
                enemy = "Goblin";                               //
                Console.ForegroundColor = ConsoleColor.Green;   //  Select enemy
            }                                                   //
            else                                                //
            {                                                   //
                enemy = "Slime";                                //
                Console.ForegroundColor = ConsoleColor.Blue;    //
            }                                                   //

            enemyHP = enemyMaxHealths[enemy];   //  Starts Battle
            DrawBattle();                       //
                        
        }

        //Deals an amount of damage to the enemy equal to the players ATK stat
        static void AttackEnemy()
        {
            enemyHP -= PlayerStats["ATK"];  //Deals DMG
            if(enemyHP < 0)     //
            {                   //  Range Check
                enemyHP = 0;    //
            }                   //

            playerTurn = false;                                         //
            DrawBattle();                                               //
            Console.SetCursorPosition(5, 20);                           //  Finish Player Turn
            Console.Write("You Dealt " + PlayerStats["ATK"] + " DMG!"); //
            Console.ReadKey(true);                                      //

            if(enemyHP > 0) //Deatch Check
            {
                EnemyTurn();
            }
            else                    //
            {                       //
                DetermineLoot();    //  Dead Enemy
                EndBattle();        //
            }                       //
        }

        //Heals the player and uses up a potion
        static void UsePotion()
        {
            if (PlayerStats["Potions"] > 0) //Potion Has Check
            {
                PlayerStats["Potions"]--;                       //  Heal Player
                PlayerStats["HP"] += potionHeal;                //

                if (PlayerStats["HP"] > PlayerStats["Max HP"])      //
                {                                                   //  Range Check
                    PlayerStats["HP"] = PlayerStats["Max HP"];      //
                }                                                   //

                playerTurn = false;                             //
                DrawBattle();                                   //
                Console.SetCursorPosition(5, 20);               //  Finish Player Turn
                Console.Write("You Drank A Health Potion!");    //
                Console.ReadKey(true);                          //
                EnemyTurn();                                    //
            }
            else                                                        //
            {                                                           //
                DrawBattle();                                           //  Potionless
                Console.SetCursorPosition(5, 20);                       //
                Console.Write("You Don't Have Any Potions To Use!");    //
            }                                                           //
        }

        //Player tries to run away from the enemy, has a chance to fail
        static void Run()
        {
            playerTurn = false; //  Finish Player Turn
            var Rand = new Random();                                    //  Attempt To Run
            if (Rand.Next(1, 16) >= runDifficulties[enemy])             //  //
            {                                                           //  //
                DrawBattle();                                           //  //
                Console.SetCursorPosition(5, 20);                       //  //  Success
                Console.Write("You Ran Away Successfully!");            //  //
                Console.ReadKey(true);                                  //  //
                EndBattle();                                            //  //
            }                                                           //  //
            else                                                        //      //
            {                                                           //      //
                DrawBattle();                                           //      //
                Console.SetCursorPosition(5, 20);                       //      //  Failure, Turn Wasted
                Console.Write("The " + enemy + " Blocks Your Path!");   //      //
                Console.ReadKey(true);                                  //      //
                EnemyTurn();                                            //      //
            }                                                           //      //
        }

        //Deals damage to the player equal to the enemie's ATK stat, and then checks if the player is still alive.
        static void EnemyTurn()
        {
            PlayerStats["HP"] -= enemyAtks[enemy];  //  DMG Player

            if (PlayerStats["HP"] < 0)  //
            {                           //  Range Check
                PlayerStats["HP"] = 0;  //
            }                           //

            DrawBattle();                                                           //
            Console.SetCursorPosition(5, 20);                                       //  Finish Enemy Turn
            Console.Write("The " + enemy + " Dealt " + enemyAtks[enemy] + " DMG");  //
            Console.ReadKey(true);                                                  //

            if (PlayerStats["HP"] == 0) //
            {                           //  Death Check
                GameOver();             //
            }                           //
            else                    //
            {                       //
                playerTurn = true;  //  Continue To Player's Turn
                DrawBattle();       //
            }                       //
        }

        //Displays the Game Over message before ending the game loop, closing the game
        static void GameOver()
        {
            gameOver = true;
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("You Died!");
            Console.ReadKey(true);
        }

        //Gives a random amount of loot determined by the enemies ATK stat
        static void DetermineLoot()
        {
            var rand = new Random();    //
            int potionToGain = 0;       //  Prepares
            int coinToGain = 0;         //

            for(int i = 0; i < enemyAtks[enemy]; i++)   //  Gives Loot. Repeat An Amount Of Times Equal To Enemy ATK Stat
            {                                           //
                if(rand.Next(0, 5) == 0)                //  //
                {                                       //  //  Get One Potion
                    potionToGain++;                     //  //
                }                                       //  //
                else                                    //      //
                {                                       //      //
                    coinToGain += rand.Next(2, 11);     //      //  Get Between 2 and 11
                }                                       //      //
            }                                           //

            PlayerStats["Potions"] += potionToGain; //  Add Loot To Player Stats
            PlayerStats["Coins"] += coinToGain;     //

            DrawBattle();                                                                           //
            Console.SetCursorPosition(5, 20);                                                       //  Display Message
            Console.Write("You Gained " + potionToGain + " Potions And " + coinToGain + " Coins!"); //
            Console.ReadKey(true);                                                                  //
        }

        //Draws the player and their weapon with the appropriate colors and weapon sprite
        static void DrawPlayer(int spaceAbove)
        {
            Console.ResetColor();                                   //
            if(currentArmorRank == 0)                               //
            {                                                       //
                Console.ForegroundColor = ConsoleColor.DarkYellow;  //
            }else if(currentArmorRank == 1)                         //
            {                                                       //
                Console.ForegroundColor = ConsoleColor.Gray;        //  Determine Armor Color
            }                                                       //
            else if(currentArmorRank == 2)                          //
            {                                                       //
                Console.ForegroundColor = ConsoleColor.Green;       //
            }                                                       //

            Console.SetCursorPosition(0, spaceAbove);       //
            for (int i = 0; i < playerSprite.Length; i++)   //
            {                                               //  Draw Player
                Console.WriteLine(playerSprite[i]);         //
            }                                               //

            if (currentWeaponRank == 0)                             //
            {                                                       //
                Console.ForegroundColor = ConsoleColor.DarkYellow;  //
            }                                                       //
            else if (currentWeaponRank == 1)                        //
            {                                                       //
                Console.ForegroundColor = ConsoleColor.Gray;        //  Determine Weapon Color
            }                                                       //
            else if(currentWeaponRank == 2)                         //
            {                                                       //
                Console.ForegroundColor = ConsoleColor.Green;       //
            }                                                       //

            Console.SetCursorPosition(0, spaceAbove);                           //
            for (int i = 0; i < weapons[currentWeaponType].Length; i++)         //
            {                                                                   //
                for(int j = 0; j < weapons[currentWeaponType][i].Length; j++)   //
                {                                                               //
                    Console.SetCursorPosition(j, spaceAbove+i);                 //
                    if(weapons[currentWeaponType][i][j] != ' ')                 //  Draw Weapon Without Covering Player
                    {                                                           //
                        Console.Write(weapons[currentWeaponType][i][j]);        //
                    }                                                           //
                }                                                               //
            }                                                                   //

            Console.ResetColor();

        }

        //Draws the shop scene and the menus, both the Shop Menu and the Weapon Select Menu
        static void DrawShop(string[] menu)
        {
            Console.ResetColor();   //
                                    //
            inShop = true;          //  Prepares
                                    //
            Console.Clear();        //

            for (int i = 0; i < shopSprite.Length; i++)                     //  Draw Shop
            {                                                               //
                for (int j = 0; j < shopSprite[i].Length; j++)              //
                {                                                           //
                    if (shopWoodChars.Contains(shopSprite[i][j]))           //  //
                    {                                                       //  //
                        Console.ForegroundColor = ConsoleColor.DarkYellow;  //  //
                    } else if (shopMetalChars.Contains(shopSprite[i][j]))   //  //  Determine Color
                    {                                                       //  //
                        Console.ForegroundColor = ConsoleColor.DarkGray;    //  //
                    }                                                       //  //
                    Console.Write(shopSprite[i][j]);                        //      //  Draw Character
                }                                                           //
                Console.WriteLine("");                                      //
            }                                                               //

            Console.ResetColor();

            if (menu == shopMenu)                                                                                       //  Write Shop Options
            {                                                                                                           //
                Console.SetCursorPosition(5, 17);                                                                       //  //
                if (currentArmorRank + 1 < ArmorStats.Length)                                                           //  //
                {                                                                                                       //  //
                    Console.WriteLine(menu[0] + " | Costs " + ArmorStats[currentArmorRank + 1] * 10 + " Coins");        //  //
                }                                                                                                       //  //  Armor Option
                else                                                                                                    //  //
                {                                                                                                       //  //
                    Console.WriteLine("You have the best armor");                                                       //  //
                }                                                                                                       //  //
                Console.SetCursorPosition(5, 19);                                                                       //      //
                if (currentWeaponRank + 1 < WeaponStats.Length)                                                         //      //
                {                                                                                                       //      //
                    Console.WriteLine(menu[1] + " | Costs " + WeaponStats[currentWeaponRank + 1] * 10 + " Coins");      //      //
                }                                                                                                       //      //  Weapon Option
                else                                                                                                    //      //
                {                                                                                                       //      //
                    Console.WriteLine("You have the best weapon");                                                      //      //
                }                                                                                                       //      //
                Console.SetCursorPosition(5, 21);                                                                       //
                Console.WriteLine(menu[2]);                                                                             //
                                                                                                                        //
                Console.SetCursorPosition(5, 23);                                                                       //
                Console.WriteLine(menu[3]);                                                                             //
                                                                                                                        //
                Console.SetCursorPosition(5, 25);                                                                       //
                Console.Write("You have " + PlayerStats["Coins"] + " Coins.");                                          //
            }                                                                                                           //
            else if (menu == weaponMenu)                                        //
            {                                                                   //
                Console.SetCursorPosition(5, 17);                               //
                Console.WriteLine(ranks[currentWeaponRank] + " " + menu[0]);    //
                                                                                //
                Console.SetCursorPosition(5, 19);                               //
                Console.WriteLine(ranks[currentWeaponRank] + " " + menu[1]);    //  Write Weapon Menu
                                                                                //
                Console.SetCursorPosition(5, 21);                               //
                Console.WriteLine(ranks[currentWeaponRank] + " " + menu[2]);    //
                                                                                //
                Console.SetCursorPosition(5, 23);                               //
                Console.WriteLine(ranks[currentWeaponRank] + " " + menu[3]);    //
            }                                                                   //

            Console.ForegroundColor = ConsoleColor.Red;             //
            Console.SetCursorPosition(3, 17 + (menuCursor * 2));    //
            Console.Write(">");                                     //  Draw Cursor
            Console.SetCursorPosition(30, 17 + (menuCursor * 2));   //
            Console.Write("<");                                     //

            DrawPlayer(27); //  Draw Player
            
        }

        //Upgrades either the Armor or Weapon of the player
        static void UpgradeGear(int gearType) //0 = armor, 1 = weapon
        {
            Console.ResetColor();

            if (gearType == 0 && currentArmorRank+1 < ArmorStats.Length)        //  Armor  
            {                                                                   //
                int Cost = ArmorStats[currentArmorRank + 1] * 10;               //
                                                                                //
                if(PlayerStats["Coins"] >= Cost)                                //  //  Player Can Afford Upgrade
                {                                                               //  //
                    if(AreYouSure(gearType) == true)                            //  //  //
                    {                                                           //  //  //
                        currentArmorRank++;                                     //  //  //
                        PlayerStats["Max HP"] = ArmorStats[currentArmorRank];   //  //  //  Upgrades Armor Should Player Not Back Out
                        PlayerStats["HP"] = ArmorStats[currentArmorRank];       //  //  //
                        PlayerStats["Coins"] -= Cost;                           //  //  //
                        menuCursor = 0;                                         //  //  //
                        DrawShop(shopMenu);                                     //  //  //
                    }                                                           //  //  //
                    else                                                        //  //      //
                    {                                                           //  //      //
                        menuCursor = 0;                                         //  //      //  Player Backs Out
                        DrawShop(shopMenu);                                     //  //      //
                    }                                                           //  //      //
                }                                                               //  //
                else                                                            //      //  Player Too Broke
                {                                                               //      //
                    for (int i = 0; i < 8; i++)                                 //      //  //
                    {                                                           //      //  //
                        for (int j = 0; j < 35; j++)                            //      //  //
                        {                                                       //      //  //  Covers Menu
                            Console.SetCursorPosition(j, i + 17);               //      //  //
                            Console.Write(' ');                                 //      //  //
                        }                                                       //      //  //
                    }                                                           //      //  //
                                                                                //      //
                    Console.SetCursorPosition(5, 19);                           //      //      //
                    Console.Write("You can't afford it.");                      //      //      //  Displays Message And Returns To Shop
                    Console.ReadKey(true);                                      //      //      //
                    DrawShop(shopMenu);                                         //      //      //
                }                                                               //      //
            }                                                                   //
            else if(gearType == 1 && currentWeaponRank + 1 < WeaponStats.Length)    //  Weapon
            {                                                                       //
                int Cost = WeaponStats[currentWeaponRank + 1] * 10;                 //
                                                                                    //
                if (PlayerStats["Coins"] >= Cost)                                   //  //  Player Can Afford Upgrade
                {                                                                   //  //
                    if (AreYouSure(gearType) == true)                               //  //  //
                    {                                                               //  //  //
                        currentWeaponRank++;                                        //  //  //
                        PlayerStats["ATK"] = WeaponStats[currentWeaponRank];        //  //  //  Upgrades Weapons Should Player Not Back out
                        PlayerStats["Coins"] -= Cost;                               //  //  //
                        menuCursor = 0;                                             //  //  //
                        DrawShop(shopMenu);                                         //  //  //
                    }                                                               //  //  //
                    else                                                            //  //      //
                    {                                                               //  //      //
                        menuCursor = 0;                                             //  //      //  Player Backs Out
                        DrawShop(shopMenu);                                         //  //      //
                    }                                                               //  //      //
                }                                                                   //  //
                else                                                                //      //  Player Too Broke
                {                                                                   //      //
                    for (int i = 0; i < 8; i++)                                     //      //  //
                    {                                                               //      //  //
                        for (int j = 0; j < 35; j++)                                //      //  //
                        {                                                           //      //  //  Covers Menu
                            Console.SetCursorPosition(j, i + 17);                   //      //  //
                            Console.Write(' ');                                     //      //  //
                        }                                                           //      //  //
                    }                                                               //      //  //
                                                                                    //      //
                    Console.SetCursorPosition(5, 19);                               //      //      //
                    Console.Write("You can't afford it.");                          //      //      //  Displays Message And Returns To Shop
                    Console.ReadKey(true);                                          //      //      //
                    DrawShop(shopMenu);                                             //      //      //
                }                                                                   //      //
            }                                                                       //
        }

        //Allows the player to back out of upgrades
        static bool AreYouSure(int gearType)
        {
            bool Unsure = true; //  Prepares
            menuCursor = 0;     //
                        
            while (Unsure)  //  Loop
            {
                for (int i = 0; i < 8; i++)                     //
                {                                               //
                    for (int j = 0; j < 35; j++)                //
                    {                                           //  Covers Menu
                        Console.SetCursorPosition(j, i + 17);   //
                        Console.Write(' ');                     //
                    }                                           //
                }                                               //

                Console.ResetColor();

                Console.SetCursorPosition(5, 17);                                                                   //
                Console.Write("Are You Sure You Want To Buy ");                                                     //
                if(gearType == 0)                                                                                   //
                {                                                                                                   //
                    Console.Write("Some " + ranks[currentArmorRank + 1] + " Armor?");                               //  Promts Player
                }else if(gearType == 1)                                                                             //
                {                                                                                                   //
                    Console.Write("A " + ranks[currentWeaponRank + 1] + " " + weaponMenu[currentWeaponType] + "?"); //
                }                                                                                                   //

                Console.SetCursorPosition(5, 19);   //
                Console.Write("Yes");               //  Write Options
                Console.SetCursorPosition(5, 21);   //
                Console.Write("No");                //

                Console.ForegroundColor = ConsoleColor.Red;             //
                Console.SetCursorPosition(3, 19 + (menuCursor * 2));    //
                Console.Write(">");                                     //  Draw Cursors
                Console.SetCursorPosition(9, 19 + (menuCursor * 2));    //
                Console.Write("<");                                     //

                input = Console.ReadKey(true);                                                                  //  Menu Interaction
                                                                                                                //
                if ((input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.W) && menuCursor > 0)           //  //
                {                                                                                               //  //
                    menuCursor--;                                                                               //  //
                }                                                                                               //  //  Move Cursor
                else if ((input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.S) && menuCursor < 1)    //  //
                {                                                                                               //  //
                    menuCursor++;                                                                               //  //
                }                                                                                               //  //
                else if (input.Key == ConsoleKey.Enter || input.Key == ConsoleKey.Spacebar)                     //      //
                {                                                                                               //      //
                    Unsure = false;                                                                             //      //
                                                                                                                //      //
                    switch (menuCursor)                                                                         //      //
                    {                                                                                           //      //
                        case 0:                                                                                 //      //  Select Menu Option
                            return true;                                                                        //      //
                        case 1:                                                                                 //      //
                            return false;                                                                       //      //
                        default:                                                                                //      //
                            return false;                                                                       //      //
                    }                                                                                           //      //
                }                                                                                               //      //

            }

            return false;
            
        }

    }
}
