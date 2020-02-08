using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;
using TMPro;


public class Player : MonoBehaviour, IPlayer
{

    public bool HoldingPiece { get; set; }

    public bool HoldingBluePiece { get; set; }
    public bool HoldingRedPiece { get; set; }
    public bool HoldingYellowPiece { get; set; }

    public bool ColourRestriction { get; set; }

    public IGhostBase ChosenPiece { get; set; }

    
    public bool start { get; set; }

    /*
    void Start()
    {
        HoldingBluePiece = false;
        HoldingYellowPiece = false;
        HoldingRedPiece = false;
        HoldingPiece = false;

        ColourRestriction = true;

        ChosenPiece = null;
    }
    */

    public List<IGhostBase> Ghosts
    {
        get
        {
            return new List<IGhostBase>();
        }
    }

  
    public string Name { get; set; }
}
