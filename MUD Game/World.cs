﻿using System;
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
        
        public static List<NPC> NPCList = new List<NPC>();
        
        public static void createMapList()
        {
            //Add map to map list. ("name of map/room", x-coord, y-coord)
            mapList.Add(new Tuple<string, int, int>("home", 0, 0));
            mapList.Add(new Tuple<string, int, int>("littleShop", 1, 0));
            mapList.Add(new Tuple<string, int, int>("street", 0, -1));
            mapList.Add(new Tuple<string, int, int>("streetRight1", 1, -1));
            mapList.Add(new Tuple<string, int, int>("streetRight2", 2, -1));
            mapList.Add(new Tuple<string, int, int>("forest", 2, 0));
            mapList.Add(new Tuple<string, int, int>("desert", 2, -2));
            mapList.Add(new Tuple<string, int, int>("mine", 3, -1));

            //Add spawning point to the rooms

            //home
            mapCoordList.Add(new Tuple<string, char, int, int>("home", 'w', 5, 6));
            
            //shop
            mapCoordList.Add(new Tuple<string, char, int, int>("littleShop", 'w', 5, 4));

            //frontyard
            mapCoordList.Add(new Tuple<string, char, int, int>("street", 'w', 5, 6));
            mapCoordList.Add(new Tuple<string, char, int, int>("street", 's', 5, 0));
            mapCoordList.Add(new Tuple<string, char, int, int>("street", 'a', 10, 3));
            mapCoordList.Add(new Tuple<string, char, int, int>("street", 'd', 0, 3));

            //streetRight1
            mapCoordList.Add(new Tuple<string, char, int, int>("streetRight1", 'w', 5, 6));
            mapCoordList.Add(new Tuple<string, char, int, int>("streetRight1", 's', 5, 0));
            mapCoordList.Add(new Tuple<string, char, int, int>("streetRight1", 'a', 10, 3));
            mapCoordList.Add(new Tuple<string, char, int, int>("streetRight1", 'd', 0, 3));

            //streetRight1
            mapCoordList.Add(new Tuple<string, char, int, int>("streetRight2", 'w', 5, 6));
            mapCoordList.Add(new Tuple<string, char, int, int>("streetRight2", 's', 5, 0));
            mapCoordList.Add(new Tuple<string, char, int, int>("streetRight2", 'a', 10, 3));
            mapCoordList.Add(new Tuple<string, char, int, int>("streetRight2", 'd', 0, 3));
            
            //mine
            mapCoordList.Add(new Tuple<string, char, int, int>("mine", 'd', 0, 3));

            //forest
            mapCoordList.Add(new Tuple<string, char, int, int>("forest", 'w', 5, 6));

            //desert
            mapCoordList.Add(new Tuple<string, char, int, int>("desert", 's', 5, 0));

            //Add different actions to each room
            //"Room Name", x-coord, y-coord - -1, -1 = everywhere in the room
            Action.roomActionList.Add(new Tuple<string, string, int, int>("mine", "Mine ores", -1, -1));
            Action.roomActionList.Add(new Tuple<string, string, int, int>("forest", "Cut trees", -1, -1));
            Action.roomActionList.Add(new Tuple<string, string, int, int>("desert", "Dig out sand", -1, -1));

            //Add NPCs
            NPCList.Add(new NPC("Buyer", "M", "littleShop", 2, 2));
            NPCList.Add(new NPC("Seller", "M", "littleShop", 8, 2));
            NPCList.Add(new NPC("Pleb", "P", "street", 8, 1));
            NPCList.Add(new NPC("Blocker", "B", "street", 1, 3));


            //Add items to shop
            Program.itemsInShop.Add(new Item("Pickaxe", 1, 10, 10, 10, 5, false));
            Program.itemsInShop.Add(new Item("Axe", 1, 10, 10, 10, 5, false));
            Program.itemsInShop.Add(new Item("Shovel", 1, 10, 10, 10, 5, false));
            Program.itemsInShop.Add(new Item("Sword", 1, 10, 10, 20, 7, false));
            Program.itemsInShop.Add(new Item("Health Potion", 2, 1, 1, 50, 5, true));
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
            Action.actionsNewRoom();
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
                string[] r1 = { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
                string[] r2 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r3 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r4 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r5 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r6 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r7 = { "#", "#", "#", "#", "#", "_", "#", "#", "#", "#", "#" };

                currentMap = new string[][] { r1, r2, r3, r4, r5, r6, r7 };

            }
            else if (currentRoom == "littleShop")
            {
                string[] r1 = { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
                string[] r2 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r3 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r4 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r5 = { "#", "#", "#", "#", "#", "_", "#", "#", "#", "#", "#" };

                currentMap = new string[][] { r1, r2, r3, r4, r5 };

            }
            else if (currentRoom == "street")
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
            else if (currentRoom == "streetRight1")
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
            else if (currentRoom == "streetRight2")
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
            else if (currentRoom == "mine")
            {
                string[] r1 = { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
                string[] r2 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r3 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r4 = { "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r5 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r6 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r7 = { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };

                currentMap = new string[][] { r1, r2, r3, r4, r5, r6, r7 };

            }
            else if (currentRoom == "forest")
            {
                string[] r1 = { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };
                string[] r2 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r3 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r4 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r5 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r6 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r7 = { "#", "#", "#", "#", "#", "_", "#", "#", "#", "#", "#" };

                currentMap = new string[][] { r1, r2, r3, r4, r5, r6, r7 };

            }
            else if (currentRoom == "desert")
            {
                string[] r1 = { "#", "#", "#", "#", "#", "_", "#", "#", "#", "#", "#" };
                string[] r2 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r3 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r4 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r5 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r6 = { "#", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#" };
                string[] r7 = { "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" };

                currentMap = new string[][] { r1, r2, r3, r4, r5, r6, r7 };

            }

            //Place the NPCs in the current room
            foreach (var npc in NPCList)
            {
                if (npc.room == currentRoom)
                {
                    currentMap[npc.yCoord][npc.xCoord] = npc.icon;
                }
            }
        }

    }
}