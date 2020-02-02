using System;
namespace _18ghostsExam
{
    public class YellowGhost : GhostBase
    {
        public YellowGhost(Positions pos)
        {
            Pos = pos;
            Colour = Colours.yellow;
            Character = Characters.ghost;
        }
    }
}
