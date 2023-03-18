using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    // Max health for each player
    public const int MAX_HEALTH = 100;

    // 1 for player 1 and 2 for player 2
    public int playerID;
    // Keybinds
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode jump;
    public KeyCode special;
    // 1 or -1
    public int direction = 1;

    // Position the player is spawned at
    public Vector2 startPosition;
    // The light to be spawned on the player
    public GameObject playerLight;

    public void SaveKeybinds()
    {
        PlayerPrefs.SetInt(playerID.ToString() + "moveRight", (int)moveRight);
        PlayerPrefs.SetInt(playerID.ToString() + "moveLeft", (int)moveLeft);
        PlayerPrefs.SetInt(playerID.ToString() + "jump", (int)jump);
        PlayerPrefs.SetInt(playerID.ToString() + "special", (int)special);
    }

    public void LoadKeybinds()
    {
        moveRight = (KeyCode)PlayerPrefs.GetInt(playerID.ToString() + "moveRight");
        moveLeft = (KeyCode)PlayerPrefs.GetInt(playerID.ToString() + "moveLeft");
        jump = (KeyCode)PlayerPrefs.GetInt(playerID.ToString() + "jump");
        special = (KeyCode)PlayerPrefs.GetInt(playerID.ToString() + "special");
    }
}

