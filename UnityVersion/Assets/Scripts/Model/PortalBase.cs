using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _18ghostsExam;

public class PortalBase : MonoBehaviour, IPortals, IMapElement
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
    /// Character to represent tile
    /// </summary>
    public char Character { get; }

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

    public PortalDir CurrentRot { get; set; }

    void Update()
    {
        UpdateDir();
        CheckAdjacents();
    }
    
    //public List<GameObject> AdjacentTiles;

    public TextMeshProUGUI Direction;

    public void UpdateDir()
    {
        if (CurrentRot == PortalDir.down)
            Direction.text = "Down";
        if (CurrentRot == PortalDir.left)
            Direction.text = "Left";
        if (CurrentRot == PortalDir.right)
            Direction.text = "Right";
        if (CurrentRot == PortalDir.up)
            Direction.text = "Up";
    }

    public PortalDir Rotate()
    {

        if (CurrentRot == PortalDir.down)
            return PortalDir.left;

        if (CurrentRot == PortalDir.left)
            return PortalDir.up;

        if (CurrentRot == PortalDir.up)
            return PortalDir.right;

        if (CurrentRot == PortalDir.right)
            return PortalDir.down;

        return PortalDir.left;
    }

    public virtual void CheckAdjacents()
    {
        /*
        foreach(GameObject piece in AdjacentTiles)
        {
            if (!piece.GetComponent<YellowHall>().empty)
                Debug.Log("oooh");
        }
        */
    }
}
