﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUD_Game
{
    class Program
    {

        public enum gameStates { start, running, fight, gameOver }
        public static gameStates gameState = gameStates.start;

        private static bool alive = true;
        private static int state = 0;
        public static string message = "";

        static void Main(string[] args)
        {
            while(alive)
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

                    case gameStates.fight:
                        fight();
                        break;

                    case gameStates.gameOver:
                        gameOver();
                        break;
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
            Console.ReadLine();
            message = "";
            World.createMapList();
            gameState = gameStates.running;
        }

        static void running()
        {
            if (state == 1)
            {
                //Console.WriteLine("Player Coord: " + Player.playerCoords[0] + "," + Player.playerCoords[1]);
                //Console.WriteLine("Map Coord: " + World.mapCoord[0] + "," + World.mapCoord[1]);
                Console.WriteLine("Room: " + World.currentRoom);
                Console.WriteLine(message);
                message = "";

            }

            World.getCurrentMapLayout(World.currentRoom);
            Player.move(Console.ReadKey().KeyChar);
            World.createMap();
            state = 1;

        }

        static void fight()
        {
            //Fight scene
        }

        static void gameOver()
        {
            message = "Game Over!";
            Console.WriteLine(message);
            alive = false;
        }

    }
}
