using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using _18ghostsExam;


    public interface IMapElement
    {
        string Type { get; }
        char Character { get; }
        Positions Pos { get; }
        Colours colour { get; }
        Sprite Img { get; }

        void Place();
        void PickPiece();
        void DropPiece();
    }

    public class MapEventArgs : EventArgs
    {
        public MapEventArgs(IMapElement piece)
        {
            Piece = piece;
        }

        public IMapElement Piece;
    }

    public class MapElement
    {
        public Positions Pos { get; set; }
        public Colours Colour { get; set; }
        public Characters Character { get; set; }

        public MapElement()
        {
            
        }

    }



