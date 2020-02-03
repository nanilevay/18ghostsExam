using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class BoardManager : MonoBehaviour
{
    public Board Board;

    public GameObject PlayerActionsPanel;

    public TextMeshProUGUI PlayerActionsTexts;

    // Start is called before the first frame update
    void Start()
    {
        Board.pieceGrab += BoardAddPiece;
        // add piece move?
        Board.pieceDrop += BoardDropPiece;
    }

    public void BoardAddPiece(object sender, MapEventArgs eventSend)
    {
        /*
        Transform boardPanel = transform.Find("BoardPanel");

        foreach (Transform piece in boardPanel)
        {
            //Transform image = piece.GetChild(0).GetChild(0).GetComponent<Image>();

            // create this
            // ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

        if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = eventSend.Item.Image;

                //itemDragHandler.Item = e.Item;

                break;
            }
        }
        */
    }

    // change this one
    public void BoardDropPiece(object sender, MapEventArgs eventSend)
    {
        
    }

    public void ShowPanel(string text)
    {
        PlayerActionsPanel.SetActive(true);
        PlayerActionsTexts.text = text;
    }

    public void ClosePanel(string text)
    {
        PlayerActionsPanel.SetActive(false);
    }
}
