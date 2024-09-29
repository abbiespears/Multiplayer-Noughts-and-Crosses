using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



/// <summary>
/// Represents the game board logic for a Tic-Tac-Toe game. Manages the state of the game, including determining the winner, switching turns, and handling game-over conditions.
/// </summary>
public class GameBoard : MonoBehaviour
{
    /// <summary>
    /// Reference to the first square in a row, column, or diagonal being evaluated.
    /// </summary>
    public GameObject First;

    /// <summary>
    /// Reference to the second square in a row, column, or diagonal being evaluated.
    /// </summary>
    public GameObject Second;

    /// <summary>
    /// Reference to the third square in a row, column, or diagonal being evaluated.
    /// </summary>
    public GameObject Third;

    /// <summary>
    /// UI screen that appears when the current game ends, offering the option to play again.
    /// </summary>
    public GameObject playAgainScreen;

    /// <summary>
    /// UI element that displays the winner of the game or the current player's turn.
    /// </summary>
    public GameObject ToFindWinner;

    /// <summary>
    /// Boolean value indicating whether the game is over.
    /// </summary>
    public bool gameOver;

    /// <summary>
    /// Static reference to the Stores component, which keeps track of scores and other data that must be stored between scene changes.
    /// </summary>
    public static Stores store;

    /// <summary>
    /// Boolean indicating the current player's turn. True if it's Nought's turn, false if it's Cross's turn.
    /// </summary>
    public bool currentTurnNoughts;

    /// <summary>
    /// Counter tracking the number of moves left before the game ends in a draw.
    /// </summary>
    public int movesLeft;

    /// <summary>
    /// UI Text element that displays the countdown timer for the current player's turn.
    /// </summary>
    public Text CountDown;

    /// <summary>
    /// UI Text element that displays a "hurry" message when the countdown timer is low.
    /// </summary>
    public Text HurryMessage;

    /// <summary>
    /// Initializes the game state at the start of the game.
    /// </summary>
    void Start()
    {
        // Find and store a reference to the Stores script component in the scene.
        store = GameObject.Find("Store").GetComponent<Stores>();

        // Set the current turn based on the initial state from the Stores component - the player who goes first will alternate between each games.
        currentTurnNoughts = store.noughts_turn;

        // Initialize game-over state as false (as game is not over) and ensure the play-again screen is not active.
        gameOver = false;
        playAgainScreen.SetActive(false);

        // Set the moves counter to 0.
        movesLeft = 0;
        HurryMessage.text = "";
        StartCoroutine(Countdown(currentTurnNoughts));
    }

    /// <summary>
    /// Coroutine that tracks the countdown for the current player's turn.
    /// </summary>
    /// <param name="current">A boolean indicating the current player's turn.</param>
    private IEnumerator Countdown(bool current)
    {
        // Initialize the timer with 10 seconds.
        int timer = 10;
        CountDown.text = "00:" + timer.ToString();

        // Continue the countdown as long as the player who's turn it is hasn't changed and the game is not over.
        while ((current == currentTurnNoughts) && (!gameOver))
        {
            if (timer == 0)
            {
                // If the timer reaches 0, end the game and declare the other player as the winner.
                gameOver = true;
                if (currentTurnNoughts)
                {
                    HurryMessage.text = "Noughts\ntook too long";
                    CrossesWin();
                }
                else
                {
                    HurryMessage.text = "Crosses\ntook too long";
                    NoughtsWin();
                }
                StopAllCoroutines();
            }

            if (timer == 3)
            {
                // Display a hurry message when the timer reaches 3 seconds.
                HurryMessage.text = "Hurry!!\nTime's Almost Up!!!";
            }

            // Wait for 1 second and decrement the timer.
            yield return new WaitForSeconds(1);
            timer--;
            CountDown.text = "00:0" + timer.ToString();
            yield return null;
        }
    }

