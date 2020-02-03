using System;
namespace _18ghostsExam
{
    public class BlueGhost : GhostBase
    {
        public BlueGhost(Positions pos)
        {
            Pos = pos;
            Colour = Colours.blue;
            Character = Characters.ghost;
        }
    }
}
