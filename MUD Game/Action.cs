using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class Action
    {

        public static Random rand = new Random();
        public static List<Tuple<string, string, int, int>> roomActionList = new List<Tuple<string, string, int, int>>();
        public static List<string> roomActions = new List<string>();
        public static string message;
        public static bool changed;

        public static void actionList()
        {
            foreach (var action in roomActionList)
            {
                //Get all actions for the current room
                if (action.Item1.Equals(World.currentRoom))
                {
                    //Add these actions to a list, that can be shown to the player
                    roomActions.Add(action.Item2);
                }
            }
        }

        public static void mine()
        {
            if (Player.hasItem("Pickaxe") && Player.itemDurability("Pickaxe"))
            {
                //Mine

                //25% for a bad outcome and 75% for a good outcome.
                int outcome = rand.Next(1,4);

                if(outcome > 1)
                {
                    //Good outcome
                    int amount = rand.Next(1,6);

                    for(int i = 0; i < Player.Inventory.Count && changed == true; i++)
                    {
                        if (Player.Inventory[i].name.Equals("Pickaxe"))
                        {
                            Player.Inventory[i].reduceDurability();
                            changed = true;
                        }
                    }

                    Program.message += "You mined " + amount + " ore and lost 1 durability on your pickaxe!\n";

                    //Add the mined ores to the inventory of the player
                    //First check if the player already have some ores
                    if (Player.hasItem("Ore"))
                    {
                        //If he have the item, then add the mined ores to the rest of the ores in the inventory
                        foreach (var item in Player.Inventory)
                        {
                            if (item.name.Equals("Ore"))
                            {
                                item.increaseQuantity(amount);
                            }
                        }
                    }
                    else
                    {
                        //Else add an new item to the inventory
                        Player.Inventory.Add(new Item("Ore", amount, 0, 0));
                    }

                } else
                {
                    //Bad outcome / loses ores
                    Program.message = "You lost some ores..";

                }

            } else
            {
                Program.message = "You need a pickaxe in order to mine here!";
            }
        }

        public static void sellItems()
        {

        }

        public static void buyItems()
        {

        }

    }
}