    /// <summary>
    /// Switches the player's turn.
    /// </summary>
    public void Switch()
    {
        movesLeft++;
        if (movesLeft == 9)
        {
            NoWinner();
        }
        else
        {
            // Update the current turn and notify the players whose turn it is.
            if (currentTurnNoughts)
            {
                ToFindWinner.GetComponent<MiddleText>().Winner("Cross's Turn...");
                currentTurnNoughts = false;
            }
            else
            {
                ToFindWinner.GetComponent<MiddleText>().Winner("Nought's Turn...");
                currentTurnNoughts = true;
            }
        }
        // Starts a new countdown coroutine for next player's turn.
        StopAllCoroutines();
        HurryMessage.text = "";
        StartCoroutine(Countdown(currentTurnNoughts));
    }

    /// <summary>
    /// Handles the scenario where Noughts win the game.
    /// </summary>
    public void NoughtsWin()
    {
        // Activates the play again screen and changes it's colour to blue (as blue indicates nought's win).
        playAgainScreen.SetActive(true);
        playAgainScreen.GetComponent<Image>().color = new Color(0, 0, 1, 0.1f);

        // Update the score for Noughts and display the winning message.
        store.nought_points++;
        ToFindWinner.GetComponent<MiddleText>().Winner("Noughts Win !!");
    }

    /// <summary>
    /// Handles the scenario where Crosses win the game.
    /// </summary>
    public void CrossesWin()
    {
        // Activates the play again screen and changes it's colour to red (as red indicates cross's win).
        playAgainScreen.SetActive(true);
        playAgainScreen.GetComponent<Image>().color = new Color(1, 0, 0, 0.1f);

        // Update the score for Crosses and display the winning message.
        store.cross_points++;
        ToFindWinner.GetComponent<MiddleText>().Winner("Crosses Win !!");
    }

    /// <summary>
    /// Checks whether there is a winning combination based on the most recent move.
    /// </summary>
    /// <param name="whichSquare">The index of the square that was just played.</param>
    public void Check(int whichSquare)
    {
        // Check the row containing the played square.
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

        // Check the column containing the played square.
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

        // Check diagonals if the played square is part of one.
        if (whichSquare % 4 == 0)
        {
            CompareValues("TopLeft", "Middle", "BottomRight");
        }
        else if (whichSquare == 2 || whichSquare == 4 || whichSquare == 6)
        {
            CompareValues("TopRight", "Middle", "BottomLeft");
        }
    }

    /// <summary>
    /// Compares the values in three squares to determine if they form a winning combination.
    /// </summary>
    /// <param name="first">The name of the first square to compare.</param>
    /// <param name="second">The name of the second square to compare.</param>
    /// <param name="third">The name of the third square to compare.</param>
    public void CompareValues(string first, string second, string third)
    {
        // Retrieve the GameObjects and their associated possession values.
        First = GameObject.Find(first);
        int leftval = First.GetComponent<ClickSquare>().possession_value;

        Second = GameObject.Find(second);
        int Midval = Second.GetComponent<ClickSquare>().possession_value;

        Third = GameObject.Find(third);
        int rightval = Third.GetComponent<ClickSquare>().possession_value;

        // Calculate the sum of the possession values.
        int col = leftval + Midval + rightval;

        // Determine if the sum indicates a win for Noughts or Crosses.
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

    /// <summary>
    /// Handles the scenario where the game ends in a draw.
    /// </summary>
    private void NoWinner()
    {
        gameOver = true;
        ToFindWinner.GetComponent<MiddleText>().Winner("No One Won");

        // Display the play-again screen with a neutral color.
        playAgainScreen.SetActive(true);
        playAgainScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0.3f);
    }

    /// <summary>
    /// Handles the event when the play-again button is clicked, restarting the game.
    /// </summary>
    public void OnClick()
    {
        // Switch the turn so the player who did not go first in the current game will go first in the next one.
        store.SwitchTurn();

        // Reload the current scene to restart the game.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

