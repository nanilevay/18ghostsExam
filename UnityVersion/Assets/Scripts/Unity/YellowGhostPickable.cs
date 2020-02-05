using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class YellowGhostPickable : Pickable
{
    public override string Type
    {
        get
        {
            return "yellow Ghost";
        }
    }

    public virtual void PickPiece()
    {
        //gameObject.SetActive(false);
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
