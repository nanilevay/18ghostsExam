using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class Pickable : MonoBehaviour, IMapElement
{
    public Dungeon dungeon;

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

    public Positions position;

    public Positions Pos
    {
        get
        {
            return position;
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
        
    }

    public virtual void SendToDungeon(Pickable dungeonGhost)
    {
       dungeonGhost.transform.position = dungeon.transform.GetChild(0).position;
        dungeon.transform.GetChild(0).gameObject.GetComponent<DungeonSlot>().empty = false;

        /*
        foreach(DungeonSlot slot in dungeon.Slots)
        {
            //if(slot != null)
               dungeonGhost.transform.position = slot.transform.position;
        }
        */
    }

    public virtual void Place()
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
