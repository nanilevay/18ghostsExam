using System;

namespace _18ghostsExam
{
    /// <summary>
    /// this class allows us to set our map for the game with the initial
    /// mirrors and portals positions
    /// </summary>
    public class Map
    {
        public Mirrors[] mirrors;

        public int MaxX = 25;

        public int MaxY = 25;

        public MapElement[,] positions;

        public Map()
        {
            MapElement[] portals = {
               
                new YellowPortal(),
                new BluePortal(),
                new RedPortal()
            };

            for(int y = 0; y< MaxY; y++)
            {
                for(int x = 0; x < MaxX; x++)
                {
                    positions[x, y] = new DefaultElement(x,y);
                }
            }

            foreach(MapElement portal in portals)
            {
                positions[portal.Pos.X, portal.Pos.Y] = portal;
            }

            foreach (Mirrors mirror in mirrors)
            {
                positions[mirror.Pos.X, mirror.Pos.Y] = mirror;
            }
        }
    }
}
