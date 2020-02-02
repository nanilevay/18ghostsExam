using System;
namespace _18ghostsExam
{
    public class BluePortal : Portals
    {
        public Positions Pos;
        public Colours Colour;
        public Characters Character;

        public BluePortal()
        {   
            Pos = new Positions(2, 4);
            Colour = Colours.blue;
            Character = Characters.portal;
        }
    }
}
