using System;
using System.Collections.Generic;

namespace _18ghostsExam
{  
    public class Tiles : MapElement
    {
        public List <Tiles> RedTileList;

        public List<Tiles> BlueTileList;

        public List<Tiles> YellowTileList;
        public Tiles()
        {
            RedTileList = new List<Tiles>();
            BlueTileList = new List<Tiles>();
            YellowTileList = new List<Tiles>();
        }

        public void SetYellowTiles()
        {
            YellowTileList.Add(new YellowTiles(new Positions(0, 0)));
        }

    }    
}
