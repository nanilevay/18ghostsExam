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

    void Start()
    {
        board.pieceGrab += boardGrabPiece;

    }


    public void boardGrabPiece(object sender, MapEventArgs eventSend)
    {
        IMapElement piece = eventSend.Piece;

        GameObject chosenPiece;

        chosenPiece = (piece as MonoBehaviour).gameObject;

        chosenPiece.SetActive(true);

       // chosenPiece.transform.parent =
       // chosenPiece
       // chosenPiece

        ChosenPiece = chosenPiece;

    }

    
}
