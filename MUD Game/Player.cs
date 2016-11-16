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
                    Inventory.Add(new Item("Pickaxe", 1, 10, 10));
                    break;

                case 'e':
                    Action.mine();
                    break;

                case 'i':
                    showInventory();
                    break;
                /*
                case '1':
                    //invokeStringMethod("Action", "mine");
                    break;
                case '2':
                    //2nd action from list
                    break;
                case '3':
                    //3rd action from list
                    break;
                case '4':
                    //4th action from list
                    break;
                    */

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
                    if(item.currentDurability >= item.durability)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /*
        public static void invokeStringMethod(string typeName, string methodName)
        {
            MethodInfo method = Type.GetType(typeName).GetMethod(methodName);

            Action c = new Action();
            string result = (string)method.Invoke(c, null);
            Console.WriteLine(result);
        }
        */

    }
}
