using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using _18ghostsExam;

    public interface IMapElement
    {
        bool empty { get; }
        string Type { get; }
        char Character { get; }
        Positions Pos { get; }
        Colours colour { get; }
        Sprite Img { get; }

        void Place(Player player);
        void PickPiece();
        void DropPiece();

    }



