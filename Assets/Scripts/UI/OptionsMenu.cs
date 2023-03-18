using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu Instance;

    public KeybindMenu player1Keybind;
    public KeybindMenu player2Keybind;

    public bool isCheckingForKey = false;

    public KeyCode currentKeyCode;
    public TextMeshProUGUI currentText;

    void Awake()
    {
        // Singleton
        if (Instance != null) Destroy(gameObject);
        if (Instance == null) Instance = this;
    }

    private void Update()
    {

    }

}
