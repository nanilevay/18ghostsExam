//using _18ghostsExam;

namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to define a blue portal with the colour and rotation
    /// </summary>
    public class BluePortals : PortalBase
    {
        /// <summary>
        /// This constructor lets us set the portal's colour and direction
        /// </summary>
        public BluePortals()
        {
            // colour of the portal
            colour = Colours.blue;

            // direction the portal is facing
            CurrentRot = PortalDir.down;
        }
    }
}
