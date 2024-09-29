using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the display of text elements in the game, including the current player's turn and the scores of Noughts and Crosses.
/// This class updates the UI with the relevant information at the start of the game and when a winner is determined.
/// </summary>
public class MiddleText : MonoBehaviour
{
    /// <summary>
    /// The text component that displays the current game status, such as whose turn it is or who won.
    /// </summary>
    public Text currentText;

    /// <summary>
    /// The text component that displays the current score for Noughts.
    /// </summary>
    public Text noughts;

    /// <summary>
    /// The text component that displays the current score for Crosses.
    /// </summary>
    public Text crosses;

    /// <summary>
    /// Initializes the UI elements at the start of the game, displaying the current turn and scores.
    /// </summary>
    void Start()
    {
        // Set the current turn text based on the current game state.
        if (GameObject.Find("Store").GetComponent<Stores>().noughts_turn)
        {
            currentText.text = "Nought's Turn...";
        }
        else
        {
            currentText.text = "Cross's Turn...";
        }

        // Update the scores for Noughts and Crosses.
        noughts.text = "Noughts: " + GameObject.Find("Store").GetComponent<Stores>().nought_points.ToString();
        crosses.text = "Crosses: " + GameObject.Find("Store").GetComponent<Stores>().cross_points.ToString();
    }

    /// <summary>
    /// Updates the UI to display the winner of the game and the current scores.
    /// </summary>
    /// <param name="win">The message to display indicating the winner or the game's status.</param>
    public void Winner(string win)
    {
        // Update the scores for Noughts and Crosses.
        noughts.text = "Noughts: " + GameObject.Find("Store").GetComponent<Stores>().nought_points.ToString();
        crosses.text = "Crosses: " + GameObject.Find("Store").GetComponent<Stores>().cross_points.ToString();

        // Set the middle text to the winning message.
        currentText.text = win;
    }
}

