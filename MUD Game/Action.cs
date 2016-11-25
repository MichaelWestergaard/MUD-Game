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
                int outcome = rand.Next(1,5);

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
                        //Else add a new item to the inventory
                        Player.Inventory.Add(new Item("Ore", amount, 0, 0, 0, 1, true));
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
                                    break;
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
        
        public static void dig()
        {
            if (Player.hasItem("Shovel") && Player.itemDurability("Shovel"))
            {
                //25% for a bad outcome and 75% for a good outcome.
                int outcome = rand.Next(1, 5);

                if (outcome > 1)
                {
                    //Good outcome
                    int amount = rand.Next(1, 6);

                    Program.message += "You digged out " + amount + " sand and lost 1 durability on your shovel!\n";

                    //Add the digged out sand to the inventory of the player
                    //First check if the player already have some sand
                    if (Player.hasItem("Sand"))
                    {
                        //If he have the item, then add the sand to the rest of the sand in the inventory
                        foreach (var item in Player.Inventory)
                        {
                            if (item.name.Equals("Sand"))
                            {
                                item.increaseQuantity(amount);
                            }
                        }
                    }
                    else
                    {
                        //Else add a new item to the inventory
                        Player.Inventory.Add(new Item("Sand", amount, 0, 0, 0, 1, true));
                    }

                }
                else
                {
                    //Bad outcome / loses sand
                    int lostSand = rand.Next(1, 5);

                    if (Player.hasItem("Sand"))
                    {
                        //If the player have sand in the inventory, then take some from him
                        foreach (var item in Player.Inventory)
                        {
                            if (item.name.Equals("Sand"))
                            {
                                if (item.quantity <= lostSand)
                                {
                                    //Remove sand from inventory, because all the sand was taken
                                    item.removeItem();
                                }
                                else
                                {
                                    item.decreaseQuantity(lostSand);
                                    break;
                                }
                                break;
                            }
                        }
                        Program.message += "You was robbed while you was digging! You lost " + lostSand + " sand!\n";
                    }
                    else
                    {
                        Program.message += "You did not find any sand this time..\n";
                    }

                }

                for (int i = 0; i < Player.Inventory.Count; i++)
                {
                    if (Player.Inventory[i].name.Equals("Shovel"))
                    {
                        Player.Inventory[i].reduceDurability();
                        break;
                    }
                }

            }
            else
            {
                Program.message = "You need a shovel in order to dig here!\n";
            }
        }

        public static void cut()
        {
            if (Player.hasItem("Axe") && Player.itemDurability("Axe"))
            {
                //25% for a bad outcome and 75% for a good outcome.
                int outcome = rand.Next(1, 5);

                if (outcome > 1)
                {
                    //Good outcome
                    int amount = rand.Next(1, 6);

                    Program.message += "You cut " + amount + " wood and lost 1 durability on your axe!\n";

                    //Add the cutted out wood to the inventory of the player
                    //First check if the player already have some wood
                    if (Player.hasItem("Wood"))
                    {
                        //If he have the item, then add the cutted wood to the rest of the wood in the inventory
                        foreach (var item in Player.Inventory)
                        {
                            if (item.name.Equals("Wood"))
                            {
                                item.increaseQuantity(amount);
                            }
                        }
                    }
                    else
                    {
                        //Else add a new item to the inventory
                        Player.Inventory.Add(new Item("Wood", amount, 0, 0, 0, 1, true));
                    }

                }
                else
                {
                    //Bad outcome / loses wood
                    int lostWood = rand.Next(1, 5);

                    if (Player.hasItem("Wood"))
                    {
                        //If the player have wood in the inventory, then take some from him
                        foreach (var item in Player.Inventory)
                        {
                            if (item.name.Equals("Wood"))
                            {
                                if (item.quantity <= lostWood)
                                {
                                    //Remove wood from inventory, because all the wood was taken
                                    item.removeItem();
                                }
                                else
                                {
                                    item.decreaseQuantity(lostWood);
                                    break;
                                }
                                break;
                            }
                        }
                        Program.message += "You was robbed while you was cutting the tree! You lost " + lostWood + " wood!\n";
                    }
                    else
                    {
                        Program.message += "You did not get any wood this time..\n";
                    }

                }

                for (int i = 0; i < Player.Inventory.Count; i++)
                {
                    if (Player.Inventory[i].name.Equals("Axe"))
                    {
                        Player.Inventory[i].reduceDurability();
                        break;
                    }
                }

            }
            else
            {
                Program.message = "You need a axe in order to cut trees here!\n";
            }
        }
        
        public static void heal()
        {
            if (Player.hasItem("Health Potion"))
            {
                //Find the health potion in the inventory
                foreach (var item in Player.Inventory)
                {
                    if (item.name.Equals("Health Potion"))
                    {
                        if (item.quantity <= 1)
                        {
                            //Remove the item, because there is no potions left
                            item.removeItem();
                        }
                        else
                        {
                            item.decreaseQuantity(1);
                        }
                        break;
                    }
                }
                
                if(Player.currentHP + 25 > Player.maxHP)
                {
                    Player.currentHP = Player.maxHP;
                }
                else
                {
                    Player.currentHP += 25;
                }
                 
                Program.message += "You used a Health Potion and restored 25 HP!\n";
            }
            else
            {
                Program.message += "You do not have any Health Potions!\n";
            }
        }

        public static void stealMoney()
        {
            int outcome;

            if (Player.hasItem("Sword"))
            {
                outcome = rand.Next(1, 5);

                for (int i = 0; i < Player.Inventory.Count; i++)
                {
                    if (Player.Inventory[i].name.Equals("Sword"))
                    {
                        Player.Inventory[i].reduceDurability();
                        break;
                    }
                }

            } else
            {
                outcome = rand.Next(1, 3);
            }

            if(outcome == 1)
            {
                //Pickpocket failed
                int damageTaken = rand.Next(5, 10);
                Player.currentHP -= damageTaken;

                Program.actionResponse += "You failed to steal money and lost " + damageTaken + " HP";

            }
            else
            {
                int money = rand.Next(1, 5);
                Player.cash += money;

                Program.actionResponse += "You stole $" + money + "!";
            }
        }

        public static void attack()
        {
            int playerDamage;
            if (Player.hasItem("Sword"))
            {
                //More dmg
                playerDamage = rand.Next(3, 11);

                //Reduce durability from sword
            }
            else 
            {
                playerDamage = rand.Next(1, 6);
            }

            int monsterIndex = Player.monsterNextToPlayer();
            var monster = World.MonsterList[monsterIndex];
            
            int monsterDamage = rand.Next(1, monster.maxDamage);

            if(playerDamage >= monster.health)
            {
                //Monster was killed
                monster.remove();

                //Amount of gold for killing the monster
                int goldReward = rand.Next(5, 16);

                Player.cash += goldReward;
                
                Program.message += "You killed the " + monster.name + " and got $" + goldReward + " as a reward!\n";
            }
            else
            {
                monster.health -= playerDamage;

                Program.actionResponse += "You did " + playerDamage + " damage to the " + monster.name + "!\n";
            }

            Player.currentHP -= monsterDamage;

            Program.actionResponse += "You lost " + monsterDamage + " HP!\n";

        }

    }
}
