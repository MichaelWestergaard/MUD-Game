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

        public static List<Tuple<string, int, int>> mapList = new List<Tuple<string, int, int>>();
        public static List<Tuple<string, char, int, int>> mapCoordList = new List<Tuple<string, char, int, int>>();

        public static void createMapList()
        {
            //Add map to map list. ("name of map/room", x-coord, y-coord)
            mapList.Add(new Tuple<string, int, int>("home", 0, 0));
            mapList.Add(new Tuple<string, int, int>("frontyard", 0, -1));

            //Add spawning point to the rooms

            //home
            mapCoordList.Add(new Tuple<string, char, int, int>("home", 'w', 5, 6));

            //frontyard
            mapCoordList.Add(new Tuple<string, char, int, int>("frontyard", 'w', 5, 5));
            mapCoordList.Add(new Tuple<string, char, int, int>("frontyard", 's', 5, 0));
            mapCoordList.Add(new Tuple<string, char, int, int>("frontyard", 'a', 10, 3));
            mapCoordList.Add(new Tuple<string, char, int, int>("frontyard", 'd', 0, 3));
        }

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
                        return true;
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
                        return true;
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
                        return true;
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
                        return true;
                    }
                    break;

            }

            return false;
        }

        public static void changeRoom(char direction)
        {
            switch (direction)
            {

                case 'w':
                    mapCoord[1]++;
                    break;

                case 's':
                    mapCoord[1]--;
                    break;

                case 'a':
                    mapCoord[0]--;
                    break;

                case 'd':
                    mapCoord[0]++;
                    break;

                default:
                    break;

            }
            setNewRoom(direction, mapCoord);
            getCurrentMapLayout(currentRoom);
        }

        public static void setNewRoom(char direction, int[] mapCoord)
        {
            //Find the new map in the map list
            foreach(var item in mapList)
            {
                //If x- & y-coord is equals to the new map coords
                if (item.Item2.Equals(mapCoord[0]) && item.Item3.Equals(mapCoord[1]))
                {
                    //Set new currentRoom (Item1 in list) and set the coords for the player in the new room
                    currentRoom = item.Item1;

                    //Set player coords for new room
                    foreach (var spawnCoord in mapCoordList)
                    {
                        if (spawnCoord.Item1.Equals(currentRoom) && spawnCoord.Item2.Equals(direction))
                        {
                            Player.playerCoords[0] = spawnCoord.Item3;
                            Player.playerCoords[1] = spawnCoord.Item4;
                        }
                    }
                }
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
