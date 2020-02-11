using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;
using UnityEngine.UI;

namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to set our players for the game, by giving them their
    /// ghost pieces and checking what action is being done in each state of the
    /// game in the main loop
    /// </summary>
    public class Player : MonoBehaviour, IPlayer
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

        /// <summary>
        /// The list of ghosts belonging to the player
        /// </summary>
        public List<IGhostBase> Ghosts { get; set; }

        /// <summary>
        /// The list of ghosts belonging to the player
        /// </summary>
        public List<IGhostBase> StartGhosts { get; set; }

        /// <summary>
        /// The list of escaped ghosts of the player
        /// </summary>
        public List<IGhostBase> EscapedGhosts { get; set; }

        /// <summary>
        /// The player's name to be displayed in the game
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Player one's ghost panel
        /// </summary>
        public GameObject PlayerPanel;

        /// <summary>
        /// Getting the ghosts prefabs
        /// </summary>
        public GameObject[] GhostsP1;


        /// <summary>
        /// This method allows us to set our ghosts on the player ghost panel
        /// </summary>
        public void SetGhosts()
        {
            // Creating our ghost list
            Ghosts = new List<IGhostBase>();

            /// Create 9 ghosts for the player
            for (int i = 0; i < 3; i++)
            {
                // Instantiate the blue ghost prefab
                GameObject instantiateBlueGhost = GhostsP1[0];

                //  Instantiate the blue ghost 
                GameObject instanceBlue =
                    Instantiate(instantiateBlueGhost) as GameObject;

                // Set parent to the panel
                instanceBlue.transform.SetParent(PlayerPanel.transform);

                // Add to player ghost list
                Ghosts.Add(instanceBlue.GetComponent<IGhostBase>());

                // Instantiate red ghost prefab
                GameObject instantiateRedGhost = GhostsP1[1];

                // Make game object in the game
                GameObject instanceRed =
                    Instantiate(instantiateRedGhost) as GameObject;

                // Make the parent the panel
                instanceRed.transform.SetParent(PlayerPanel.transform);

                // Add to player list
                Ghosts.Add(instanceRed.GetComponent<IGhostBase>());

                // Instantiate yellow ghost from prefab
                GameObject instantiateYellowGhost = GhostsP1[2];

                // Set yellow ghost in game
                GameObject instanceYellow =
                    Instantiate(instantiateYellowGhost) as GameObject;

                // Transform ghost parent to the panel
                instanceYellow.transform.SetParent(PlayerPanel.transform);

                // Add ghost to the list
                Ghosts.Add(instanceYellow.GetComponent<IGhostBase>());
            }
        }
    }
}