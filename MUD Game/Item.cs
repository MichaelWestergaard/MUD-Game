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
        public int buyPrice { get; set; }
        public int sellPrice { get; set; }
        public bool stackable { get; set; }

        public Item(string name, int quantity, int currentDurability, int durability, int buyPrice, int sellPrice, bool stackable)
        {
            this.name = name;
            this.quantity = quantity;
            this.durability = durability;
            this.currentDurability = currentDurability;
            this.buyPrice = buyPrice;
            this.sellPrice = sellPrice;
            this.stackable = stackable;
        }

        public void reduceDurability()
        {
            currentDurability = currentDurability-1;
            if(currentDurability <= 0)
            {
                Program.message += "Your item was destroyed.\n";
                removeItem();
            }
        }

        public void increaseQuantity(int amount)
        {
            quantity += amount;
        }

        public void decreaseQuantity(int amount)
        {
            quantity -= amount;
        }

        public void removeItem()
        {
            Player.Inventory.Remove(this);
        }

        public bool sellItem()
        {
            if (Player.Inventory.Remove(this))
            {
                return true;
            }
            return false;

        }

    }
}
