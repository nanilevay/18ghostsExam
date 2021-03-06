﻿namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to define a general portal that can rotate when a
    /// ghost is killed depending on the direction it was facing previously
    /// </summary>
    public class PortalBase : IPortals, IMapElement
    {
        /// <summary>
        /// Check if tile is empty
        /// </summary>
        public bool empty { get; set; }

        /// <summary>
        /// Type of tile
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Character to represent portal
        /// </summary>
        public char Character
        {
            get

            {
                return (char)Characters.portal;
            }

            set
            {

            }
        }

        /// <summary>
        /// Position of tile in map
        /// </summary>
        public Positions Pos { get; set; }

        /// <summary>
        /// Colour of tile
        /// </summary>
        public Colours colour { get; set; }

        /// <summary>
        /// To check what piece is currently in the tile
        /// </summary>
        public IGhostBase PieceOnTile { get; set; }

        /// <summary>
        ///  Current direction the portal is facing at a given point
        /// </summary>
        public PortalDir CurrentRot { get; set; }

        /// <summary>
        /// Text displaying the direction being faced
        /// </summary>
        public string Direction;

        /// <summary>
        /// This method let's us update the direcion text displayed to the user
        /// Depending on the portal's current facing direction
        /// </summary>
        public void UpdateDir()
        {
            // If portal is facing down write "down"
            if (CurrentRot == PortalDir.down)
                Direction = "Down";

            // If portal is facing left write "left"
            if (CurrentRot == PortalDir.left)
                Direction = "Left";

            // If portal is facing right write "right"
            if (CurrentRot == PortalDir.right)
                Direction = "Right";

            // If portal is facing up write "up"
            if (CurrentRot == PortalDir.up)
                Direction = "Up";
        }

        /// <summary>
        /// This method lets us rotate our portal according to the previous
        /// rotation whenever its called
        /// </summary>
        /// <returns></returns>
        public PortalDir Rotate()
        {
            // If portal was facing down turn left
            if (CurrentRot == PortalDir.down)
                return PortalDir.left;

            // If portal was facing left turn up
            if (CurrentRot == PortalDir.left)
                return PortalDir.up;

            // If portal was facing up turn right
            if (CurrentRot == PortalDir.up)
                return PortalDir.right;

            // If portal was right down turn down
            if (CurrentRot == PortalDir.right)
                return PortalDir.down;

            // Return default in case any of the others fails
            return CurrentRot;
        }
    }
}
