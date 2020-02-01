using System;

namespace _18ghostsExam
{
    /// <summary>
    /// this class allows us to set our map for the game with the initial
    /// mirrors and portals positions
    /// </summary>
    public class Map
    {
       // public Mirrors[] mirrors;

        public int MaxX = 25;

        public int MaxY = 25;

        public MapElement[,] positions;

        BluePortal bluePortal;

        YellowPortal yellowPortal;

        RedPortal RedPortal;

        public Map()
        {

            bluePortal = new BluePortal();
            yellowPortal = new YellowPortal();
            RedPortal = new RedPortal();

            positions = new MapElement[MaxX,MaxY];

            for (int y = 0; y< MaxY; y++)
            {
                for(int x = 0; x < MaxX; x++)
                {
                    positions[x, y] = new MapElement
                        (new Positions(x,y), Colours.white, Characters.map);
                }
            }

            positions[bluePortal.Pos.X, bluePortal.Pos.Y] = new MapElement
                (new Positions(bluePortal.Pos.X, bluePortal.Pos.Y),
                Colours.blue, Characters.portal);

            
        }
    }
}
