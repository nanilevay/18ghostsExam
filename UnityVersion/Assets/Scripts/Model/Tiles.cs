using System;
using System.Collections.Generic;

namespace _18ghostsExam
{  
    public class Tiles //: IMapElement
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
            YellowTileList.Add(new YellowTiles(new Positions(0, 1)));
            YellowTileList.Add(new YellowTiles(new Positions(2, 1)));
            YellowTileList.Add(new YellowTiles(new Positions(4, 1)));
            YellowTileList.Add(new YellowTiles(new Positions(2, 3)));
            YellowTileList.Add(new YellowTiles(new Positions(0, 4)));
            YellowTileList.Add(new YellowTiles(new Positions(4, 4)));
        }

        public void SetBlueTiles()
        {
            BlueTileList.Add(new BlueTiles(new Positions(0, 0)));

            BlueTileList.Add(new BlueTiles(new Positions(3, 0)));

            BlueTileList.Add(new BlueTiles(new Positions(1, 2)));

            BlueTileList.Add(new BlueTiles(new Positions(3, 2)));

            BlueTileList.Add(new BlueTiles(new Positions(0, 3)));

            BlueTileList.Add(new BlueTiles(new Positions(3, 4)));

        }

        public void SetRedTiles()
        {
            RedTileList.Add(new RedTiles(new Positions(1, 0)));

            RedTileList.Add(new RedTiles(new Positions(4, 0)));

            RedTileList.Add(new RedTiles(new Positions(0, 2)));

            RedTileList.Add(new RedTiles(new Positions(2, 2)));

            RedTileList.Add(new RedTiles(new Positions(4, 3)));

            RedTileList.Add(new RedTiles(new Positions(1, 4)));

        }
    }
}
