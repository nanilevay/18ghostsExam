namespace _18ghostsExam
{
    /// <summary>
    /// This class lets us set the different tiles in the map, implementing the
    /// IMapElement interface in order to have all its properties and always
    /// be aware of its state within the current round on the board
    /// </summary>
    public class Tiles : IMapElement
    {
        /// <summary>
        /// Type of tile
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Character to represent the tile
        /// </summary>
        public char Character
        {
            get

            {
                return (char)Characters.map;
            }

            set
            {

            }
        }

        /// <summary>
        /// Colour of the tile
        /// </summary>
        public Colours colour { get; set; }

        /// <summary>
        /// Position of the tile on the map
        /// </summary>
        public Positions Pos { get; set; }

        /// <summary>
        /// checking if the tile is empty or occupied
        /// </summary>
        public bool empty { get; set; }

        /// <summary>
        /// Gets the ghost contained in the tile if any
        /// </summary>
        public IGhostBase PieceOnTile { get; set; }

        /// <summary>
        /// To set the colours in the unity version of the game
        /// </summary>
        public Tiles()
        {
            // If the tile is white set the sprite colour to white
            if (this is Mirror)
                this.colour = Colours.white;

            // If the tile is yellow set the sprite colour to white
            if (this is YellowHall)
                this.colour = Colours.yellow;

            // If the tile is blue set the sprite colour to white
            if (this is BlueHall)
                this.colour = Colours.blue;

            // If the tile is red set the sprite colour to white
            if (this is RedHall)
                this.colour = Colours.red;
        }
    }
}