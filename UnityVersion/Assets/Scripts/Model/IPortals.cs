using System;

namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to set a portal and define
    /// it's current rotation as well as update it
    /// </summary>
    public interface IPortals : IMapElement
    {
        // Current rotation
        PortalDir CurrentRot { get; set; }

        // Change rotation 
        PortalDir Rotate();

        // Update the direction shown
        void UpdateDir();
    }
}
