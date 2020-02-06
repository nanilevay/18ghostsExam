using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class YellowHall : MonoBehaviour, IMapElement
{
    public IGhostBase pieceOnTile;

    public IGhostBase PieceOnTile
    {
        get
        {
            return pieceOnTile;
        }

        set
        {

        }
    }

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

    public void PickPiece()
    { }

    public void DropPiece()
    {}

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

    public void Place(Player player)
    {
        Debug.Log("ouch");
        if (player.HoldingYellowPiece)
            (player.ChosenPiece as MonoBehaviour).transform.position = this.transform.position;
    }
}
