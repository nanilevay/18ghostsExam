using System;
using System.Collections.Generic;
namespace _18ghostsExam
{
    public interface IPlayer
    {
        List<IGhostBase> Ghosts { get; set; }

        List<IGhostBase> EscapedGhosts { get; set; }

        string Name { get; set; }

        bool HoldingPiece { get; set; }

        IGhostBase ChosenPiece { get; set; }

        bool start { get; set; }
    }
}