using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;
using UnityEngine;

/// <summary>
/// this class allows us to define a yellow portal with its initial rotation
/// and colour
/// </summary>
public class YellowPortals : PortalBase
{
    /// <summary>
    /// sets the portal with the initial direction facing right
    /// </summary>
    public YellowPortals()
    {
        // current starting rotation of the portal
        CurrentRot = PortalDir.right;
    }
    
    /// <summary>
    /// this method allows us to check the tiles adjacent to the portal
    /// </summary>
    public override void CheckAdjacents()
    {
        /*
        foreach (GameObject piece in AdjacentTiles)
        {
            if (TileUp.PieceOnTile?.colour 
                == Colours.yellow && CurrentRot.up )
                Debug.Log("oooh");

            if (TileLeft.PieceOnTile?.colour
                == Colours.yellow && CurrentRot.left)
                Debug.Log("oooh");

            if (TileDown.PieceOnTile?.colour
                == Colours.yellow && CurrentRot.down)
                Debug.Log("oooh");
        }    
        */
    }
}
