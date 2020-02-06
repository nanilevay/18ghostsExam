﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class BlueHall : MonoBehaviour, IMapElement
{
    public IGhostBase PieceOnTile { get; set; }

    public string Type { get; set; }

    public char Character { get; }

    public Colours colours;

    public Colours colour
    {
        get
        {
            return colours;
        }
    }

    public Sprite Img { get; }

    public Positions Pos { get; set; }

    public bool empty
    {
        get
        {
            return true;
        }

        set
        {

        }
    }

    
}
