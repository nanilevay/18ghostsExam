using _18ghostsExam;

/// <summary>
/// This class allows us to define a dungeon slot and determine whether it's
/// Empty or not
/// </summary>
public class DungeonSlot : IDungeonSlot
{
    /// <summary>
    /// Dungeon slot is empty by default
    /// </summary>
    public bool empty = true;

    /// <summary>
    /// Base map character
    /// </summary>
    public char Character = (char)Characters.map;

    /// <summary>
    /// To check what ghost is on the slot
    /// </summary>
    public IGhostBase GhostInSlot { get; set; }
}
