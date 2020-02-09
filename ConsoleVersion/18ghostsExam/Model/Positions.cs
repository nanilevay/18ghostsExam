using System;
namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to get a certain position on the board with both
    /// X and Y coordinates
    /// </summary>
    public class Positions
    {
        /// <summary>
        /// The x coordinate of our board position
        /// </summary>
        public int X;

        /// <summary>
        /// The y coordinate of our board position
        /// </summary>
        public int Y;

        /// <summary>
        /// This constructor allows us to set positions giving both coordinates
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public Positions(int x, int y)
        {
            // x of position
            X = x;

            // y of position
            Y = y;
        }
    }
}