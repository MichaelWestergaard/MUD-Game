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
        public string room { get; set; }
        public int xCoord { get; set; }
        public int yCoord { get; set; }

        public NPC(string name, string room, int xCoord, int yCoord)
        {
            this.name = name;
            this.room = room;
            this.xCoord = xCoord;
            this.yCoord = yCoord;
        }

    }
}
