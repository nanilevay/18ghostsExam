using System;
namespace _18ghostsExam
{
    public class YellowPortal : Portals
    {
        public YellowPortal()
        {   
            Pos = new Positions(4, 2);
            Colour = Colours.yellow;
            Character = Characters.portal;
        }
    }
}
