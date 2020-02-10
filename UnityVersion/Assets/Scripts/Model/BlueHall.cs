using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;

/// <summary>
/// This class allows us to set the blue tiles
/// </summary>
public class BlueHall : Tiles
{
    /// <summary>
    /// This constructor lets us set the blue tile's colour
    /// </summary>
    public BlueHall()
    {
        // These tiles will be displayed as blue for restriction checking
        colour = Colours.blue;
    }
}
