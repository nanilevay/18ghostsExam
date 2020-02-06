using System;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;
using UnityEngine.UI;

namespace _18ghostsExam
{  
    public class Tiles : MonoBehaviour, IMapElement
    {

        private Button _button;

        private IPlayer player;

        void Awake()
        { 
            _button = GetComponent<Button>();
            player = GameObject.Find("CurrentPlayer").GetComponent<IPlayer>();
        }

        public void OnItemClicked()
        {
            if (player.ChosenPiece.inDungeon) // || piece.inStart)
            {
                if (this.colour == player.ChosenPiece.colour)
                    (player.ChosenPiece as MonoBehaviour).transform.position =
                        (this as MonoBehaviour).transform.position;
            }

            if (this.colour == Colours.white)
                player.OnMirror = true;

            if (this.PieceOnTile == null)
            {
                Debug.Log("move to empty piece");

                if (!player.OnMirror)
                {
                    this.PieceOnTile = player.ChosenPiece;
                    this.empty = false;
                    (player.ChosenPiece as MonoBehaviour).transform.position =
                        (this as MonoBehaviour).transform.position;
                    if ((player.ChosenPiece as MonoBehaviour).
                        GetComponent<Pickable>().colour == Colours.yellow)
                        player.HoldingYellowPiece = false;
                    if ((player.ChosenPiece as MonoBehaviour).
                        GetComponent<Pickable>().colour == Colours.blue)
                        player.HoldingBluePiece = false;
                    if ((player.ChosenPiece as MonoBehaviour).
                        GetComponent<Pickable>().colour == Colours.red)
                        player.HoldingRedPiece = false;
                    if ((player.ChosenPiece as MonoBehaviour).
                        GetComponent<Pickable>().colour == Colours.white)
                        player.OnMirror = true;
                }

                else
                {
                    if (this.colour == Colours.white)
                        (player.ChosenPiece as MonoBehaviour).transform.position =
                            (this as MonoBehaviour).transform.position;
                    player.OnMirror = false;
                }

                this.PieceOnTile = player.ChosenPiece;
            }

            else
            {
                Debug.Log("occupied piece");
                (player.ChosenPiece as MonoBehaviour).transform.position =
                            (this as MonoBehaviour).transform.position;
                player.ChosenPiece.Fight(this.PieceOnTile);
            }

            player.HoldingYellowPiece = false;
            player.HoldingBluePiece = false;
            player.HoldingRedPiece = false;
            player.HoldingPiece = false;

        }
        
        public IGhostBase pieceOnTile;

        public IGhostBase PieceOnTile
        {
            get
            {
                return pieceOnTile;
            }

            set
            {

            }
        }

        public string Type { get; set; }

        public char Character { get; }

        public Colours colours;

        public Colours colour
        {
            get
            {
                return colours;
            }
        }

        public Sprite Img { get; }

        public Positions Pos { get; set; }

        public bool empty
        {
            get
            {
                return true;
            }

            set
            {

            }
        }

    }
}
