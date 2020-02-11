using _18ghostsExam;

/// <summary>
/// This class allows us to set a blue ghost pickable piece in order to change
/// its position along the board during the gameloop
/// </summary>
public class BlueGhostPickable : Pickable
{
    public BlueGhostPickable()
    {
        colour = Colours.blue;
    }

    /// <summary>
    /// For identifying the type of piece
    /// </summary>
    public override string Type
    {
        get
        {
            // The piece is a blue ghost
            return "blue Ghost";
        }
    }

    /// <summary>
    /// This method allows us to fight and recognise another ghost in order to
    /// determine the winner based on their colour and this ghost's colour
    /// </summary>
    /// <param name="other">ghost to be fought</param>
    public override IGhostBase Fight(IGhostBase other)
    {
        // Check if other ghost is yellow
        if (other is YellowGhostPickable)
        {
            GhostDied = other;
        }

        // Check if other ghost is not yellow
        else
        {
            // Send this ghost to dungeon
            GhostDied = this;
        }

        // send the ghost that lost the fight
        return GhostDied;
    }
}
