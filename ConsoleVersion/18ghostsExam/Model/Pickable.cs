using System.Collections;
using System.Collections.Generic;
using _18ghostsExam;

/// <summary>
/// This class allows us to set a default pickable piece element for the ghosts
/// </summary>
public class Pickable : IGhostBase
{
    public bool empty { get; set; }

    public bool inDungeon { get; set; }

    public bool OnMirror { get; set; }

    public virtual string Type 
    { 
        get
        {
            return "default piece";
        }
     }

    public char character
    {
        get;

        //return (char)Characters.ghost;


        set;
        

        
    }

    public Positions Pos { get; set; }

    public Colours colour { get; set; }

    public IGhostBase GhostDied { get; set; }

    public virtual IGhostBase Fight(IGhostBase other)
    {
        return null;
    }
}
