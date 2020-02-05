using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using _18ghostsExam;

public class Board : MonoBehaviour
{

    public Map map;

    private int boardSpaces;

    public List<IMapElement> mapPieces = new List<IMapElement>();

    public event EventHandler<MapEventArgs> pieceGrab;

    public event EventHandler<MapEventArgs> pieceMove;

    public event EventHandler<MapEventArgs> pieceDrop;

    public void Start()
    {
        boardSpaces = 25;//map.MaxX * map.MaxY;
    }

    public void GrabPiece(IMapElement piece)
    {
        if (mapPieces.Count < boardSpaces)
        {
            Collider collider = (piece as MonoBehaviour).GetComponent<Collider>();

            if(collider.enabled)
            {
                collider.enabled = false;

                mapPieces.Add(piece);

                piece.PickPiece();

                if(pieceGrab != null)
                {
                    pieceGrab(this, new MapEventArgs(piece));
                }
            }
        }
    }

    public void MovePiece()
    {

    }

    public void DropPiece(IMapElement piece)
    {
        if (mapPieces.Contains(piece))
        {
            mapPieces.Remove(piece);

            piece.DropPiece();

            Collider collider = (piece as MonoBehaviour).GetComponent<Collider>();

            if(collider != null)
            {
                collider.enabled = true;
            }

            if(pieceDrop != null)
            {
                pieceDrop(this, new MapEventArgs(piece));
            }
        }
    }
}
