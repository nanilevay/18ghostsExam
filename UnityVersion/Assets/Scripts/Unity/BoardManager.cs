using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BoardManager : MonoBehaviour
{
   // public //Board Board;

    public GameObject PlayerActionsPanel;

    public TextMeshProUGUI PlayerActionsTexts;

    public TextMeshProUGUI CurrentPlayerText;

    public Player CurrentPlayer;

    public Player PlayerOne;

    public Player PlayerTwo;

    public Pickable[] Pieces;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayer = PlayerOne;
        CurrentPlayerText.text = CurrentPlayer.Name;
    }

    void Update()
    {
        CurrentPlayerText.text = CurrentPlayer.Name;

       
    }
}
