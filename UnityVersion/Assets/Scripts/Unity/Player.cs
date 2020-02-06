using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class Player : MonoBehaviour, IPlayer
{
    public bool HoldingPiece;

    public bool HoldingBluePiece;
    public bool HoldingRedPiece;
    public bool HoldingYellowPiece;

    public bool OnMirror;

    public bool ColourRestriction;

    public IGhostBase ChosenPiece;

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

    public void PlacePiece(GameObject tile)
    {
        if (ChosenPiece.inDungeon) // || piece.inStart)
        {
            if (tile.GetComponent<IMapElement>().colour == ChosenPiece.colour)
                (ChosenPiece as MonoBehaviour).transform.position =
                    tile.transform.position;
            
        }

        if (tile.GetComponent<IMapElement>().colour == Colours.white)
            OnMirror = true;

        if (tile.GetComponent<IMapElement>().PieceOnTile == null)
        {
            Debug.Log("move to empty piece");

            if (!OnMirror)
            {
                tile.GetComponent<IMapElement>().PieceOnTile = ChosenPiece;
                tile.GetComponent<IMapElement>().empty = false;
                (ChosenPiece as MonoBehaviour).transform.position = tile.transform.position;
                if ((ChosenPiece as MonoBehaviour).GetComponent<Pickable>().colour == Colours.yellow)
                    HoldingYellowPiece = false;
                if ((ChosenPiece as MonoBehaviour).GetComponent<Pickable>().colour == Colours.blue)
                    HoldingBluePiece = false;
                if ((ChosenPiece as MonoBehaviour).GetComponent<Pickable>().colour == Colours.red)
                    HoldingRedPiece = false;
                if ((ChosenPiece as MonoBehaviour).GetComponent<Pickable>().colour == Colours.white)
                    OnMirror = true;
            }

            else
            {
                if (tile.GetComponent<IMapElement>().colour == Colours.white)
                    (ChosenPiece as MonoBehaviour).transform.position =
                        tile.transform.position;
                OnMirror = false;
            }

            tile.GetComponent<IMapElement>().PieceOnTile = ChosenPiece;
        }

        else
        {
            Debug.Log("occupied piece");
            (ChosenPiece as MonoBehaviour).transform.position =
                        tile.transform.position;
            ChosenPiece.Fight(tile.GetComponent<IMapElement>().PieceOnTile);
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
