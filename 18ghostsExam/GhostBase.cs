using System;
namespace _18ghostsExam
{
    public class GhostBase
    {
        Positions Pos;

        Characters Img;

        Colours Colour;

        public GhostBase(Positions pos, Characters img, Colours colour)
        {
            Pos = pos;
            Img = img;
            Colour = colour;
        }
    }
}