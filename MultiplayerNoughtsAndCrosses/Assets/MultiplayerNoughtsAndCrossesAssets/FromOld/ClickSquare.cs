using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the behavior of a single square in a Tic-Tac-Toe game.
/// This script handles user interactions (mouse clicks) and updates
/// the game state accordingly.
/// </summary>
public class ClickSquare : MonoBehaviour
{
    // Renderer components for the "nought" and "cross" GameObjects
    private Renderer o_SpriteRenderer;
    private Renderer x_SpriteRenderer;

    /// <summary>
    /// Indicates which player owns this square. 
    /// 0: Unclaimed, 1: Player 1 (nought), -1: Player 2 (cross).
    /// </summary>
    public int possession_value = 0;

    /// <summary>
    /// Reference to the GameBoard object that manages the overall game state.
    /// </summary>
    public GameObject gameboard;

    /// <summary>
    /// Index of this square on the game board.
    /// </summary>
    public int Square;

    /// <summary>
    /// Indicates whether the game is over.
    /// </summary>
    public bool gameover;

    /// <summary>
    /// Initializes the square by finding and disabling the "nought" and "cross" sprites.
    /// </summary>
    void Start()
    {
        // Locate and disable the "nought" sprite
        GameObject noughts = gameObject.transform.Find("nought").gameObject;
        o_SpriteRenderer = noughts.GetComponent<SpriteRenderer>();
        o_SpriteRenderer.enabled = false;

        // Locate and disable the "cross" sprite
        GameObject crosses = gameObject.transform.Find("cross").gameObject;
        x_SpriteRenderer = crosses.GetComponent<SpriteRenderer>();
        x_SpriteRenderer.enabled = false;
    }

    /// <summary>
    /// Handles the logic when the square is clicked by the player.
    /// Updates the game state and switches turns if the game is not over.
    /// </summary>
    private void OnMouseDown()
    {
        // Check if the game is over
        gameover = gameboard.GetComponent<GameBoard>().gameOver;

        // Proceed if the square is unclaimed and the game is not over
        if (possession_value == 0 && !gameover)
        {
            // If it's Player 1's turn (noughts)
            if (gameboard.GetComponent<GameBoard>().currentTurnNoughts)
            {
                // Display the nought, update possession, and check for game end
                o_SpriteRenderer.enabled = true;
                x_SpriteRenderer.enabled = false;
                possession_value = 1;

                gameboard.GetComponent<GameBoard>().Check(Square);
                gameover = gameboard.GetComponent<GameBoard>().gameOver;

                // If the game continues, switch turns; otherwise, end the game
                if (!gameover)
                {
                    gameboard.GetComponent<GameBoard>().Switch();
                }
                else
                {
                    gameboard.GetComponent<GameBoard>().currentTurnNoughts = false;
                }
            }
            // If it's Player 2's turn (crosses)
            else
            {
                // Display the cross, update possession, and check for game end
                o_SpriteRenderer.enabled = false;
                x_SpriteRenderer.enabled = true;
                possession_value = -1;

                gameboard.GetComponent<GameBoard>().Check(Square);
                gameover = gameboard.GetComponent<GameBoard>().gameOver;

                // If the game continues, switch turns; otherwise, end the game
                if (!gameover)
                {
                    gameboard.GetComponent<GameBoard>().Switch();
                }
                else
                {
                    gameboard.GetComponent<GameBoard>().currentTurnNoughts = true;
                }
            }
        }
    }
}
