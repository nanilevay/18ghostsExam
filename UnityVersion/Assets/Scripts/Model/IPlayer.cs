using System;
using System.Collections.Generic;
namespace _18ghostsExam
{
    public interface IPlayer
    {
        List<IGhostBase> Ghosts { get; }

        string Name { get; }
       

    }
}