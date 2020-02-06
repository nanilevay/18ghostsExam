using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using _18ghostsExam;

public class BoardManager : MonoBehaviour
{
    public GameObject PlayerActionsPanel;

    public TextMeshProUGUI PlayerActionsTexts;

    public TextMeshProUGUI CurrentPlayerText;

    public Player CurrentPlayer;

    public Player PlayerOne;

    public Player PlayerTwo;

    public Pickable[] Pieces;

    public Pickable[] Ghosts;

    public PortalBase bluePortal;

    public PortalBase redPortal;

    public PortalBase yellowPortal;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayer = PlayerOne;
        CurrentPlayerText.text = CurrentPlayer.Name;
        bluePortal.CurrentRot = PortalDir.down;
        redPortal.CurrentRot = PortalDir.up;
        yellowPortal.CurrentRot = PortalDir.right;
    }

    void Update()
    {
        CurrentPlayerText.text = CurrentPlayer.Name;

        if (Input.GetKeyDown("space"))
        {
            bluePortal.CurrentRot = bluePortal.Rotate();

            redPortal.CurrentRot = redPortal.Rotate();

            yellowPortal.CurrentRot = yellowPortal.Rotate();
        }

        foreach (IGhostBase ghost in Ghosts)
       {
            if (ghost.GhostDied != null)
            { 
                
                if (ghost.GhostDied.colour == Colours.blue)
                    bluePortal.CurrentRot = bluePortal.Rotate();

                if (ghost.GhostDied.colour == Colours.red)
                    redPortal.CurrentRot = redPortal.Rotate();

                if (ghost.GhostDied.colour == Colours.yellow)
                    yellowPortal.CurrentRot = yellowPortal.Rotate();

                ghost.GhostDied = null;
            }
       }
    }
}
