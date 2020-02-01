using System;
namespace _18ghostsExam
{
    public class RedPortal 
    {
        public Positions Pos;
        public Colours Colour;
        public Characters Character;
        public RedPortal()
        {   
            Pos = new Positions(2, 0); 
            Colour = Colours.red;
            Character = Characters.portal;
        }
    }
}
