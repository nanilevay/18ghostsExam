﻿using System;
using System.Collections.Generic;
namespace _18ghostsExam
{
    public class Player
    {
        public List<GhostBase> Ghosts;
        public Player()
        {
            Ghosts = new List<GhostBase>();

            for (int i = 0; i < 9; i++)
                Ghosts.Add(new YellowGhost(new Positions(-1,-1)));
        }
    }
}