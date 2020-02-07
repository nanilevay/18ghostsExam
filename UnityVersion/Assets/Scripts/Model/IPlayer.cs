using System;
using System.Collections.Generic;
namespace _18ghostsExam
{
    public interface IPlayer
    {
        List<IGhostBase> Ghosts { get; }

        string Name { get; }

        bool HoldingPiece { get; set; }

        bool HoldingBluePiece { get; set; }
        bool HoldingRedPiece { get; set; }
        bool HoldingYellowPiece { get; set; }
        bool ColourRestriction { get; set; }

        IGhostBase ChosenPiece { get; set; }

        //  add stuff from player
    }
}