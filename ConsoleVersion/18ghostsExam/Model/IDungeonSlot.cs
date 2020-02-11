using System.Collections;
using System.Collections.Generic;

namespace _18ghostsExam
{
    /// <summary>
    /// This interface allows us to set a dungeonslot for our game
    /// </summary>
    public interface IDungeonSlot
    {
        /// <summary>
        /// Check what ghost is in the slot if not empty
        /// </summary>
        IGhostBase GhostInSlot { get; set; }
    }
}