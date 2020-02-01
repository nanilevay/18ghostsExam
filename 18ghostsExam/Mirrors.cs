using System;
namespace _18ghostsExam
{
    public class Mirrors : MapElement
    {
        /// <summary>
        /// this class allows us to create and set the mirrors on their right 
        /// positions and character inside the game
        /// </summary>
        public Mirrors(Positions pos)
        {
            // mirror position
            Pos = pos;

            // mirror colour
            Colour = Colours.white;

            // mirror character
            Character = Characters.mirror;
        }
    }
}
