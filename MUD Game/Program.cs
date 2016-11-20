using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class Program
    {

        public enum gameStates { start, running, action, gameOver }
        public static gameStates gameState = gameStates.start;

        private static bool alive = true;
        private static int state = 0;
        public static string message;
        public static string actionList;
        public static string action;
        public static string actionMessage;
        public static string actionResponse;
        public static string title;

        public static List<Item> itemsInShop = new List<Item>();

        static void Main(string[] args)
        {
            while(Player.currentHP > 0)
            {
                if (Player.currentHP <= 0)
                {
                    gameState = gameStates.gameOver;
                }

                switch (gameState)
                {
                    case gameStates.start:
                        start();
                        break;

                    case gameStates.running:
                        running();
                        break;

                    case gameStates.action:
                        actionScene(action);
                        break;

                    case gameStates.gameOver:
                        gameOver();
                        break;
                }
                if (gameState != gameStates.start)
                {
                    title = "Name: " + Player.name + " HP: " + Player.currentHP + " Cash: $" + Player.cash;
                    Console.Title = title;
                }
            }
        }

        static void start()
        {
            message = "Hello! What is your name?";
            Console.WriteLine(message);
            Player.name = Console.ReadLine();
            Console.Clear();
            message = "Welcome, {0}! Your starting stats is: HP: {1}, Cash: {2}$";
            Console.WriteLine(message, Player.name, Player.currentHP, Player.cash);
            message = "Start your journey by pressing a button..";
            Console.WriteLine(message);
            message = "";
            World.createMapList();
            gameState = gameStates.running;
            Action.actionsNewRoom();
        }

        static void running()
        {
            if (state == 1)
            {
                Console.WriteLine("Player Coord: " + Player.playerCoords[0] + "," + Player.playerCoords[1]);
                //Console.WriteLine("Map Coord: " + World.mapCoord[0] + "," + World.mapCoord[1]);
                Console.WriteLine("Room: " + World.currentRoom);
                Console.WriteLine(message);
                message = "";
                actionList = "[i]  Inventory  |  [e]  Interact  |  [h]  Heal  |  ";
                Console.SetCursorPosition(0,23);
                Action.action();
                Console.WriteLine(actionList);
            }

            World.getCurrentMapLayout(World.currentRoom);
            Player.move(Console.ReadKey().KeyChar);
            World.moveMonsters(World.currentRoom);
            World.createMap();
            state = 1;

        }

        static void gameOver()
        {
            message = "Game Over!";
            Console.WriteLine(message);
            Console.ReadLine();
        }

        public static void actionScene(string action)
        {
            Console.Clear();
            if (action == "buyItems")
            {
                //tilføj error/success msg on purchase
                Console.WriteLine("Buy items here:");
                actionMessage = "";
                for (int i = 0; i < itemsInShop.Count && i <= 9; i++)
                {
                    int n = i + 1;
                    actionMessage += "[" + n + "] - " + itemsInShop[i].name + ": $" + itemsInShop[i].buyPrice + "\n";
                }

                Console.WriteLine(actionMessage);

                actions(Console.ReadKey().KeyChar, action);
            }
            else if (action == "sellItems")
            {
                Console.WriteLine("Sell your item(s):");
                actionMessage = "";
                for (int i = 0; i < Player.Inventory.Count && i <= 9; i++)
                {
                    int n = i + 1;
                    actionMessage += "[" + n + "] - " + Player.Inventory[i].name + " (" + Player.Inventory[i].quantity + "): $" + Player.Inventory[i].quantity * Player.Inventory[i].sellPrice + "\n";
                }

                Console.WriteLine(actionMessage);
                
                actions(Console.ReadKey().KeyChar, action);
            }
            else if (action == "fight")
            {

            }
            else if (action == "interact")
            {
                if (Player.nextToNPC("Pleb"))
                {
                    actionMessage = "[1] Talk\n[2] Steal Money\n[3] Attack";


                    Console.WriteLine(actionMessage);
                    Console.WriteLine(actionResponse);
                    actionResponse = "";

                    actions(Console.ReadKey().KeyChar, action);
                }
                else if(Player.nextToNPC("Blocker"))
                {
                    actionMessage = "[1] Talk\n[2] Steal Money\n[3] Give $200 to Unblock Door";

                    Console.WriteLine(actionMessage);
                    Console.WriteLine(actionResponse);
                    actionResponse = "";

                    actions(Console.ReadKey().KeyChar, action);
                }
            }

        }

        public static void actions(char key, string action)
        {
            switch (key)
            {
                case '1':
                    if(action == "buyItems")
                    {
                        int n = 0;
                        //If player have enough cash to buy, then buy the item
                        if(Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        //Sell ALL items - (need to add sell x items)
                        int cashToAdd = Player.Inventory[0].sellPrice * Player.Inventory[0].quantity;
                        
                        if (Player.Inventory[0].sellItem())
                        {
                            Player.cash += cashToAdd;
                        }
                    }
                    else if (action == "interact")
                    {
                        if (Player.nextToNPC("Pleb"))
                        {
                            Program.actionResponse += "You need to find a way to earn some money..\n";
                        }
                        else if (Player.nextToNPC("Blocker"))
                        {
                            Program.actionResponse += "If you give me $200, I'll let you in!\n";
                        }
                    }
                    break;

                case '2':
                    if (action == "buyItems")
                    {
                        int n = 1;
                        //If player have enough cash to buy, then buy the item
                        if (Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        int cashToAdd = Player.Inventory[1].sellPrice * Player.Inventory[1].quantity;

                        if (Player.Inventory[1].sellItem())
                        {
                            Player.cash = Player.cash + cashToAdd;
                        }
                    }
                    else if (action == "interact")
                    {
                        //Steal money from the NPC
                        Action.stealMoney();
                    }
                    break;

                case '3':
                    if (action == "buyItems")
                    {
                        int n = 2;
                        //If player have enough cash to buy, then buy the item
                        if (Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        int cashToAdd = Player.Inventory[2].sellPrice * Player.Inventory[2].quantity;

                        if (Player.Inventory[2].sellItem())
                        {
                            Player.cash += cashToAdd;
                        }
                    }
                    else if (action == "interact")
                    {
                        if (Player.nextToNPC("Pleb"))
                        {
                            //Attack
                        }
                        else if (Player.nextToNPC("Blocker"))
                        {
                            if (Player.cash >= 200)
                            {
                                Player.cash -= 200;
                                foreach (var npc in World.NPCList)
                                {
                                    if (npc.name.Equals("Blocker"))
                                    {
                                        npc.remove();
                                        break;
                                    }
                                }

                                Console.Clear();
                                gameState = gameStates.running;
                                World.getCurrentMapLayout(World.currentRoom);
                                World.createMap();
                            }
                            else
                            {
                                Program.actionResponse += "You do not have enough money!\n";
                            }
                        }
                    }
                    break;

                case '4':
                    if (action == "buyItems")
                    {
                        int n = 3;
                        //If player have enough cash to buy, then buy the item
                        if (Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        int cashToAdd = Player.Inventory[3].sellPrice * Player.Inventory[3].quantity;

                        if (Player.Inventory[3].sellItem())
                        {
                            Player.cash += cashToAdd;
                        }
                    }
                    break;

                case '5':
                    if (action == "buyItems")
                    {
                        int n = 4;
                        //If player have enough cash to buy, then buy the item
                        if (Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        int cashToAdd = Player.Inventory[4].sellPrice * Player.Inventory[4].quantity;

                        if (Player.Inventory[4].sellItem())
                        {
                            Player.cash += cashToAdd;
                        }
                    }
                    break;

                case '6':
                    if (action == "buyItems")
                    {
                        int n = 5;
                        //If player have enough cash to buy, then buy the item
                        if (Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        int cashToAdd = Player.Inventory[5].sellPrice * Player.Inventory[5].quantity;

                        if (Player.Inventory[5].sellItem())
                        {
                            Player.cash += cashToAdd;
                        }
                    }
                    break;

                case '7':
                    if (action == "buyItems")
                    {
                        int n = 6;
                        //If player have enough cash to buy, then buy the item
                        if (Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        int cashToAdd = Player.Inventory[6].sellPrice * Player.Inventory[6].quantity;

                        if (Player.Inventory[6].sellItem())
                        {
                            Player.cash += cashToAdd;
                        }
                    }
                    break;

                case '8':
                    if (action == "buyItems")
                    {
                        int n = 7;
                        //If player have enough cash to buy, then buy the item
                        if (Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        int cashToAdd = Player.Inventory[7].sellPrice * Player.Inventory[7].quantity;

                        if (Player.Inventory[7].sellItem())
                        {
                            Player.cash += cashToAdd;
                        }
                    }
                    break;

                case '9':
                    if (action == "buyItems")
                    {
                        int n = 8;
                        //If player have enough cash to buy, then buy the item
                        if (Player.cash >= itemsInShop[n].buyPrice)
                        {
                            Player.cash -= itemsInShop[n].buyPrice;
                            if (Player.hasItem(itemsInShop[n].name) && itemsInShop[n].stackable == true)
                            {
                                foreach (var item in Player.Inventory)
                                {
                                    if (item.name.Equals(itemsInShop[n].name))
                                    {
                                        item.increaseQuantity(itemsInShop[n].quantity);
                                        break;
                                    }
                                }
                            }
                            else
                            {

                                Player.Inventory.Add(new Item(itemsInShop[n].name, itemsInShop[n].quantity, itemsInShop[n].durability, itemsInShop[n].durability, itemsInShop[n].buyPrice, itemsInShop[n].sellPrice, itemsInShop[n].stackable));
                            }
                        }
                    }
                    else if (action == "sellItems")
                    {
                        int cashToAdd = Player.Inventory[8].sellPrice * Player.Inventory[8].quantity;

                        if (Player.Inventory[8].sellItem())
                        {
                            Player.cash += cashToAdd;
                        }
                    }
                    break;

                case 'q':
                    Console.Clear();
                    gameState = gameStates.running;
                    World.getCurrentMapLayout(World.currentRoom);
                    World.createMap();
                    break;

            }
        }
        
    }
}
