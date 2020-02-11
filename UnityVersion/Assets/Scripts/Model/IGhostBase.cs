using System;
namespace _18ghostsExam
{
    /// <summary>
    /// This interface allows us to set the base for our ghosts and their 
    /// state in the game
    /// </summary>
    public interface IGhostBase
    {
        /// <summary>
        /// Checks if a ghost is inside the dungeon
        /// </summary>
        bool inDungeon { get; set; }

        /// <summary>
        /// Tells type of ghost
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Char to represent ghost in the board
        /// </summary>
        char character
        { get; set; }

        /// <summary>
        /// Position of ghost in the board
        /// </summary>
        Positions Pos { get; set; }

        /// <summary>
        /// Ghost's colour
        /// </summary>
        Colours colour { get; set; }

        /// <summary>
        /// Checking if ghost is on mirror
        /// </summary>
        bool OnMirror { get; set; }

        /// <summary>
        /// Fighting another ghost and determining winner to send to dungeon
        /// </summary>
        /// <param name="other">other ghost in tile</param>
        IGhostBase Fight(IGhostBase other);

        /// <summary>
        /// To determine which ghost lost the fight to rotate the portal
        /// </summary>
        /// /// <param name="deadGhost">ghost that lost fight</param>
        IGhostBase GhostDied { get; set; }
    }
}