using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

/// <summary>
/// This class allows us to define a dungeon slot and determine whether it's
/// Empty or not
/// </summary>
namespace _18ghostsExam
{
    /// <summary>
    /// Set the dungeonslot to inherit monobehaviour and be instantiated
    /// </summary>
    public class DungeonSlot : MonoBehaviour, IDungeonSlot
    {
        /// <summary>
        /// Check if slot is empty
        /// </summary>
        public bool empty = true;

        /// <summary>
        /// Check slot's character
        /// </summary>
        public char Character { get; set; }

        /// <summary>
        /// Check what ghost is in the slot if not empty
        /// </summary>
        public IGhostBase GhostInSlot { get; set; }
    }
}