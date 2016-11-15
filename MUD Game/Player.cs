using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class Player
    {

        public static string name;
        
        public static int maxHP = 100;
        public static int currentHP = 100;
        public static int cash = 0;
        public static int score = 0;

        public static int[] playerCoords = new int[2] { 4, 4 };
        public static List<int> Inventory = new List<int>();

        public static void move(char direction)
        {
            World.createMap();
            if (World.walkable(playerCoords, direction) == true)
            {
                //If the direction is walkable, then first remove the player from the old place
                //and then change the coordinates of the player and last place the player again.
                switch (direction)
                {
                    case 'w':
                        World.removePlayer(playerCoords);
                        playerCoords[1]--;
                        World.placePlayer(playerCoords);
                        break;

                    case 's':
                        World.removePlayer(playerCoords);
                        playerCoords[1]++;
                        World.placePlayer(playerCoords);
                        break;

                    case 'a':
                        World.removePlayer(playerCoords);
                        playerCoords[0]--;
                        World.placePlayer(playerCoords);
                        break;

                    case 'd':
                        World.removePlayer(playerCoords);
                        playerCoords[0]++;
                        World.placePlayer(playerCoords);
                        break;
                        
                }
            }
            else
            {
                //Unable to walk that direction
            }
        }

    }
}
