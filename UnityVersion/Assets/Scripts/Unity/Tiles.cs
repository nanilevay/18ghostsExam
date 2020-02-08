using System;
using System.Collections.Generic;
using UnityEngine;
using _18ghostsExam;
using UnityEngine.UI;

namespace _18ghostsExam
{  
    public class Tiles : MonoBehaviour, IMapElement
    {
        public string Type { get; set; }

        public char Character { get; }

        public Colours colour { get; set; }

        public Positions Pos { get; set; }

        public bool empty { get; set; }

        private IPlayer Player;

        void Awake()
        { 
            Player = GameObject.Find("CurrentPlayer").GetComponent<IPlayer>();

            if (colour == Colours.white)
                this.GetComponent<Image>().color = Color.white;
            if (colour == Colours.yellow)
                this.GetComponent<Image>().color = Color.yellow;
            if (colour == Colours.blue)
                this.GetComponent<Image>().color = Color.blue;
            if (colour == Colours.red)
                this.GetComponent<Image>().color = Color.red;
        }

        public virtual void PlacePiece(IPlayer CurrentPlayer)
        {
            Debug.Log(CurrentPlayer.ChosenPiece.Type);
            Debug.Log(CurrentPlayer.ChosenPiece.OnMirror);

            if (PieceOnTile == null)
            {
                Debug.Log("move" + CurrentPlayer.ChosenPiece.colour + "to empty"
                    + colour + "piece");

                if (CurrentPlayer.ChosenPiece.OnMirror)
                {
                    Debug.Log("onMirror");

                    if (colour == Colours.white)
                        (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                                (this as MonoBehaviour).transform.position;

                    CurrentPlayer.ChosenPiece.OnMirror = false;
                }

                else if (CurrentPlayer.ChosenPiece.inDungeon)
                {
                    Debug.Log("Jailed");

                    if (colour == CurrentPlayer.ChosenPiece.colour)
                    {
                        (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                                (this as MonoBehaviour).transform.position;
                        CurrentPlayer.ChosenPiece.inDungeon = false;
                    }
                }

                else
                {
                    if (CurrentPlayer.ChosenPiece.colour == Colours.yellow)
                        CurrentPlayer.HoldingYellowPiece = false;

                    if (CurrentPlayer.ChosenPiece.colour == Colours.blue)
                        CurrentPlayer.HoldingBluePiece = false;

                    if (CurrentPlayer.ChosenPiece.colour == Colours.red)
                        CurrentPlayer.HoldingRedPiece = false;

                    if (colour == Colours.white && !CurrentPlayer.ChosenPiece.OnMirror)
                        CurrentPlayer.ChosenPiece.OnMirror = true;

                    (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                                (this as MonoBehaviour).transform.position;

                }

                empty = false;

                PieceOnTile = CurrentPlayer.ChosenPiece;
            }

            else
            {
                Debug.Log("occupied piece");

                (CurrentPlayer.ChosenPiece as MonoBehaviour).transform.position =
                            (this as MonoBehaviour).transform.position;

                CurrentPlayer.ChosenPiece.Fight(PieceOnTile);

                PieceOnTile = CurrentPlayer.ChosenPiece;
            }
            CurrentPlayer.HoldingPiece = false;
        }

        public IGhostBase PieceOnTile{ get; set; }
    }
}
