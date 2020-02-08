using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;
using UnityEngine;

public class YellowPortals : PortalBase
{
    public YellowPortals()
    {
        CurrentRot = PortalDir.right;
    }

    // change this to IMapElement when new classes done
    public IMapElement TileDown;

    public IMapElement TileUp;

    public IMapElement TileLeft;

    
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
