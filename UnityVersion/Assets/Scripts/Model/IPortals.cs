using System;

namespace _18ghostsExam
{
    public interface IPortals : IMapElement
    {
        PortalDir CurrentRot { get; set; }

        PortalDir Rotate();

        void UpdateDir();
    }
}
