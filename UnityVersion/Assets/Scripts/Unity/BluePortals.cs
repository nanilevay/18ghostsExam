using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _18ghostsExam;

public class BluePortals : MonoBehaviour, IPortals
{
    public PortalDir CurrentRot
    {
        get
        {
            return PortalDir.down;
        }
    }

    public Positions Position
    {
        get
        {
            return new Positions(4, 3);
        }
    }

    public List<GameObject> AdjacentTiles;
    public TextMeshProUGUI Direction;

    // Start is called before the first frame update
    void Start()
    {
        Direction.text = "" + (char)CurrentRot;
    }

    void Update()
    {
        CheckAdjacents();
    }

    public void Rotate()
    {      
        if(Direction.text == "" + (char)PortalDir.down)
            Direction.text = "" + (char)PortalDir.left;

        if (Direction.text == "" + (char)PortalDir.left)
            Direction.text = "" + (char)PortalDir.up;

        if (Direction.text == "" + (char)PortalDir.up)
            Direction.text = "" + (char)PortalDir.right;

        if (Direction.text == "" + (char)PortalDir.right)
            Direction.text = "" + (char)PortalDir.down;
        
        Debug.Log(Direction.text);
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
