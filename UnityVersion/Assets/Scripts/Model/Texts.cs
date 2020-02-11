using System;
using System.Collections.Generic;
using System.Text;

namespace _18ghostsExam
{
    /// <summary>
    /// This class allows us to store all the necessary lines to be printed
    /// during the game at different instances in string format
    /// </summary>
    public class Texts
    {
        private static Texts instance;

        protected Texts()
        {

        }

        public static Texts Instance()
        {
            if (instance == null)
            {
                instance = new Texts();
            }

            return instance;
        }

        public string IntroText()
        {
            return "Welcome to 18Ghosts! Press enter to play.";
        }


        public string StartText(IPlayer Player)
        {
            return Player.Name + " , pick one of your ghosts!";

        }


        /// <summary>
        /// Text displayed to the players to show which actions are valid
        /// </summary>
        public string ActionText(string Name)
        {
            return
                "It's " + Name + "'s turn!"
                + "\n You can either:" +
                " \n Move a Piece to any adjacent tile that isn't a portal."
                + "\n Fight a ghost if the chamber is occupied (can be your " +
                "own)." + "\n Remove a ghost in the dungeon if you have one, " +
                "this can only be done" + "if there's an available tile" +
                " matching the colour of your chosen ghost" +
                "and the other player will be the one to set it.";

        }

        /// <summary>
        /// When a player is holding a piece
        /// </summary>
        public string HoldingPieceText(IGhostBase Piece)
        {

            return "holding " + Piece.colour + " piece" +
                 "\n Place on a tile of its same colour!";
        }


        public string PlacingPieceText(IGhostBase Piece, IMapElement Tile)
        {

            return "placed " + Piece + "on" + "tile (" + Tile.Pos.X +
                                "," + Tile.Pos.Y + ")";
        }

        public string OnMirrorText()
        {
            return "You're on a mirror! You can teleport.";
        }
        public string MirrorErrorText()
        {
            return "You have to move to another mirror!";
        }

        public string SentToDungeon()
        {
            return "ghost sent to dungeon";
        }

        public string DungeonErrorText()
        {
            return "invalid move!";
        }

        public string MovementError()
        {
            return "Move to a tile of your colour that isn't a portal!";
        }

        public string WrongTurn()
        {
            return "Not your turn!";
        }

        public string VictoryText(IPlayer Player)

        {
            return "Congratulations! Player " + Player.Name + "won!";
        }
    }
}