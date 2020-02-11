using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _18ghostsExam
{
    /// <summary>
    /// This interface allows us to set a dungeonslot for our game
    /// </summary>
    public interface IDungeonSlot
    {
        /// <summary>
        /// Check if slot is empty
        /// </summary>
        bool empty { get; set; }

        /// <summary>
        /// Check slot's character
        /// </summary>
        char Character { get; set; }

        /// <summary>
        /// Check what ghost is in the slot if not empty
        /// </summary>
        IGhostBase GhostInSlot { get; set; }
    }
}