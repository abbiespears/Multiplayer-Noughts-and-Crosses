using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ClickSquareOnline : MonoBehaviourPun
{
    public int possession_value = 0;

    public GameObject gameboard;

    public int Square;

    private Renderer o_SpriteRenderer;
    private Renderer x_SpriteRenderer;

    private void Start()
    {
        gameboard = GameObject.Find("Grid");

        GameObject noughts = gameObject.transform.Find("nought").gameObject;
        o_SpriteRenderer = noughts.GetComponent<SpriteRenderer>();
        o_SpriteRenderer.enabled = false;

        GameObject crosses = gameObject.transform.Find("cross").gameObject;
        x_SpriteRenderer = crosses.GetComponent<SpriteRenderer>();
        x_SpriteRenderer.enabled = false;
    }

    private void OnMouseDown()
    {
        
        if (gameboard.GetComponent<OnlineGameManager>().currentTurnNoughts == gameboard.GetComponent<OnlineGameManager>().isNoughts)
        {
            if (possession_value == 0 && (!gameboard.GetComponent<OnlineGameManager>().gameOver)) {

                if (gameboard.GetComponent<OnlineGameManager>().currentTurnNoughts)
                {
                    photonView.RPC("ShowNoughts", RpcTarget.All);
                }
            
                else
                {
                    photonView.RPC("ShowCrosses", RpcTarget.All);
                }
            }
        } 
    }



    [PunRPC]
    void ShowNoughts()
    {
        
        o_SpriteRenderer.enabled = true;
        x_SpriteRenderer.enabled = false;
        possession_value = 1;
        
        gameboard.GetComponent<OnlineGameManager>().Check(Square);
    }

    [PunRPC]
    void ShowCrosses()
    {
        o_SpriteRenderer.enabled = false;
        x_SpriteRenderer.enabled = true;
        possession_value = -1;

        gameboard.GetComponent<OnlineGameManager>().Check(Square);
    }

}
