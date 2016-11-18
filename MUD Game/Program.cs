using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class Program
    {

        public enum gameStates { start, running, action, gameOver }
        public static gameStates gameState = gameStates.start;

        private static bool alive = true;
        private static int state = 0;
        public static string message;
        public static string actionList;
        public static string action;

        static void Main(string[] args)
        {
            while(alive)
            {
                if (Player.currentHP <= 0)
                {
                    gameState = gameStates.gameOver;
                }

                switch (gameState)
                {
                    case gameStates.start:
                        start();
                        break;

                    case gameStates.running:
                        running();
                        break;

                    case gameStates.action:
                        actionScene(action);
                        break;

                    case gameStates.gameOver:
                        gameOver();
                        break;
                }
            }
        }

        static void start()
        {
            message = "Hello! What is your name?";
            Console.WriteLine(message);
            Player.name = Console.ReadLine();
            Console.Clear();
            message = "Welcome, {0}! Your starting stats is: HP: {1}, Cash: {2}$";
            Console.WriteLine(message, Player.name, Player.currentHP, Player.cash);
            message = "Start your journey by pressing a button..";
            Console.WriteLine(message);
            Console.ReadLine();
            message = "";
            World.createMapList();
            gameState = gameStates.running;
            Action.actionsNewRoom();
        }

        static void running()
        {
            if (state == 1)
            {
                Console.WriteLine("Player Coord: " + Player.playerCoords[0] + "," + Player.playerCoords[1]);
                //Console.WriteLine("Map Coord: " + World.mapCoord[0] + "," + World.mapCoord[1]);
                Console.WriteLine("Room: " + World.currentRoom);
                Console.WriteLine(message);
                message = "";
                actionList = "[i]  Inventory  |  [e]  Interact  |  ";
                Console.SetCursorPosition(0,23);
                Action.action();
                Console.WriteLine(actionList);
            }

            World.getCurrentMapLayout(World.currentRoom);
            Player.move(Console.ReadKey().KeyChar);
            World.createMap();
            state = 1;

        }

        static void gameOver()
        {
            message = "Game Over!";
            Console.WriteLine(message);
            alive = false;
        }

        public static void actionScene(string action)
        {
            char key = Console.ReadKey().KeyChar;
            if (action == "buyItems")
            {
                Console.WriteLine("Køb ting her");
                actions(Console.ReadKey().KeyChar, action);
            }
            else if (action == "sellItems")
            {
                Console.WriteLine("Sælg ting her");
                actions(Console.ReadKey().KeyChar, action);
            }
            else if (action == "fight")
            {

            }

        }

        public static void actions(char key, string action)
        {
            switch (key)
            {
                case '1':
                    if(action == "buyItems")
                    {
                        //But item 1
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 1
                    }
                    break;

                case '2':
                    if (action == "buyItems")
                    {
                        //But item 2
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 2
                    }
                    break;

                case '3':
                    if (action == "buyItems")
                    {
                        //But item 3
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 3
                    }
                    break;

                case '4':
                    if (action == "buyItems")
                    {
                        //But item 4
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 4
                    }
                    break;

                case '5':
                    if (action == "buyItems")
                    {
                        //But item 5
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 5
                    }
                    break;

                case '6':
                    if (action == "buyItems")
                    {
                        //But item 6
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 6
                    }
                    break;

                case '7':
                    if (action == "buyItems")
                    {
                        //But item 7
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 7
                    }
                    break;

                case '8':
                    if (action == "buyItems")
                    {
                        //But item 8
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 8
                    }
                    break;

                case '9':
                    if (action == "buyItems")
                    {
                        //But item 9
                    }
                    else if (action == "sellItems")
                    {
                        //Sell item 9
                    }
                    break;

                case 'q':
                    Console.Clear();
                    gameState = gameStates.running;
                    World.getCurrentMapLayout(World.currentRoom);
                    World.createMap();
                    break;

            }
        }
        
    }
}
