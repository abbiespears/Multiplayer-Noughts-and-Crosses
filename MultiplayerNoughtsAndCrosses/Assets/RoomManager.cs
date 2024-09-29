using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private int playerCount;

    public Text TwoPlayers;
    public GameObject StartGame;


    public void Start()
    {
        StartGame.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
        TwoPlayers.text = "1/2 players in the room.";
        Debug.Log("You joined the room.");
        Debug.Log(PhotonNetwork.IsMasterClient);

        if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
        {
            UpdateButtons();
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Other players joined the room.");
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1 && PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount + "/2 Starting game...");

            UpdateButtons();
        }
    }

    private void UpdateButtons()
    {
        TwoPlayers.text = (PhotonNetwork.CurrentRoom.PlayerCount).ToString() + "/2 players in the room.";
        
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                StartGame.SetActive(true);
            }
            
        }
        
    }
}
