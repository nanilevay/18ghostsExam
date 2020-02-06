using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class Player : MonoBehaviour, IPlayer
{
    public bool HoldingPiece { get; set; }

    public bool HoldingBluePiece { get; set; }
    public bool HoldingRedPiece { get; set; }
    public bool HoldingYellowPiece { get; set; }

    public bool OnMirror { get; set; }

    public bool ColourRestriction { get; set; }

    public IGhostBase ChosenPiece { get; set; }

    public List<IGhostBase> Ghosts
    {
        get
        {
            return new List<IGhostBase>();
        }
    }

    public string name;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public virtual void PickPiece(IGhostBase piece)
    {
        if (piece.inDungeon) //|| piece.inStart)
            Debug.Log("In Dungeon");

        if (!HoldingPiece)
        {
            Debug.Log("picked piece");
            ChosenPiece = piece;
            if (piece.colour == Colours.yellow)
                HoldingYellowPiece = true;
            if (piece.colour == Colours.blue)
                HoldingBluePiece = true;
            if (piece.colour == Colours.red)
                HoldingRedPiece = true;
            HoldingPiece = true;
        }

    }

    public virtual void PlacePiece(IMapElement tile)
    {
        if (ChosenPiece.inDungeon) // || piece.inStart)
        {
            if (tile.colour == ChosenPiece.colour)
                (ChosenPiece as MonoBehaviour).transform.position =
                    (tile as MonoBehaviour).transform.position;          
        }

        if (tile.colour == Colours.white)
            OnMirror = true;

        if (tile.PieceOnTile == null)
        {
            Debug.Log("move to empty piece");

            if (!OnMirror)
            {
                tile.PieceOnTile = ChosenPiece;
                tile.empty = false;
                (ChosenPiece as MonoBehaviour).transform.position = 
                    (tile as MonoBehaviour).transform.position;
                if ((ChosenPiece as MonoBehaviour).
                    GetComponent<Pickable>().colour == Colours.yellow)
                    HoldingYellowPiece = false;
                if ((ChosenPiece as MonoBehaviour).
                    GetComponent<Pickable>().colour == Colours.blue)
                    HoldingBluePiece = false;
                if ((ChosenPiece as MonoBehaviour).
                    GetComponent<Pickable>().colour == Colours.red)
                    HoldingRedPiece = false;
                if ((ChosenPiece as MonoBehaviour).
                    GetComponent<Pickable>().colour == Colours.white)
                    OnMirror = true;
            }

            else
            {
                if (tile.colour == Colours.white)
                    (ChosenPiece as MonoBehaviour).transform.position =
                        (tile as MonoBehaviour).transform.position;
                OnMirror = false;
            }

            tile.PieceOnTile = ChosenPiece;
        }

        else
        {
            Debug.Log("occupied piece");
            (ChosenPiece as MonoBehaviour).transform.position =
                        (tile as MonoBehaviour).transform.position;
            ChosenPiece.Fight(tile.PieceOnTile);
        }

        HoldingYellowPiece = false;
        HoldingBluePiece = false;
        HoldingRedPiece = false;
        HoldingPiece = false;
    }

    public void RemoveFromDungeon(GameObject tile)
    {

    }


}
