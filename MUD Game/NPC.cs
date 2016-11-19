using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class NPC
    {

        public string name { get; set; }
        public string icon { get; set; }
        public string room { get; set; }
        public int xCoord { get; set; }
        public int yCoord { get; set; }

        public NPC(string name, string icon, string room, int xCoord, int yCoord)
        {
            this.name = name;
            this.icon = icon;
            this.room = room;
            this.xCoord = xCoord;
            this.yCoord = yCoord;
        }

        public void remove()
        {
            World.NPCList.Remove(this);
        }

    }
}
