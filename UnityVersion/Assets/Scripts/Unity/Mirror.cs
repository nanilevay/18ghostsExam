using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

/// <summary>
/// This class allows us to set the mirror tiles
/// </summary>
public class Mirror : Tiles
{
    /// <summary>
    /// This constructor allows us to set the mirror's default colour
    /// </summary>
    public Mirror()
    {
        // Mirror colour defaults to white to display in the game
        colour = Colours.white;
    }
}
