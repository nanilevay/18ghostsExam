using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;

namespace _18ghostsExam
{
    public interface IBoardSetup
    {
        /// <summary>
        /// To get the prefab of the yellow portal
        /// </summary>
        IPortals yellowPortal { get; set; }

        /// <summary>
        /// To get the prefab of the blue portal
        /// </summary>
        IPortals bluePortal { get; set; }

        /// <summary>
        /// To get the prefab of the red portal
        /// </summary>
        IPortals redPortal { get; set; }

        /// <summary>
        /// Max number of rows
        /// </summary>
        int MaxX { get; set; }

        /// <summary>
        /// Max number of columns
        /// </summary>
        int MaxY { get; set; }

        /// <summary>
        /// Getting all map elements on the board to set
        /// their positions
        /// </summary>
        IMapElement[,] positions { get; set; }

        /// <summary>
        /// Getting a list of the slots in the dungeon for their positions
        /// </summary>
        DungeonSlot[,] DungeonSlots { get; set; }

        void SetUpScene();

        void InitialiseBoard();

        void BoardSetUp();

        void SetUpDungeon();

        bool CheckYellowSurrounding();
        bool CheckRedSurrounding();
        bool CheckBlueSurrounding();
    }
}