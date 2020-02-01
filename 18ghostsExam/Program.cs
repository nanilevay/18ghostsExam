using System;

namespace _18ghostsExam
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map();

            for (int y = 0; y < map.MaxY; y++)
            {
                for (int x = 0; x < map.MaxX; x++)
                {
                    if (map.positions[x,y] is BluePortal)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (map.positions[x, y] is RedPortal)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (map.positions[x, y] is YellowPortal)
                        Console.ForegroundColor = ConsoleColor.Yellow;
           
                    Console.Write((char)map.positions[x,y].Character);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
