﻿using System;
namespace _18ghostsExam
{
    public class RedPortal : Portals
    {
        public RedPortal()
        {   
            Pos = new Positions(2, 0); 
            Colour = Colours.red;
            Character = Characters.portal;
        }
    }
}
