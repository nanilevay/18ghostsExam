using System;
using System.Collections.Generic;
namespace _18ghostsExam
{
    /// <summary>
    /// This interface allows us to set the behaviours our players will have
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Ghosts in the players' possession
        /// </summary>
        List<IGhostBase> Ghosts { get; set; }

        /// <summary>
        /// Ghosts that have escaped for winning condition
        /// </summary>
        List<IGhostBase> EscapedGhosts { get; set; }

        /// <summary>
        /// Player name for display
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Check if player is holding a piece
        /// </summary>
        bool HoldingPiece { get; set; }

        /// <summary>
        /// Player's chosen piece each turn
        /// </summary>
        IGhostBase ChosenPiece { get; set; }

        /// <summary>
        /// Ghosts at the start
        /// </summary>
        List<IGhostBase> StartGhosts { get; set; }

        /// <summary>
        /// If the game (past setting ghosts) has started
        /// </summary>
        bool start { get; set; }

        /// <summary>
        /// Set the player's ghosts
        /// </summary>
        void SetGhosts();
    }
}