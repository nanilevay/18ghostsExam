using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

/// <summary>
/// This class allows us to set a default pickable piece element for the ghosts
/// </summary>
public class Pickable : MonoBehaviour, IGhostBase
{

    public GameObject bluePortal;

    public bool empty
    {
        get
        {
            return true;
        }
    }

    public bool inDungeon { get; set; }

    public bool OnMirror { get; set; }

    public bool inStart { get; set; }

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

    public IGhostBase GhostDied { get; set; }

    public virtual IGhostBase Fight(IGhostBase other)
    {
        Debug.Log("fight");
        return null;
    }

    public void RemoveFromDungeon()
    {

    }

}
