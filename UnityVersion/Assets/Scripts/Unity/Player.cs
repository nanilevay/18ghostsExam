using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;
using TMPro;

/// <summary>
/// This class allows us to set our players for the game, by giving them their
/// ghost pieces and checking what action is being done in each state of the
/// game in the main loop
/// </summary>
public class Player : MonoBehaviour, IPlayer
{
    /// <summary>
    /// Check if character is holding a piece
    /// </summary>
    public bool HoldingPiece { get; set; }

    ///////////////////////////////////////////////////// CHECK IF NEEDED
    public bool HoldingBluePiece { get; set; }
    public bool HoldingRedPiece { get; set; }
    public bool HoldingYellowPiece { get; set; }
    ///////////////////////////////////////////////////


    /// <summary>
    /// The current piece chosen by the player in a round
    /// </summary>
    public IGhostBase ChosenPiece { get; set; }

    /// <summary>
    /// To check if the game has just started to set the pieces in the needed
    /// order
    /// </summary>
    public bool start { get; set; }

    /// <summary>
    /// The list of ghosts belonging to the player
    /// </summary>
    public List<IGhostBase> Ghosts { get; set; }

    /// <summary>
    /// The player's name to be displayed in the game
    /// </summary>
    public string Name { get; set; }
}
