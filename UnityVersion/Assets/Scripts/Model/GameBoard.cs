using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _18ghostsExam
{
    public class GameBoard : MonoBehaviour, IBoardSetup
    {

        public GameObject Mirror;

        /// <summary>
        /// Getting the prefabs of the tiles
        /// </summary>
        public GameObject[] Pieces;

        /// <summary>
        /// To get the prefab of the yellow portal
        /// </summary>
        public IPortals yellowPortal { get; set; }

        /// <summary>
        /// To get the prefab of the blue portal
        /// </summary>
        public IPortals bluePortal { get; set; }

        /// <summary>
        /// To get the prefab of the red portal
        /// </summary>
        public IPortals redPortal { get; set; }


        /// <summary>
        /// Blue portal to instantiate
        /// </summary>
        public GameObject BluePortal;

        /// <summary>
        /// Red portal to instantiate
        /// </summary>
        public GameObject RedPortal;

        /// <summary>
        /// Yellow portal to instantiate
        /// </summary>
        public GameObject YellowPortal;

        public List<IGhostBase> PlayerOnePanel;

        public List<IGhostBase> PlayerTwoPanel;

        /// <summary>
        /// To get the dungeon panel
        /// </summary>
        public GameObject DungeonPanel;

        /// <summary>
        /// To get the prefab for the dungeon slots
        /// </summary>
        public GameObject DungeonSlot;

        /// <summary>
        /// Getting the board game object to set everything in a grid inside it
        /// </summary>
        public GameObject BoardObject;


        /// <summary>
        /// Max number of rows
        /// </summary>
        public int MaxX
        {
            get
            {
                return 5;
            }

            set
            {
            }
        }

        /// <summary>
        /// Max number of columns
        /// </summary>
        public int MaxY
        {
            get
            {
                return 5;
            }

            set
            {
            }
        }

        /// <summary>
        /// Getting all map elements on the board to set
        /// their positions
        /// </summary>
        public IMapElement[,] positions { get; set; }

        /// <summary>
        /// Getting a list of the slots in the dungeon for their positions
        /// </summary>
        public DungeonSlot[,] DungeonSlots { get; set; }



        public void SetUpScene()
        {
            positions = new IMapElement[MaxX, MaxY];
            InitialiseBoard();
            BoardSetUp();
            DungeonSlots = new DungeonSlot[2, 10];
            SetUpDungeon();

           
        }


        /// <summary>
        /// This method allows us to set the main map with all the given positions
        /// </summary>
        public void InitialiseBoard()
        {
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if (x == 0)
                    {
                        if (y == 0 || y == 3)
                            positions[x, y] = new BlueHall();
                        if (y == 1 || y == 4)
                            positions[x, y] = new RedHall();
                        if (y == 2)
                        {
                            positions[x, y] = new RedPortals();
                            redPortal = positions[x, y] as IPortals;
                        }
                    }

                    if (x == 1)
                    {
                        if (y == 0 || y == 2 || y == 4)
                            positions[x, y] = new YellowHall();
                        if (y == 1 || y == 3)
                            positions[x, y] = new Mirror();

                    }

                    if (x == 2)
                    {
                        if (y == 1 || y == 3)
                            positions[x, y] = new BlueHall();
                        if (y == 0 || y == 2)
                            positions[x, y] = new RedHall();
                        if (y == 4)
                        {
                            positions[x, y] = new YellowPortals();
                            yellowPortal = positions[x, y] as IPortals;
                        }
                    }

                    if (x == 3)
                    {
                        if (y == 0)
                            positions[x, y] = new BlueHall();
                        if (y == 2)
                            positions[x, y] = new YellowHall();
                        if (y == 1 || y == 3)
                            positions[x, y] = new Mirror();
                        if (y == 4)
                            positions[x, y] = new RedHall();
                    }

                    if (x == 4)
                    {
                        if (y == 3)
                            positions[x, y] = new BlueHall();
                        if (y == 1)
                            positions[x, y] = new RedHall();
                        if (y == 0 || y == 4)
                            positions[x, y] = new YellowHall();
                        if (y == 2)
                        {
                            positions[x, y] = new BluePortals();
                            bluePortal = positions[x, y] as IPortals;
                        }
                    }

                    positions[x, y].Pos = new Positions(x, y);
                }
            }
        }

        /// <summary>
    /// This method allows us to set up the board by defining each tile in 
    /// its specified position for a new game according to its type
    /// </summary>
    public void BoardSetUp()
        {
            for (int x = 0; x < MaxX; x++)
            {
                for (int y = 0; y < MaxY; y++)
                {
                    if (positions[x, y] is BlueHall)
                    {
                        GameObject toInstantiate = Pieces[0];

                        GameObject instance = Instantiate(toInstantiate) as GameObject;

                        instance.transform.SetParent(BoardObject.transform);

                        positions[x, y] = instance.GetComponent<IMapElement>();
                    }

                    if (positions[x, y] is RedHall)
                    {
                        GameObject toInstantiate = Pieces[1];

                        GameObject instance = Instantiate(toInstantiate) as GameObject;

                        instance.transform.SetParent(BoardObject.transform);

                        positions[x, y] = instance.GetComponent<IMapElement>();
                    }

                    if (positions[x, y] is YellowHall)
                    {
                        GameObject toInstantiate = Pieces[2];

                        GameObject instance = Instantiate(toInstantiate) as GameObject;

                        instance.transform.SetParent(BoardObject.transform);

                        positions[x, y] = instance.GetComponent<IMapElement>();
                    }

                    if (positions[x, y] is Mirror)
                    {
                        GameObject toInstantiate = Mirror;

                        GameObject instance = Instantiate(toInstantiate) as GameObject;

                        instance.transform.SetParent(BoardObject.transform);

                        positions[x, y] = instance.GetComponent<IMapElement>();
                    }

                    if (positions[x, y] is RedPortals)
                    {
                        GameObject toInstantiate = RedPortal;

                        GameObject instance = Instantiate(toInstantiate) as GameObject;

                        instance.transform.SetParent(BoardObject.transform);

                        positions[x, y] = instance.GetComponent<IPortals>();

                        redPortal = instance.GetComponent<IPortals>();

                        redPortal.Pos = new Positions(x, y);
                    }

                    if (positions[x, y] is BluePortals)
                    {
                        GameObject toInstantiate = BluePortal;

                        GameObject instance = Instantiate(toInstantiate) as GameObject;

                        instance.transform.SetParent(BoardObject.transform);

                        positions[x, y] = instance.GetComponent<IPortals>();

                        bluePortal = instance.GetComponent<IPortals>();

                        bluePortal.Pos = new Positions(x, y);
                    }

                    if (positions[x, y] is YellowPortals)
                    {
                        GameObject toInstantiate = YellowPortal;

                        GameObject instance = Instantiate(toInstantiate) as GameObject;

                        instance.transform.SetParent(BoardObject.transform);

                        positions[x, y] = instance.GetComponent<IPortals>();

                        yellowPortal.Pos = new Positions(x, y);
                    }

                    positions[x, y].Pos = new Positions(x, y);
                }
            }
        }



        /// <summary>
        /// This void allows us to set up the dungeon for when ghosts die
        /// </summary>
        public void SetUpDungeon()
        {
            for (int a = 0; a < 2; a++)
            {
                for (int b = 0; b < 9; b++)
                {
                    GameObject slotToInstantiate = DungeonSlot;

                    GameObject slotInstance = Instantiate(slotToInstantiate) as GameObject;

                    slotInstance.transform.SetParent(DungeonPanel.transform);

                    DungeonSlots[a, b] = slotInstance.GetComponent<DungeonSlot>();
                }
            }
        }

        public bool CheckRedSurrounding()
        {
            if (positions[0, 1].PieceOnTile is RedGhostPickable &&
               redPortal.CurrentRot == PortalDir.left)
            {
                return true;
            }

            if (positions[0, 3].PieceOnTile is BlueGhostPickable &&
                redPortal.CurrentRot == PortalDir.right)
            {
                return true;
            }

            if (positions[1, 2].PieceOnTile is RedGhostPickable &&
                redPortal.CurrentRot == PortalDir.down)
            {
                return true;
            }
            return false;
        }


       public  bool CheckYellowSurrounding()
        {
            if (positions[1, 4].PieceOnTile is YellowGhostPickable &&
                yellowPortal.CurrentRot == PortalDir.up)
            {
                return true;
            }

            if (positions[2, 3].PieceOnTile is YellowGhostPickable &&
                yellowPortal.CurrentRot == PortalDir.left)
            {
                return true;
            }

            if (positions[3, 4].PieceOnTile is YellowGhostPickable &&
                yellowPortal.CurrentRot == PortalDir.down)
            {
                return true;
            }

            return false;
        }

        public bool CheckBlueSurrounding()
        {
            if (positions[4, 2].PieceOnTile is BlueGhostPickable &&
                bluePortal.CurrentRot == PortalDir.left)
            {
                return true;
            }

            if (positions[3, 2].PieceOnTile is BlueGhostPickable &&
                 bluePortal.CurrentRot == PortalDir.up)
            {
                return true;
            }

            if (positions[1, 4].PieceOnTile is BlueGhostPickable &&
                bluePortal.CurrentRot == PortalDir.right)
            {
                return true;
            }
            return false;
        }


    }
}
