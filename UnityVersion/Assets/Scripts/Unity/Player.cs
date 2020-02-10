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

        public GameObject[] GhostsP1;


        public Player()
        {
            
        }

        public void SetGhosts()
        {
            Ghosts = new List<IGhostBase>();

            for (int i = 0; i < 3; i++)
            {
                GameObject instantiateBlueGhost = GhostsP1[0];

                GameObject instanceBlue =
                    Instantiate(instantiateBlueGhost) as GameObject;

                instanceBlue.transform.SetParent(PlayerPanel.transform);

                Ghosts.Add(instanceBlue.GetComponent<IGhostBase>());

                //

                GameObject instantiateRedGhost = GhostsP1[1];

                GameObject instanceRed =
                    Instantiate(instantiateRedGhost) as GameObject;

                instanceRed.transform.SetParent(PlayerPanel.transform);

                Ghosts.Add(instanceRed.GetComponent<IGhostBase>());



                //
                GameObject instantiateYellowGhost = GhostsP1[2];

                GameObject instanceYellow =
                    Instantiate(instantiateYellowGhost) as GameObject;

                instanceYellow.transform.SetParent(PlayerPanel.transform);

                Ghosts.Add(instanceYellow.GetComponent<IGhostBase>());
            }
        }
    }

}