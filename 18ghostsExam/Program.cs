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
                    Console.Write(map.positions[x,y].Character);
                }
                Console.WriteLine();
            }
        }
    }
}
