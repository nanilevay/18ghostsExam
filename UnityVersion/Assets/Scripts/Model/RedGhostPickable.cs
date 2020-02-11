using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;

/// <summary>
/// This class allows us to set a pickable ghost piece of the colour red
/// </summary>
public class RedGhostPickable : Pickable
{
    /// <summary>
    /// The type of the ghost is red ghost for identification
    /// </summary>
    public override string Type
    {
        get
        {
            // Red ghost
            return "red Ghost";
        }
    }

    public RedGhostPickable()
    {
        colour = Colours.red;
    }

    /// <summary>
    /// This method allows us to fight another ghost and determine the winner
    /// based on the colour of this ghost and the other
    /// </summary>
    /// <param name="other">ghost being fought</param>
    public override IGhostBase Fight(IGhostBase other)
    {
        // Check if the ghost's colour is blue
        if (other.colour == Colours.blue)
        {
            // Send the other ghost to the dungeon

            GhostDied = other;
        }

        // Check if the other ghost's colour isn't blue
        else
        {
            // Sent this ghost to the Dungeon
            GhostDied = this;
        }

        return GhostDied;

    }
}