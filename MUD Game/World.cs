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

        public static void createMap()
        {
            getCurrentMapLayout(currentRoom);

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

            switch(direction){

                case 'w':
                    if (currentMap[playerCoords[1] - 1][playerCoords[0]] == " ")
                    {
                        return true;
                    }
                    break;

                case 's':
                    if (currentMap[playerCoords[1] + 1][playerCoords[0]] == " ")
                    {
                        return true;
                    }
                    break;

                case 'a':
                    if (currentMap[playerCoords[1]][playerCoords[0] - 1] == " ")
                    {
                        return true;
                    }
                    break;

                case 'd':
                    if (currentMap[playerCoords[1]][playerCoords[0] + 1] == " ")
                    {
                        return true;
                    }
                    break;

                default:
                    return false;
                    break;

            }

            return false;
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
        }

    }
}
