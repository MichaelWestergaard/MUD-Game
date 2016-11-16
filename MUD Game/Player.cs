using System;
using System.Collections.Generic;
using System.Linq;
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

        public static void move(char direction)
        {
            if (World.walkable(playerCoords, direction) == true)
            {
                //If the direction is walkable, then first remove the player from the old place
                //and then change the coordinates of the player and last place the player again.
                switch (direction)
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
                char lastDir = direction;
            }
            else
            {
                World.placePlayer(playerCoords);
            }
            
            switch (direction)
            {
                case 'e':
                    Inventory.Add(new Item("Sword", 10, 10));
                    break;

                case 'i':
                    showInventory();
                    break;

            }

        }
        
        public static void showInventory()
        {
            itemList = "";
            foreach (var item in Inventory)
            {
                itemList += "Name: " + item.name + " - Durability: " + item.currentDurability + "/" + item.durability + "\n";
            }
            Program.message = itemList;
        }
        
    }
}
