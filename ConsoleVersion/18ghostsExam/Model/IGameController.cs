using System;
using System.Collections.Generic;
using System.Text;

namespace _18ghostsExam
{
    /// <summary>
    /// This interface allows us to set the game's progress, to be implemented
    /// by whichever class controls the gameloop of the game
    /// </summary>
    interface IGameController
    {
        // The  game board
        GameBoard board { get; set; }

        // The player whose turn is currently active
        IPlayer CurrentPlayer { get; set; }

        // First player
        IPlayer PlayerOne { get; set; }

        // Second player
        IPlayer PlayerTwo { get; set; }

        // Previous tile for checking inputs
        IMapElement PreviousTile { get; set; }

        // To switch between players
        void Play();

        // To setup game
        void Start();

        // For the gameLoop
        void Update();

        // Check if a move is valid
        bool ValidMove(IMapElement NextTile);

        // Place Piece on the board
        void PlacePiece(IMapElement ChosenTile);

        // Send piece to dungeon
        void SendToDungeon(IGhostBase dungeonGhos);
    }
}