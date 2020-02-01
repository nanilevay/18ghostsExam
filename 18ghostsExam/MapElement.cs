using System;
using System.Collections.Generic;
using System.Text;


namespace _18ghostsExam
{

    public class MapElement
    {
        public Positions Pos;
        public Colours Colour;
        public Characters Character;

        public MapElement(Positions pos, Colours colour, Characters character)
        {
            Pos = pos;
            Colour = colour;
            Character = character;
        }

    }

}

