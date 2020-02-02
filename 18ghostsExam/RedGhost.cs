using System;
namespace _18ghostsExam
{
    public class RedGhost : GhostBase
    {
        public RedGhost(Positions pos)
        {
            Pos = pos;
            Colour = Colours.blue;
            Character = Characters.ghost;
        }
    }
}
