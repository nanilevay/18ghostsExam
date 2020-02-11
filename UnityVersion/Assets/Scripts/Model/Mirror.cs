using System;
using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;

/// <summary>
/// This class allows us to set the mirror tiles
/// </summary>
public class Mirror : Tiles
{
    /// <summary>
    /// Character to represent the tile
    /// </summary>
    public char Character
    {
        get
        {
            return (char)Characters.mirror;
        }

        set
        {

        }
    }

    /// <summary>
    /// Type of tile
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Colour of the tile
    /// </summary>
    public Colours colour { get; set; }

    /// <summary>
    /// Position of the tile on the map
    /// </summary>
    public Positions Pos { get; set; }

    /// <summary>
    /// checking if the tile is empty or occupied
    /// </summary>
    public bool empty { get; set; }

    /// <summary>
    /// Gets the ghost contained in the tile if any
    /// </summary>
    public IGhostBase PieceOnTile { get; set; }

    /// <summary>
    /// To set the colours in the unity version of the game
    /// </summary>

}