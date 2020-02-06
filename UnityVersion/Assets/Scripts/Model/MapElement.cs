using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using _18ghostsExam;

/// <summary>
/// this interface lets us define a generic map element in order to know its
/// state during the game and assign its values
/// </summary>
    public interface IMapElement
    {
        /// <summary>
        /// check if tile is empty
        /// </summary>
        bool empty { get; set; }

        /// <summary>
        /// type of tile
        /// </summary>
        string Type { get; }

        /// <summary>
        /// character to represent tile
        /// </summary>
        char Character { get; }

        /// <summary>
        /// position of tile in map
        /// </summary>
        Positions Pos { get; }
        
        /// <summary>
        /// colour of tile
        /// </summary>
        Colours colour { get; }

        /// <summary>
        /// to check what piece is currently in the tile
        /// </summary>
        IGhostBase PieceOnTile { get; set; }

    }



