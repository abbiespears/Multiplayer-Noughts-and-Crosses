using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

using TMPro;


public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField TMPcreateInput, TMPjoinInput;

    public string gameSceneName;

    public string roomName;

    public GameObject Lobby;
    public GameObject Room;

    public Text RoomText;

    public bool success = false;

    public void Awake()
    {
        Lobby.SetActive(true);
        Room.SetActive(false);
    }

    public void CreateRoom()
    {
        roomName = TMPcreateInput.text;
        PhotonNetwork.CreateRoom(TMPcreateInput.text);
    }

    public void JoinRoom()
    {
        roomName = TMPjoinInput.text;
        PhotonNetwork.JoinRoom(TMPjoinInput.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Lobby.SetActive(false);
        Room.SetActive(true);
        RoomText.text = "Room Name: " + roomName;
    }

    public void StartOnlineGame()
    {
        
        PhotonNetwork.LoadLevel("OnlineGame");
    }

    public void LeaveRoomPressed()
    {
        PhotonNetwork.LeaveRoom();
    }


    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("Lobby");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    

}
