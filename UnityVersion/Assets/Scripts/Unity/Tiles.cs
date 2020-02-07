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

        public Sprite Img { get; }

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

        public void OnItemClicked()
        {
            Debug.Log(Player.ChosenPiece.Type);
            Debug.Log(Player.ChosenPiece.OnMirror);

            Debug.Log(Player.HoldingYellowPiece);

            if (PieceOnTile == null)
            {                          
                Debug.Log("move" + Player.ChosenPiece.colour + "to empty"
                    + colour + "piece");

                if (Player.ChosenPiece.OnMirror)
                {
                    Debug.Log("onMirror");

                    if (colour == Colours.white)
                        (Player.ChosenPiece as MonoBehaviour).transform.position =
                                (this as MonoBehaviour).transform.position;

                    Player.ChosenPiece.OnMirror = false;
                }

                else if (Player.ChosenPiece.inDungeon)
                {
                    Debug.Log("Jailed");

                    if (colour == Player.ChosenPiece.colour)
                    {
                        (Player.ChosenPiece as MonoBehaviour).transform.position =
                                (this as MonoBehaviour).transform.position;
                        Player.ChosenPiece.inDungeon = false;
                    }                  
                }

                else
                {
                    if (Player.ChosenPiece.colour == Colours.yellow)
                        Player.HoldingYellowPiece = false;

                    if (Player.ChosenPiece.colour == Colours.blue)
                        Player.HoldingBluePiece = false;

                    if (Player.ChosenPiece.colour == Colours.red)
                        Player.HoldingRedPiece = false;

                    if (colour == Colours.white && !Player.ChosenPiece.OnMirror)
                        Player.ChosenPiece.OnMirror = true;

                    (Player.ChosenPiece as MonoBehaviour).transform.position =
                                this.transform.position;

                }
               
                empty = false;

                PieceOnTile = Player.ChosenPiece;
            }
            
            else
            {
                Debug.Log("occupied piece");

                (Player.ChosenPiece as MonoBehaviour).transform.position =
                            (this as MonoBehaviour).transform.position;

                Player.ChosenPiece.Fight(this.PieceOnTile);

                PieceOnTile = Player.ChosenPiece;
            }

            Player.HoldingPiece = false;
            
        }
        
        public IGhostBase PieceOnTile{ get; set; }
    }
}
