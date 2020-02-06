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
        Player.ChosenPiece = this;
        Player.HoldingYellowPiece = true;
    }

    public override void Fight(IGhostBase other)
    {
        if (other.colour == Colours.red)
            other.SendToDungeon(other);

        else
            SendToDungeon(this);

    }

    public virtual void DropPiece()
    {

    }
}
