using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class Item
    {

        public string name { get; set; }
        public int quantity { get; set; }
        public int currentDurability { get; set; }
        public int durability { get; set; }

        public Item(string name, int quantity, int currentDurability, int durability)
        {
            this.name = name;
            this.quantity = quantity;
            this.durability = durability;
            this.currentDurability = currentDurability;
        }

        public void reduceDurability()
        {
            currentDurability = currentDurability-1;
            if(currentDurability <= 0)
            {
                Program.message += "Your item was destroyed.\n";
            }
        }

        public void increaseQuantity(int amount)
        {
            quantity += amount;
        }

    }
}
