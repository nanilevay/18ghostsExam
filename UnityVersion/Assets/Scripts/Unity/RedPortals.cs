using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _18ghostsExam;

/// <summary>
/// This class lets us set a portal with the colour red as its default
/// and its rotation set to up
/// </summary>
public class RedPortals : PortalBase
{
    /// <summary>
    /// This constructor sets the direction to up
    /// </summary>
    public RedPortals()
    {
        // Initial portal rotation is facing up as stated in the game's rules
        CurrentRot = PortalDir.up;
    }
}


