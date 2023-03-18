using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class KeybindButtonInfo : MonoBehaviour
{
    // Stores the PlayerData that this keybind affects
    public PlayerData playerData;
    // Stores the text in the button
    public TextMeshProUGUI text;
    // Stores the keybind that the button effects
    public KeyBind keyBind;
}
