using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeybindMenu : MonoBehaviour
{
    // Stores the playerData being manipulated
    private PlayerData currentPlayerData;

    // Stores reference to both playerdatas
    public PlayerData player1Data;
    public PlayerData player2Data;

    // UI text objects for player1
    public TextMeshProUGUI p1moveRightText;
    public TextMeshProUGUI p1moveLeftText;
    public TextMeshProUGUI p1jumpText;
    public TextMeshProUGUI p1specialText;

    // UI text objects for player2
    public TextMeshProUGUI p2moveRightText;
    public TextMeshProUGUI p2moveLeftText;
    public TextMeshProUGUI p2jumpText;
    public TextMeshProUGUI p2specialText;


    // The current keybind to be modified
    private KeyBind currentKeybind = KeyBind.NONE;

    private void UpdateButtonText()
    {
        p1moveRightText.text = player1Data.moveRight.ToString();
        p1moveLeftText.text = player1Data.moveLeft.ToString();
        p1jumpText.text = player1Data.jump.ToString();
        p1specialText.text = player1Data.special.ToString();

        p2moveRightText.text = player2Data.moveRight.ToString();
        p2moveLeftText.text = player2Data.moveLeft.ToString();
        p2jumpText.text = player2Data.jump.ToString();
        p2specialText.text = player2Data.special.ToString();
    }

    void Start()
    {
        UpdateButtonText();
    }

    private void Update()
    {
        // Looks for keys being pressed
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            // Only calls function if a keybind is being modified
            if (Input.GetKeyDown(kcode) && currentKeybind != KeyBind.NONE)
                SetKeyBind(kcode);
        }
    }
    public void KeybindButtonCallback(KeybindButtonInfo info)
    {
        ResetMenu();
        currentKeybind = info.keyBind;
        currentPlayerData = info.playerData;
        SetPressedText(info.text);
    }

    private void SetKeyBind(KeyCode pressedKey)
    {
        switch (currentKeybind)
        {
            case KeyBind.MOVE_LEFT:
                currentPlayerData.moveLeft = pressedKey;
                break;
            case KeyBind.MOVE_RIGHT:
                currentPlayerData.moveRight = pressedKey;
                break;
            case KeyBind.JUMP:
                currentPlayerData.jump = pressedKey;
                break;
            case KeyBind.SPECIAL:
                currentPlayerData.special = pressedKey;
                break;
            case KeyBind.NONE:
                Debug.LogError("Attempting to set keybind whiule currentKeybind is NONE");
                break;
        }
        currentPlayerData.SaveKeybinds();
        ResetMenu();
    }

    private void ResetMenu()
    {
        currentPlayerData = null;
        currentKeybind = KeyBind.NONE;
        UpdateButtonText();
    }

    private void SetPressedText(TextMeshProUGUI pressedText)
    {
        pressedText.text = "Press a button...";
    }
}

public enum KeyBind
{
    NONE,
    MOVE_RIGHT, 
    MOVE_LEFT,
    JUMP,
    SPECIAL
}
