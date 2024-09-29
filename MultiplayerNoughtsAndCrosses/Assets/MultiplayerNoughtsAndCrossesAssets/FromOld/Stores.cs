using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Manages the persistent game data across scenes, including the current player's turn and the score for Noughts and Crosses.
/// This class ensures that data such as scores and information pertaining to who's turn it is is not lost when the game scene is reloaded.
/// </summary>
public class Stores : MonoBehaviour
{
    /// <summary>
    /// Indicates whether it is Noughts' turn. True if it's Noughts' turn, false if it's Crosses' turn.
    /// </summary>
    public bool noughts_turn;

    /// <summary>
    /// Stores the current score for Noughts.
    /// </summary>
    public int nought_points;

    /// <summary>
    /// Stores the current score for Crosses.
    /// </summary>
    public int cross_points;

    /// <summary>
    /// Called when the script instance is being loaded. Ensures that this object is not destroyed when loading a new scene.
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    /// <summary>
    /// Initializes the game data at the start of the game.
    /// </summary>
    void Start()
    {
        // Initialize the scores to 0 and set the first turn to Noughts.
        noughts_turn = true;
        nought_points = 0;
        cross_points = 0;

    }

    /// <summary>
    /// Switches the turn between Noughts and Crosses.
    /// </summary>
    public void SwitchTurn()
    {
        // Toggles the noughts_turn boolean to switch turns.
        noughts_turn = !noughts_turn;
    }
}
