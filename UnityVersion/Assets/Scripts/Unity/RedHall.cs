using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class RedHall : MonoBehaviour, IMapElement
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

    }

    public void Place(Player player)
    {
        Debug.Log("ouch");
        if (player.HoldingRedPiece)
            player.ChosenPiece.transform.position = this.transform.position;
    }
}
