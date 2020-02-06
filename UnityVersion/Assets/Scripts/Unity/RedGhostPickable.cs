using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class RedGhostPickable : Pickable
{
    public Player Player;

    public override string Type
    {
        get
        {
            return "red Ghost";
        }
    }

    public override void Fight(IGhostBase other)
    {
       // bluePortal.GetComponent<BluePortals>().Direction.text = "" + (char)PortalDir.up;
        if (other.colour == Colours.blue)
            other.SendToDungeon(other);

        else
            SendToDungeon(this);

    }
}
