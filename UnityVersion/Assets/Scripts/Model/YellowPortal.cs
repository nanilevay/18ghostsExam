using System;
namespace _18ghostsExam
{
    public class YellowPortal //: IPortals
    {
        public PortalDir CurrentRot
        {
            get
            {
                return PortalDir.right;
            }
        }

       
        public YellowPortal()
        {
            //Pos = new Positions(4, 2);
            //Colour = Colours.yellow;
            //Character = Characters.portal;
        }

        public void Rotate()
        {

        }
    }
}
