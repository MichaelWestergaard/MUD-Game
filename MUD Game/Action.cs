using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MUD_Game
{
    class Action
    {

        public static Random rand = new Random();
        public static List<Tuple<string, string, int, int>> roomActionList = new List<Tuple<string, string, int, int>>();
        public static List<string> roomActions = new List<string>();
        public static string actionList;

        public static void action()
        {
            actionList = "";
            
            for (int i = 0; i < roomActions.Count; i++)
            {
                actionList += "[space]  " + roomActions[i] + "  |  ";
            }
            Program.actionList += actionList;
        }

        public static void actionsNewRoom()
        {
            //Remove all actions from old room
            roomActions.Clear();

            foreach (var action in roomActionList)
            {
                //Get all actions for the current room
                if (action.Item1.Equals(World.currentRoom))
                {
                    //Add actions to a list, that will be shown to the player
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
                    int lostOre = rand.Next(1, 5);

                    if (Player.hasItem("Ore"))
                    {
                        //If the player have ores in the inventory, then take some from him
                        foreach (var item in Player.Inventory)
                        {
                            if (item.name.Equals("Ore"))
                            {
                                if(item.quantity <= lostOre)
                                {
                                    //Remove ore from inventory, because all the ores was taken
                                    item.removeItem();
                                }
                                else
                                {
                                    item.decreaseQuantity(lostOre);
                                }
                                break;
                            }
                        }
                        Program.message += "You was robbed while you mined! You lost " + lostOre + " ores!\n";
                    }
                    else
                    {
                        Program.message += "You did not find any ores this time..\n";
                    }

                }
                
                for (int i = 0; i < Player.Inventory.Count; i++)
                {
                    if (Player.Inventory[i].name.Equals("Pickaxe"))
                    {
                        Player.Inventory[i].reduceDurability();
                        break;
                    }
                }

            } else
            {
                Program.message = "You need a pickaxe in order to mine here!\n";
            }
        }

        public static void sellItems()
        {

        }

        public static void buyItems()
        {
            //Åben ny gamestate i program
            //Vis ting der kan købes med 1-9 i siden
            //Når der trykkes 1 købes ting 1
        }

        public static void interact()
        {
            Program.message += "Nothing to do here..\n";
        }

    }
}
