using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;

public class YellowHall : Pickable
{

    public void PlacePiece(Pickable piece)
    {
        if(piece.colour == this.colour)
            piece.transform.position = this.transform.position;
    }
}
