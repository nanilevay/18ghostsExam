using System;
namespace _18ghostsExam
{
    public class YellowPortal : Portals
    {
        public Positions Pos;
        public Colours Colour;
        public Characters Character;
        public YellowPortal()
        {
            Pos = new Positions(4, 2);
            Colour = Colours.yellow;
            Character = Characters.portal;
        }
    }
}
