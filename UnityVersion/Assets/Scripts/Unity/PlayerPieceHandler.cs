using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPieceHandler : MonoBehaviour
{
    public Board board;

    // add logic later to place on board
    public GameObject[] possibleBoardPlaces;

    public GameObject ChosenPiece;// goItem

    public BoardManager bm; // hub

    public IMapElement currentPiece = null;

    voud Start()
    {
        board.pieceGrab += boardGrabPiece;

    }


    boardGrabPiece(object sender, MapEventArgs eventSend)
    {
        IMapElement piece = eventSent.Piece;

        GameObject chosenPiece;

        chosenPiece = (piece as MonoBehaviour).gameObject;

        chosenPiece.SetActive(true);

       // chosenPiece.transform.parent =
       // chosenPiece
       // chosenPiece

        ChosenPiece = chosenPiece;

    }

    
}
