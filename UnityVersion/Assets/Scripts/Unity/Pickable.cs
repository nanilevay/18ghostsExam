using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class Pickable : MonoBehaviour, IMapElement
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

    public bool inDungeon = false;

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

    public char character;

    public char Character
    {
        get
        {
            return character;
        }
    }

//    public Positions position;

    public Positions Pos
    {
        get
        {
            return new Positions(0,0);
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

    public virtual void Fight(Pickable other)
    {
        bluePortal.GetComponent<BluePortals>().Direction.text = "" + (char)PortalDir.up;
    }

    public virtual void SendToDungeon(Pickable dungeonGhost)
    {     
        foreach(Transform slot in dungeon.transform)
        {
            if(slot.gameObject.GetComponent<DungeonSlot>().empty == true)
            {
                dungeonGhost.transform.position = slot.position;
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
