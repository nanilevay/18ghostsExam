using System;
using System.Collections.Generic;
using System.Text;

namespace _18ghostsExam
{

    public class DefaultElement
    {
        public Positions Pos;
        public Colours Colour;
        public Characters Character;
        public DefaultElement(int x, int y)
        {
            Pos = new Positions(x, y);
            Colour = Colours.white;
            Character = Characters.map;
        }

    }

}

