using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class Mirror : MonoBehaviour, IMapElement
{

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

    public Sprite Img { get; }

    public void PickPiece()
    { }

    public void DropPiece()
    { }

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
        if (player.HoldingBluePiece)
        {
            (player.ChosenPiece as MonoBehaviour).gameObject.transform.position = this.transform.position;
            player.HoldingBluePiece = false;
        }
    }
}
