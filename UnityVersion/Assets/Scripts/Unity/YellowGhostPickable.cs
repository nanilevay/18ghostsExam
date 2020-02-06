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

    public override void Fight(IGhostBase other)
    {
        if (other.colour == Colours.red)
        {
            other.SendToDungeon(other);
            other.inDungeon = true;
            GhostDied = other;
        }

        else
        {
            SendToDungeon(this);
            inDungeon = true;
            GhostDied = this;
        }

    }
}
