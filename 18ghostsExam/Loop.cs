using System;

namespace _18ghostsExam
{
    /// <summary>
    /// this class serves as the main gameloop for our console based game
    /// </summary>
    public class Loop
    {
        /// <summary>
        /// create new map for game
        /// </summary>
        public Map map;

        /// <summary>
        /// get player
        /// </summary>
        public Player player;

        public Loop()
        {
            
        }

        /// <summary>
        /// check if a move is valid by getting the piece trying to be placed
        /// and checking the current map piece
        /// </summary>
        /// <param name="elementInPosition">the piece being checked</param>
        /// <param name="newElement">new piece placed</param>
        /// <returns></returns>
        public bool ValidMove(MapElement elementInPosition, 
            MapElement newElement)
        {
            // checks if the piece matches the colour and isn't 
            // another ghost occupying the house
            if (elementInPosition.Colour == newElement.Colour  &&
                !(elementInPosition is GhostBase))
                return true;

            return false;
        }

        /// <summary>
        /// the main gameloop for our game
        /// </summary>
        public void GameLoop()
        {
            // initiates the map
            map = new Map();
            
            // initiates the player
            player = new Player();       

            // print the map
            for (int y = 0; y < map.MaxY; y++)
            {
                for (int x = 0; x < map.MaxX; x++)
                {
                    if (map.positions[x, y] is BluePortal)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    if (map.positions[x, y] is RedPortal)
                        Console.ForegroundColor = ConsoleColor.Red;
                    if (map.positions[x, y] is YellowPortal ||
                        map.positions[x, y] is YellowTiles)
                        Console.ForegroundColor = ConsoleColor.Yellow;

                    Console.Write((char)map.positions[x, y].Character);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }

            // debug
            Console.WriteLine(player.Ghosts[0].Colour);

            // asks player where to put their ghost
            Console.WriteLine("input coordinates you wish to put ghost");
            
            // gets player input
            string input = Console.ReadLine();

            // splits input coordinates
            string[] coordinates = input.Split('*');

            // sets player's piece if valid
            if(ValidMove(map.positions[int.Parse(coordinates[0]), 
                int.Parse(coordinates[1])],
                new YellowGhost(new Positions
                (int.Parse(coordinates[0]), int.Parse(coordinates[1])))))
                map.UpdateMap(
                    map.positions[int.Parse(coordinates[0]), 
                    int.Parse(coordinates[1])], 
                    new YellowGhost(
                        new Positions(int.Parse(coordinates[0]), 
                        int.Parse(coordinates[1]))));


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
