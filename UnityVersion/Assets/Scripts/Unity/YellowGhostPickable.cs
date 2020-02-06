using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class YellowGhostPickable : Pickable
{
    public Player Player;

    public override string Type
    {
        get
        {
            return "yellow Ghost";
        }
    }

    public virtual void PickPiece()
    {
        Debug.Log("picked yellow piece");
        Player.ChosenPiece = this.gameObject;
        Player.HoldingYellowPiece = true;
    }

    public override void Fight(Pickable other)
    {
        if (other.colour == Colours.red)
            other.SendToDungeon(other);
        
    }

    public virtual void DropPiece()
    {

    }
}
