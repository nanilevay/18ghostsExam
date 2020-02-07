using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;


public class Player : MonoBehaviour, IPlayer
{
    public bool HoldingPiece { get; set; }

    public bool HoldingBluePiece { get; set; }
    public bool HoldingRedPiece { get; set; }
    public bool HoldingYellowPiece { get; set; }

    public bool ColourRestriction { get; set; }

    public IGhostBase ChosenPiece { get; set; }

    void start()
    {
        HoldingBluePiece = false;
        HoldingYellowPiece = false;
        HoldingRedPiece = false;
        HoldingPiece = false;

        ColourRestriction = true;

        ChosenPiece = null;
    }

    public List<IGhostBase> Ghosts
    {
        get
        {
            return new List<IGhostBase>();
        }
    }

    public string name;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public virtual void PickPiece(IGhostBase piece)
    {
        if (piece.inDungeon) //|| piece.inStart)
            Debug.Log("In Dungeon");

        if (!HoldingPiece)
        {
            Debug.Log("picked piece");
            ChosenPiece = piece;
            if (piece.colour == Colours.yellow)
                HoldingYellowPiece = true;
            if (piece.colour == Colours.blue)
                HoldingBluePiece = true;
            if (piece.colour == Colours.red)
                HoldingRedPiece = true;
            HoldingPiece = true;
        }

        //if piece in that position occupied, make it empty
    }
}
