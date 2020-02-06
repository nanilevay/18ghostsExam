using System;

namespace _18ghostsExam
{
    /// <summary>
    /// this class allows us to set our map for the game with the initial
    /// mirrors and portals positions
    /// </summary>
    public class Map
    {    
        /// <summary>
        /// getting the maximum x for the map dimension
        /// </summary>
        public int MaxX = 5;

        /// <summary>
        /// getting the maximum y for the map dimension
        /// </summary>
        public int MaxY = 5;

        /// <summary>
        /// setting all the positions of the map
        /// </summary>
       // public MapElement[,] positions;

        /// <summary>
        /// setting the upper right mirror position
        /// </summary>
        private Positions UpperRightMirror = new Positions(1,1);

        /// <summary>
        /// setting the upper left mirror position
        /// </summary>
        private Positions UpperLeftMirror = new Positions(1, 3);

        /// <summary>
        /// setting the lower left mirror position
        /// </summary>
        private Positions LowerLeftMirror = new Positions(3,1);

        /// <summary>
        /// setting the lower right mirror position
        /// </summary>
        private Positions LowerRightMirror = new Positions(3,3);

        public Tiles tiles;

        /// <summary>
        /// this constructor let's us set the initial map with everything
        /// in it's right position and with it's right representation
        /// </summary>
        public Map()
        {
            MaxX = 5;
            MaxY = 5;

            tiles = new Tiles();

            tiles.SetYellowTiles();
            tiles.SetBlueTiles();
            tiles.SetRedTiles();

            // array to set all the mirrors
            Mirrors[] mirrors =
            {
                new Mirrors(UpperRightMirror),
                new Mirrors(UpperLeftMirror),
                new Mirrors(LowerLeftMirror),
                new Mirrors(LowerRightMirror)
            };

            // define map array with set size
            //positions = new MapElement[MaxX, MaxY];

            //// define portals 
            //Portals[] portals = {
               
            //    new YellowPortal(),
            //    new BluePortal(),
            //    new RedPortal()
            //};

            // set the map with the default chars and colours
            for(int y = 0; y< MaxY; y++)
            {
                for(int x = 0; x < MaxX; x++)
                {
                    //positions[x, y] = new DefaultElement(x,y);
                }
            }

            //// set the portals in the map
            //foreach(MapElement portal in portals)
            //{
            //    positions[portal.Pos.X, portal.Pos.Y] = portal;
            //}
            
            // set the mirrors in the map
            foreach (Mirrors mirror in mirrors)
            {
                //positions[mirror.Pos.X, mirror.Pos.Y] = mirror;
            }

            // set the mirrors in the map
            foreach (Tiles tile in tiles.YellowTileList)
            {
                //positions[tile.Pos.X, tile.Pos.Y] = tile;
            }

            // set the mirrors in the map
            foreach (Tiles tile in tiles.RedTileList)
            {
                //positions[tile.Pos.X, tile.Pos.Y] = tile;
            }

            // set the mirrors in the map
            foreach (Tiles tile in tiles.BlueTileList)
            {
                //positions[tile.Pos.X, tile.Pos.Y] = tile;
            }        
        }

        /*
        public void UpdateMap(MapElement previousElement)//, MapElement nextElement)
        {
            MapElement auxElement = previousElement;
            previousElement = nextElement;
            nextElement = auxElement;

            positions[nextElement.Pos.X, nextElement.Pos.Y] = 
                new YellowGhost(
                    new Positions(nextElement.Pos.X, nextElement.Pos.Y));
        }
        */
    }
}
