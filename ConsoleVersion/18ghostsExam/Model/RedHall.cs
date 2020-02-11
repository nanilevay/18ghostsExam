using System.Collections;
using System.Collections.Generic;
//using _18ghostsExam;

namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to set a red tile with the default colour being red
    /// </summary>
    public class RedHall : Tiles
    {
        /// <summary>
        /// Constructor to set the tile colour
        /// </summary>
        public RedHall()
        {
            // colour of tile is red to be displayed
            colour = Colours.red;
        }
    }
}