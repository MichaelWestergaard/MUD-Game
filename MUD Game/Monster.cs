using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class Monster
    {
        public Random rand = new Random();
        public string name { get; set; }
        public string room { get; set; }
        public int health { get; set; }
        public int maxDamage { get; set; }
        public int xCoord { get; set; }
        public int yCoord { get; set; }

        public Monster(string name, string room, int health, int maxDamage, int xCoord, int yCoord)
        {
            this.name = name;
            this.room = room;
            this.health = health;
            this.maxDamage = maxDamage;
            this.xCoord = xCoord;
            this.yCoord = yCoord;
        }
        
        public void remove()
        {
            World.MonsterList.Remove(this);
        }

        
    }
}
