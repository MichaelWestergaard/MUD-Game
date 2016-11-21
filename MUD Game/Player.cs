using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class Player
    {

        public static string name;
        
        public static int maxHP = 100;
        public static int currentHP = 100;
        public static int cash = 0;
        public static int score = 0;

        public static int[] playerCoords = new int[2] { 4, 4 };
        public static List<Item> Inventory = new List<Item>();
        public static string itemList;
        
        public static void move(char key)
        {
            if (World.walkable(playerCoords, key) == true)
            {
                //If the direction is walkable, then first remove the player from the old place
                //and then change the coordinates of the player and last place the player again.
                switch (key)
                {
                    case 'w':
                        World.removePlayer(playerCoords);
                        playerCoords[1]--;
                        World.placePlayer(playerCoords);
                        break;

                    case 's':
                        World.removePlayer(playerCoords);
                        playerCoords[1]++;
                        World.placePlayer(playerCoords);
                        break;

                    case 'a':
                        World.removePlayer(playerCoords);
                        playerCoords[0]--;
                        World.placePlayer(playerCoords);
                        break;

                    case 'd':
                        World.removePlayer(playerCoords);
                        playerCoords[0]++;
                        World.placePlayer(playerCoords);
                        break;
                        
                }
                char lastDir = key;
            }
            else
            {
                World.placePlayer(playerCoords);
            }
            
            switch (key)
            {
                case 'c':
                    cash += 50;
                    break;
                    
                case 'h':
                    Action.heal();
                    break;

                case 'i':
                    showInventory();
                    break;

                case ' ':
                    if(World.currentRoom == "mine")
                    {
                        Action.mine();
                    }
                    else if(World.currentRoom == "forest")
                    {
                        Action.cut();
                    }
                    else if (World.currentRoom == "desert")
                    {
                        Action.dig(); 
                    }
                    else if(World.currentRoom == "littleShop")
                    {
                        if (nextToNPC("Buyer"))
                        {
                            Program.action = "buyItems";
                            Program.gameState = Program.gameStates.action;
                        }
                        else if (nextToNPC("Seller"))
                        {
                            Program.action = "sellItems";
                            Program.gameState = Program.gameStates.action;
                        }
                    }
                    else if (nextToMonster())
                    {
                        Program.action = "fight";
                        Program.gameState = Program.gameStates.action;
                    }
                    else
                    {
                        if (nextToAnyNPC())
                        {
                            Program.action = "interact";
                            Program.gameState = Program.gameStates.action;
                        }
                        else
                        {
                            Program.message += "Nothing to do here..\n";
                        }
                    }
                    break;
                
            }

        }
        
        public static void showInventory()
        {
            itemList = "";
            foreach (var item in Inventory)
            {
                itemList += "Name: " + item.name + "(" + item.quantity + ") - Durability: " + item.currentDurability + "/" + item.durability + "\n";
            }
            Program.message = itemList;
        }

        public static bool hasItem(string itemName)
        {
            foreach (var item in Inventory)
            {
                if (item.name.Equals(itemName))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool itemDurability(string itemName)
        {
            foreach (var item in Inventory)
            {
                if (item.name.Equals(itemName))
                {
                    if(item.currentDurability >= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public static bool nextToNPC(string NPC)
        {
            
            foreach (var npc in World.NPCList)
            {
                if (npc.name == NPC)
                {
                    if (npc.xCoord - 1 == playerCoords[0] && npc.yCoord == playerCoords[1] || npc.xCoord + 1 == playerCoords[0] && npc.yCoord == playerCoords[1] || npc.yCoord - 1 == playerCoords[1] && npc.xCoord == playerCoords[0] || npc.yCoord + 1 == playerCoords[1] && npc.xCoord == playerCoords[0])
                    {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }

        public static bool nextToAnyNPC()
        {
            foreach (var npc in World.NPCList)
            {
                if (npc.room == World.currentRoom)
                {
                    if (npc.xCoord - 1 == playerCoords[0] && npc.yCoord == playerCoords[1] || npc.xCoord + 1 == playerCoords[0] && npc.yCoord == playerCoords[1] || npc.yCoord - 1 == playerCoords[1] && npc.xCoord == playerCoords[0] || npc.yCoord + 1 == playerCoords[1] && npc.xCoord == playerCoords[0])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool nextToMonster()
        {
            foreach (var monster in World.MonsterList)
            {
                if (monster.room == World.currentRoom)
                {
                    if (monster.xCoord - 1 == playerCoords[0] && monster.yCoord == playerCoords[1] || monster.xCoord + 1 == playerCoords[0] && monster.yCoord == playerCoords[1] || monster.yCoord - 1 == playerCoords[1] && monster.xCoord == playerCoords[0] || monster.yCoord + 1 == playerCoords[1] && monster.xCoord == playerCoords[0])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static int monsterNextToPlayer()
        {
            for (int i = 0; i <= World.MonsterList.Count; i++)
            {
                if (World.MonsterList[i].room == World.currentRoom)
                {
                    if (World.MonsterList[i].xCoord - 1 == playerCoords[0] && World.MonsterList[i].yCoord == playerCoords[1] || World.MonsterList[i].xCoord + 1 == playerCoords[0] && World.MonsterList[i].yCoord == playerCoords[1] || World.MonsterList[i].yCoord - 1 == playerCoords[1] && World.MonsterList[i].xCoord == playerCoords[0] || World.MonsterList[i].yCoord + 1 == playerCoords[1] && World.MonsterList[i].xCoord == playerCoords[0])
                    {
                        return i;
                        break;
                    }
                }
            }
            return 99;
        }

    }
}
