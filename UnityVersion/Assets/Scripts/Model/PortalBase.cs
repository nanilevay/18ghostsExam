using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _18ghostsExam;

public class PortalBase : MonoBehaviour, IPortals
{
    public PortalDir CurrentRot { get; set; }

    public Positions Position
    {
        get
        {
            return new Positions(4, 3);
        }
    }

    void Update()
    {
        UpdateDir();
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
        Debug.Log(CurrentRot);

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

    public void CheckAdjacents()
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
