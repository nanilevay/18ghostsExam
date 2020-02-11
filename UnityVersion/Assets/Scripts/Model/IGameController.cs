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
        // The board
        GameBoard board { get; set; }

        IPlayer CurrentPlayer { get; set; }

        IPlayer PlayerOne { get; set; }

        IPlayer PlayerTwo { get; set; }

        IMapElement PreviousTile { get; set; }

        IMapElement NextTile { get; set; }

        void Play();

        void Start();

        void FirstPlay();

        void MirrorTeleport();

        void DisplayGhosts();

        void DisplayActions();

        void Update();

        void DungeonCheck();

        void WinCheck();

        void PickFromDungeon();

        void PickPiece();

        bool ValidMove(IMapElement NextTile);

        void PlacePiece(IMapElement ChosenTile);

        void SendToDungeon(IGhostBase dungeonGhos);

        void PlaceStartGhosts();

        void MoveGhosts();



    }
}
