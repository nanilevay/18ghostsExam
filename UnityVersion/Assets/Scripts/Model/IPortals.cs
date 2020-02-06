using System;

namespace _18ghostsExam
{
    public interface IPortals
    {
        PortalDir CurrentRot { get; }

        Positions Position { get; }

        void Rotate();
    }
}
