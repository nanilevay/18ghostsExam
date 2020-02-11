using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;

/// <summary>
/// This class allows us to set our players for the game, by giving them their
/// ghost pieces and checking what action is being done in each state of the
/// game in the main loop
/// </summary>
/// 
namespace _18ghostsExam
{
    public class Player : IPlayer
    {
        /// <summary>
        /// Check if character is holding a piece
        /// </summary>
        public bool HoldingPiece { get; set; }

        /// <summary>
        /// The current piece chosen by the player in a round
        /// </summary>
        public IGhostBase ChosenPiece { get; set; }

        /// <summary>
        /// To check if the game has just started to set the pieces in the needed
        /// order
        /// </summary>
        public bool start { get; set; }

        public List<IGhostBase> StartGhosts { get; set; }
        /// <summary>
        /// The list of ghosts belonging to the player
        /// </summary>
        public List<IGhostBase> Ghosts { get; set; }

        /// <summary>
        /// The list of escaped ghosts of the player
        /// </summary>
        public List<IGhostBase> EscapedGhosts { get; set; }

        /// <summary>
        /// The player's name to be displayed in the game
        /// </summary>
        public string Name { get; set; }

        public Player()
        {
            Ghosts = new List<IGhostBase>();
            StartGhosts = new List<IGhostBase>();

            for (int i = 0; i < 3; i++)
            {
                IGhostBase instantiateBlueGhost = new BlueGhostPickable();

                StartGhosts.Add(instantiateBlueGhost);

                IGhostBase instantiateRedGhost = new RedGhostPickable();

                StartGhosts.Add(instantiateRedGhost);

                IGhostBase instantiateYellowGhost = new YellowGhostPickable();

                StartGhosts.Add(instantiateYellowGhost);

            }
        }
    }
}
