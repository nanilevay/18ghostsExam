using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using _18ghostsExam;

    /// <summary>
    /// This interface lets us define a generic map element in order to know its
    /// state during the game and assign its values
    /// </summary>
    public interface IMapElement
    {
        /// <summary>
        /// Check if tile is empty
        /// </summary>
        bool empty { get; set; }

        /// <summary>
        /// Type of tile
        /// </summary>
        string Type { get; }

        /// <summary>
        /// Character to represent tile
        /// </summary>
        char Character { get; }

        /// <summary>
        /// Position of tile in map
        /// </summary>
        Positions Pos { get; set; }
        
        /// <summary>
        /// Colour of tile
        /// </summary>
        Colours colour { get; set; }

        /// <summary>
        /// To check what piece is currently in the tile
        /// </summary>
        IGhostBase PieceOnTile { get; set; }
    }



