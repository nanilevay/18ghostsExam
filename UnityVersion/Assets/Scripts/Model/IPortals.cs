using System;

namespace _18ghostsExam
{
    public interface IPortals
    {
        PortalDir CurrentRot { get; set; }

        Positions Position { get; }

        PortalDir Rotate();
    }
}
