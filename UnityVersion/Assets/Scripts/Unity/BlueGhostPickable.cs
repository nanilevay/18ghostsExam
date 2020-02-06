using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class BlueGhostPickable : Pickable
{
    public Player Player;

    public override string Type
    {
        get
        {
            return "blue Ghost";
        }
    }

    public override void Fight(IGhostBase other)
    {
        if (other.colour == Colours.yellow)
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
