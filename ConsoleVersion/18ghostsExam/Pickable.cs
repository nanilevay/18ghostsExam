namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to set a default pickable piece element for 
    /// the ghosts
    /// </summary>
    public class Pickable : IGhostBase
    {
        /// <summary>
        /// If the piece is in the dungeon
        /// </summary>
        public bool inDungeon { get; set; }

        /// <summary>
        /// If the piece is on a mirror
        /// </summary>
        public bool OnMirror { get; set; }

        /// <summary>
        /// The type of piece
        /// </summary>
        public virtual string Type
        {
            get
            {
                return "default piece";
            }
        }

        /// <summary>
        /// The character representing the piece
        /// </summary>
        public char character { get; set; }

        /// <summary>
        /// Piece's position on the board
        /// </summary>
        public Positions Pos { get; set; }

        /// <summary>
        /// Piece's colour
        /// </summary>
        public Colours colour { get; set; }

        /// <summary>
        /// To check what ghost lost in a fight depending on the colour
        /// </summary>
        public IGhostBase GhostDied { get; set; }

        /// <summary>
        /// Fight other ghost and determine winner
        /// </summary>
        /// <param name="other">ghost being fought</param>
        /// <returns></returns>
        public virtual IGhostBase Fight(IGhostBase other)
        {
            return null;
        }
    }
}
