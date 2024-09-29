using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Unity.VisualScripting;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using static UnityEngine.ParticleSystem;
using Photon.Chat;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon.StructWrapping;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    public GameObject NoughtsPlayerPrefab;
    public GameObject CrossesPlayerPrefab;
    public GameObject GridPrefabs;

    public GameObject NoughtsPlayer;
    public GameObject CrossesPlayer;

    public static int ready = 0;

    public GameObject StartPanel;
    public GameObject PlayAgainPanel;
    public GameObject PlayerLeftPanel;

    public Text PlayAgainText;
    public Text MiddleText;
    public Text PlayText;
    
    public bool isNoughts;
    public bool currentTurnNoughts;
    public bool firstToStart;

    public int movesLeft = 0;
    public bool gameOver = false;

    public GameObject First;
    public GameObject Second;
    public GameObject Third;


    public int NoughtsScore;
    public int CrossesScore;
    public Text NoughtsScoreTracker;
    public Text CrossesScoreTracker;

    public Text WhichPlayerNoughts;
    public Text WhichPlayerCrosses;

    public GameObject MenuButton;
    public GameObject WinnerPanel;
    public Text WinnerPanelText;

    void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(NoughtsPlayerPrefab.name, new Vector2(-6, 0), Quaternion.identity);
            
        }
        else
        {
            PhotonNetwork.Instantiate(CrossesPlayerPrefab.name, new Vector2(6, 0), Quaternion.identity);
        }
    }

    private void Start()
    {
        NoughtsScore = 0;
        CrossesScore = 0;
        movesLeft = 0;
        currentTurnNoughts = true;
        firstToStart = false;
        NoughtsPlayer = GameObject.Find("Noughts Player(Clone)");

        if (NoughtsPlayer.IsUnityNull())
        {
            isNoughts = false;

            WhichPlayerCrosses.text = "You are playing as\nCrosses";
            WhichPlayerNoughts.text = string.Empty;
            
            PlayText.text = "You are\nCrosses";
        }
        else
        {
            isNoughts = true;
            PlayText.text = "You are\nNoughts";
            WhichPlayerCrosses.text = string.Empty;
            WhichPlayerNoughts.text = "You are playing as\nNoughts";
        }

    }

    public void OnPlayButton()
    {

        StartPanel.SetActive(false);

        CheckIfReady();


    }

    public void CheckIfReady()
    {
        photonView.RPC("IncrementPlayersReady", RpcTarget.All);
        if (ready == 2)
        {
            
            photonView.RPC("StartGame", RpcTarget.All);
        }
        else
        {
            MiddleText.text = "Waiting for other player...";
        }

    }

    [PunRPC]
    void IncrementPlayersReady()
    {
        ready++;
    }


    [PunRPC]
    void StartGame()
    {
        ready = 0;
        gameOver = false;
        movesLeft = 0;
        firstToStart = !firstToStart;
        currentTurnNoughts = firstToStart;

            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.InstantiateRoomObject(GridPrefabs.name, new Vector2(0, 0), Quaternion.identity);
            }
            if (isNoughts == currentTurnNoughts)
            {
                MiddleText.text = "Your Turn...";
            }
            else
            {
                MiddleText.text = "Opponent's Turn...";
            }
        }


    [PunRPC]
    void Switch() {
        if (movesLeft == 9)
        {
            gameOver = true;
            photonView.RPC("NoWinner", RpcTarget.All);  
        }
        else {
            currentTurnNoughts = !currentTurnNoughts;
            if (isNoughts && currentTurnNoughts)
            {
                MiddleText.text = "Your Turn...";
                
            }
            else if (!isNoughts && !currentTurnNoughts)
            {
                MiddleText.text = "Your Turn...";
                
            }
            else
            {
                MiddleText.text = "Opponent's Turn...";
                
            }
        } }



    public void Check(int whichSquare)
    {
        movesLeft += 1;


        if (!gameOver)
        {
            if (whichSquare < 3)
            {
                CompareValues("TopLeft", "TopMid", "TopRight");
            }
            else if (whichSquare < 6)
            {
                CompareValues("MidLeft", "Middle", "MidRight");
            }
            else if (whichSquare < 9)
            {
                CompareValues("BottomLeft", "BottomMid", "BottomRight");
            }
        }

        if (!gameOver)
        {
            if (whichSquare % 3 == 0)
            {
                CompareValues("TopLeft", "MidLeft", "BottomLeft");
            }
            else if (whichSquare % 3 == 1)
            {
                CompareValues("TopMid", "Middle", "BottomMid");
            }
            else
            {
                CompareValues("TopRight", "MidRight", "BottomRight");
            }
        }
        if (!gameOver)
        {
            if (whichSquare % 4 == 0)
            {
                CompareValues("TopLeft", "Middle", "BottomRight");
            }
            else if (whichSquare == 2 || whichSquare == 4 || whichSquare == 6)
            {
                CompareValues("TopRight", "Middle", "BottomLeft");
            }
        }

        if ((!gameOver) && (currentTurnNoughts == isNoughts))
        {
            photonView.RPC("Switch", RpcTarget.All);
        }
    }





    public void CompareValues(string first, string second, string third)
    {

        First = GameObject.Find(first);
        int leftval = First.GetComponent<ClickSquareOnline>().possession_value;

        Second = GameObject.Find(second);
        int Midval = Second.GetComponent<ClickSquareOnline>().possession_value;

        Third = GameObject.Find(third);
        int rightval = Third.GetComponent<ClickSquareOnline>().possession_value;

  
        int col = leftval + Midval + rightval;
        Debug.Log(col);
        
        if (col == -3)
        {
            gameOver = true;

            CrossesWin();
        }
        else if (col == 3)
        {
            gameOver = true;
            

            NoughtsWin();
        }
        
    }


    
    void CrossesWin()
    {
        Debug.LogError("Crosses win called" + CrossesScore);
        CrossesScore++;
        
        CrossesScoreTracker.text = "Crosses: " + CrossesScore;

        if (!isNoughts)
        {
            Debug.LogError("You Win (You are Crosses)");
            PlayAgainPanel.SetActive(true);
            PlayAgainText.text = "You Won";
            
        }
        else
        {
            Debug.LogError("You Lose (You are Noughts)");
            PlayAgainPanel.SetActive(true);
            PlayAgainText.text = "You Lost";
        }
    }

    
    void NoughtsWin()
    {
        Debug.LogError("Noughts Win called " + NoughtsScore);
        NoughtsScore++;
        
        NoughtsScoreTracker.text = "Noughts: " + NoughtsScore;
        
        if (isNoughts)
        {
            Debug.LogError("You Win (You are Noughts)");
            PlayAgainPanel.SetActive(true);
            PlayAgainText.text = "You Won";
        }
        else
        {
            Debug.LogError("You Lose (You are Crosses)");
            PlayAgainPanel.SetActive(true);
            PlayAgainText.text = "You Lost";
        }
    }

    [PunRPC]
    public void NoWinner()
    {
        Debug.LogError("No One Wins");
        PlayAgainPanel.SetActive(true);
        PlayAgainText.text = "No One Won";
    }

    public void onPlayAgain()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(GameObject.Find("Grid Prefabs(Clone)"));
        }

        PlayAgainPanel.SetActive(false);

        CheckIfReady();
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        MenuButton.SetActive(false);
        PhotonNetwork.Destroy(GameObject.Find("Grid Prefabs(Clone)"));
        StartPanel.SetActive(false);
        PlayAgainPanel.SetActive(false);
        WinnerPanel.SetActive(true);

        MiddleText.text = "";
        if (CrossesScore == NoughtsScore)
        {
            WinnerPanelText.text = "The overall result was a tie...";
        }
        else if (CrossesScore > NoughtsScore)
        {
            WinnerPanelText.text = "The overall winner was Crosses!!!";
        }
        else
        {
            WinnerPanelText.text = "The overall winner was Noughts!!!";
        }

        PhotonNetwork.LeaveRoom();
    }

    public void LeaveTheRoomFirstStep()
    {
        PhotonNetwork.LeaveRoom();
        MenuButton.SetActive(false);
        MiddleText.text = "";
        StartPanel.SetActive(false);
        PlayAgainPanel.SetActive(false);

        WinnerPanel.SetActive(true);
        if (CrossesScore == NoughtsScore)
        {
            WinnerPanelText.text = "The overall result\n was a tie...";
        }
        else if (CrossesScore > NoughtsScore)
        {
            WinnerPanelText.text = "The overall winner\n was Crosses!!!";
        }
        else
        {
            WinnerPanelText.text = "The overall winner\n was Noughts!!!";
        }
        
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}


