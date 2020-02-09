using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;

/// <summary>
/// This class lets us create a yellow ghost piece that can be manipulated
/// and placed on different tiles
/// </summary>
public class YellowGhostPickable : Pickable
{
    /// <summary>
    /// To keep track of the type of piece
    /// </summary>
    public override string Type
    {
        get
        {
            // The piece is a yellow ghost
            return "yellow Ghost";
        }
    }

    public YellowGhostPickable()
    {
        colour = Colours.yellow;
    }

    /// <summary>
    /// This method allows us to fight another ghost by checking its colour
    /// and determining the winner depending on the current ghost's colour
    /// </summary>
    /// <param name="other">other ghost being fought</param>
    public override IGhostBase Fight(IGhostBase other)
    {
        // check colour of the other ghost
        if (other.colour == Colours.red)
        {
            // if the ghost is red send it to the dungeon
            GhostDied = other;
        }

        // if other ghost isn't red
        else
        {
            // send this ghost to the dungeon
            GhostDied = this;
        }

        return GhostDied;
    }
}
