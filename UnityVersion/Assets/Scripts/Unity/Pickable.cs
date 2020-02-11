using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using _18ghostsExam;

namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to set a default pickable piece element for the ghosts
    /// </summary>
    public class Pickable : MonoBehaviour, IGhostBase
    {
        /// <summary>
        /// Check if the piece is empty
        /// </summary>
        public bool empty { get; set; }

        /// <summary>
        /// Check if the piece is in the dungeon
        /// </summary>
        public bool inDungeon { get; set; }

        /// <summary>
        /// Check if the piece is on a mirror
        /// </summary>
        public bool OnMirror { get; set; }

        /// <summary>
        /// Return the type of piece
        /// </summary>
        public virtual string Type
        {
            get
            {
                return "default piece";
            }
        }

        /// <summary>
        /// Get and set the character to represent the piece
        /// </summary>
        public char character { get; set; }

        /// <summary>
        /// Get and set the piece's position
        /// </summary>
        public Positions Pos { get; set; }

        /// <summary>
        /// Get and set the piece's colour
        /// </summary>
        public Colours colour { get; set; }

        /// <summary>
        /// Default to override to check fights between different types of ghosts
        /// </summary>
        /// <param name="other">ghost being fought</param>
        /// <returns></returns>
        public virtual IGhostBase Fight(IGhostBase other)
        {
            return null;
        }

        /// <summary>
        /// To determine which ghost lost the fight to rotate the portal
        /// </summary>
        /// /// <param name="deadGhost">ghost that lost fight</param>
        public IGhostBase GhostDied { get; set; }
    }
}