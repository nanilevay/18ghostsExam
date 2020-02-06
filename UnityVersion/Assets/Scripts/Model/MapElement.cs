using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using _18ghostsExam;

    public interface IMapElement
    {
        bool empty { get; set; }
        string Type { get; }
        char Character { get; }
        Positions Pos { get; }
        Colours colour { get; }
        Sprite Img { get; }
        IGhostBase PieceOnTile { get; set; }

    }



