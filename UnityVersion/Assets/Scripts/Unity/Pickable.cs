using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class Pickable : MonoBehaviour, IGhostBase
{
    public Dungeon dungeon;

    public GameObject bluePortal;

    public bool empty
    {
        get
        {
            return true;
        }
    }

    public bool inDungeon
    {
        get
        {
            return false;
        }
        set
        {

        }
    }

    public virtual string Type 
    { 
        get
        {
            return "default piece";
        }
     }

    public Sprite img;

    public Sprite Img
    {
        get 
        { 
            return img;
        }
    }

    public char Character;

    public char character
    {
        get
        {
            return Character;
        }
    }

//    public Positions position;

    public Positions Pos
    {
        get
        {
            return new Positions(0,0);
        }

        set
        {

        }
    }


    public Colours colours;

    public Colours colour
    {
        get
        {
            return colours;
        }
    }

    public virtual void Fight(IGhostBase other)
    {
        bluePortal.GetComponent<BluePortals>().Direction.text = "" + (char)PortalDir.up;
    }

    public virtual void SendToDungeon(IGhostBase dungeonGhost)
    {     
        foreach(Transform slot in dungeon.transform)
        {
            if(slot.gameObject.GetComponent<DungeonSlot>().empty == true)
            {
                (dungeonGhost as MonoBehaviour).transform.position = slot.position;
                slot.gameObject.GetComponent<DungeonSlot>().empty = false;
                break;
            }            
        }

        inDungeon = true;
    }

    public virtual void Place(Player player)
    {

    }

    public virtual void PickPiece()
    {
        //gameObject.SetActive(false);
    }

    public virtual void DropPiece()
    {

    }
}
