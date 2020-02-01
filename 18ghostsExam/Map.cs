using System;

namespace _18ghostsExam
{
    /// <summary>
    /// this class allows us to set our map for the game with the initial
    /// mirrors and portals positions
    /// </summary>
    public class Map
    {
      

        public int MaxX = 5;

        public int MaxY = 5;

        public MapElement[,] positions;

        private Positions UpperRightMirror = new Positions(1,1);

        private Positions UpperLeftMirror = new Positions(1, 3);

        private Positions LowerLeftMirror = new Positions(3,1);

        private Positions LowerRightMirror = new Positions(3,3);

        public Map()
        {
            Mirrors[] mirrors =
            {
                new Mirrors(UpperRightMirror),
                new Mirrors(UpperLeftMirror),
                new Mirrors(LowerLeftMirror),
                new Mirrors(LowerRightMirror)
            };

            positions = new MapElement[MaxX, MaxY];

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
