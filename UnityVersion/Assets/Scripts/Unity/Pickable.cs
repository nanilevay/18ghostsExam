using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class Pickable : MonoBehaviour, IMapElement
{
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


    public virtual void Place()
    {

    }

    public virtual void PickPiece()
    {
        gameObject.SetActive(false);
    }

    public virtual void DropPiece()
    {

    }
}
