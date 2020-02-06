using System;
namespace _18ghostsExam
{
    public class BluePortal  //: IPortals
    {
        public Positions currentPos = new Positions(1,0);

        public PortalDir CurrentRot
        {
            get
            {
                return PortalDir.down;
            }
        }

        
        public Colours Colour;

        public BluePortal()
        {   
            //Pos = new Positions(2, 4);
            //Colour = Colours.blue;
            //Character = Characters.portal;
        }

        public void Rotate()
        { }

    }
}
