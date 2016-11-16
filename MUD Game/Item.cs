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
        public int currentDurability { get; set; }
        public int durability { get; set; }

        public Item(string name, int currentDurability, int durability)
        {
            this.name = name;
            this.durability = durability;
            this.currentDurability = currentDurability;
        }

    }
}
