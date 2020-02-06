using System;
namespace _18ghostsExam
{
    /// <summary>
    /// this interface allows us to set the base for our ghosts and their 
    /// state in the game
    /// </summary>
    public interface IGhostBase
    {
        /// <summary>
        /// checks if a ghost is inside the dungeon
        /// </summary>
        bool inDungeon { get; set; }

        /// <summary>
        /// tells type of ghost
        /// </summary>
        string Type { get; }

        /// <summary>
        /// char to represent ghost in the board
        /// </summary>
        char character { get; }

        /// <summary>
        /// position of ghost in the board
        /// </summary>
        Positions Pos { get; set; }

        /// <summary>
        /// ghost's colour
        /// </summary>
        Colours colour { get; }

        /// <summary>
        /// fighting another ghost and determining winner to send to dungeon
        /// </summary>
        /// <param name="other">other ghost in tile</param>
        void Fight(IGhostBase other);

        /// <summary>
        /// to determine which ghost lost the fight to rotate the portal
        /// </summary>
        /// /// <param name="deadGhost">ghost that lost fight</param>
        IGhostBase GhostDied { get; set; }

        /// <summary>
        /// sending the loser ghost to the dungeon
        /// </summary>
        /// <param name="dungeonGhost">ghost to be sent to dungeon</param>
        void SendToDungeon(IGhostBase dungeonGhost);
    }
}