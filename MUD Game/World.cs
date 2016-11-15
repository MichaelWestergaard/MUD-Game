using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class World
    {

        public static string[][] currentMap = new string[][] { };
        public static string currentRoom = "home";
        
        public static int[] mapCoord = new int[2] { 0, 0 };

        public static void createMap()
        {
            Console.Clear();
            for (int i = 0; i < currentMap.Length; i++)
            {
                for (int n = 0; n < currentMap[i].Length; n++)
                {
                    Console.Write(currentMap[i][n]);
                }
                Console.Write("\n");
            }
        }

        public static void placePlayer(int[] playerCoords)
        {
            currentMap[playerCoords[1]][playerCoords[0]] = "x";
        }

        public static void removePlayer(int[] playerCoords)
        {
            currentMap[playerCoords[1]][playerCoords[0]] = " ";
        }

        public static bool walkable(int[] playerCoords, char direction)
        {
            //W = Up, S = Down, A = Left and D = Right
            //Checks if the string in the direction is empty
            //If it is empty, then return true else return false;

            string cell;

            switch(direction){

                case 'w':
                    cell = currentMap[playerCoords[1] - 1][playerCoords[0]];
                    if (cell == " ")
                    {
                        return true;
                    }
                    else if (cell == "_")
                    {
                        changeRoom(direction);
                    }
                    break;

                case 's':
                    cell = currentMap[playerCoords[1] + 1][playerCoords[0]];
                    if (cell == " ")
                    {
                        return true;
                    }
                    else if (cell == "_")
                    {
                        changeRoom(direction);
                    }
                    break;

                case 'a':
                    cell = currentMap[playerCoords[1]][playerCoords[0] - 1];
                    if (cell == " ")
                    {
                        return true;
                    }
                    else if (cell == "|")
                    {
                        changeRoom(direction);
                    }
                    break;

                case 'd':
                    cell = currentMap[playerCoords[1]][playerCoords[0] + 1];
                    if (cell == " ")
                    {
                        return true;
                    }
                    else if (cell == "|")
                    {
                        changeRoom(direction);
                    }
                    break;

                default:
                    return false;
                    break;

            }

            return false;
        }

        public static void changeRoom(char direction)
        {
            switch (direction)
            {

                case 'w':
                    mapCoord[0]--;
                    //Ændre currentRoom
                    break;

                case 's':
                    mapCoord[0]++;
                    break;

                case 'a':
                    mapCoord[1]--;
                    break;

                case 'd':
                    mapCoord[1]++;
                    break;

                default:
                    break;

            }
        }

        //Maps

        public static void getCurrentMapLayout(string currentRoom)
        {
            if (currentRoom == "home")
            {
                string[] r1  = { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
                string[] r2  = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r3  = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r4  = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r5  = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r6  = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r7 = { "#", "#", "#", "#", "#", "_", "#", "#", "#", "#", "#" };

                currentMap = new string[][] { r1, r2, r3, r4, r5, r6, r7 };
            }
            else if (currentRoom == "frontyard")
            {
                string[] r1 = { "#", "#", "#", "#", "#", "_", "#", "#", "#", "#", "#" };
                string[] r2 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r3 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r4 = { "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", "|" };
                string[] r5 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r6 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r7 = { "#", "#", "#", "#", "#", "_", "#", "#", "#", "#", "#" };

                currentMap = new string[][] { r1, r2, r3, r4, r5, r6, r7 };
            }
        }

    }
}
