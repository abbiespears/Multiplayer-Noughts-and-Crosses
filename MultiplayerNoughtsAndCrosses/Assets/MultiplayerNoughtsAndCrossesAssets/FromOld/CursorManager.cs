using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the appearance and behavior of the cursor in the game.
/// This class allows for setting a custom cursor texture and positioning the cursor hotspot correctly.
/// </summary>
public class CursorManager : MonoBehaviour
{
    /// <summary>
    /// The custom texture to be used for the cursor.
    /// </summary>
    [SerializeField] private Texture2D cursor;

    /// <summary>
    /// The position of the cursor's hotspot, which defines the point within the cursor texture that interacts with UI elements or objects in the game.
    /// </summary>
    private Vector2 cursorHotspot;

    /// <summary>
    /// Initializes the cursor with the specified texture and sets the hotspot position.
    /// </summary>
    void Start()
    {
        // Calculate the hotspot position, typically set to the center of the cursor texture.
        cursorHotspot = new Vector2(cursor.width / 2, cursor.height / 2);

        // Set the cursor to use the custom texture with the specified hotspot and cursor mode.
        Cursor.SetCursor(cursor, cursorHotspot, CursorMode.Auto);
    }
}
