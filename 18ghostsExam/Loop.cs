using System;

namespace _18ghostsExam
{
    public class Loop
    {
        public Map map;

        public Player player;

        public Loop()
        {
            
        }

        public void GameLoop()
        {
            map = new Map();

            player = new Player();       

            for (int y = 0; y < map.MaxY; y++)
            {
                for (int x = 0; x < map.MaxX; x++)
                {
                    if (map.positions[x, y] is BluePortal)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (map.positions[x, y] is RedPortal)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (map.positions[x, y] is YellowPortal)
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write((char)map.positions[x, y].Character);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            // debug
            Console.WriteLine(player.Ghosts[0].Colour);

            Console.WriteLine("input coordinates you wish to put ghost");
            
            string input = Console.ReadLine();

            string[] coordinates = input.Split('*');

            map.UpdateMap(map.positions[int.Parse(coordinates[0]), int.Parse(coordinates[1])], new YellowGhost(new Positions(int.Parse(coordinates[0]), int.Parse(coordinates[1]))));


            for (int y = 0; y < map.MaxY; y++)
            {
                for (int x = 0; x < map.MaxX; x++)
                {
                    if (map.positions[x, y] is BluePortal)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (map.positions[x, y] is RedPortal)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (map.positions[x, y] is YellowPortal ||
                        map.positions[x, y] is YellowGhost)
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write((char)map.positions[x, y].Character);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
