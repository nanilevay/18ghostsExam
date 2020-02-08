using System;
using System.Collections.Generic;
namespace _18ghostsExam
{
    public interface IPlayer
    {
        List<IGhostBase> Ghosts { get; }

        string Name { get; set; }

        bool HoldingPiece { get; set; }

        bool HoldingBluePiece { get; set; }
        bool HoldingRedPiece { get; set; }
        bool HoldingYellowPiece { get; set; }

        IGhostBase ChosenPiece { get; set; }

        bool start { get; set; }

        //  add stuff from player
    }
}