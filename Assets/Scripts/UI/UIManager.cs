using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    // Singleton
    public static UIManager Instance;
    // Stores health bars
    public RectTransform player1Health;
    public RectTransform player2Health;
    // Max width of health bar
    [HideInInspector]
    public float MAX_HEALTH_BAR_SIZE;
    // Stores blood particle effect (probably should move this somewhere else)
    public GameObject bloodEffect;

    public TextMeshProUGUI winnerText;
    public RectTransform deathMenu;

    void Awake()
    {
        // Singleton
        if (Instance != null) Destroy(gameObject);
        if (Instance == null) Instance = this;

        MAX_HEALTH_BAR_SIZE = player1Health.rect.width;
    }

    public void UpdateHealthBars(int playerID, int newHealth)
    {
        // Stores the health bar to be modified
        RectTransform currHealthBar = null;
        // Chooses based off of player ID
        if (playerID == 1) currHealthBar = player1Health;
        else if (playerID == 2) currHealthBar = player2Health;
        else Debug.LogError("Invalid PlayerID passed to UpdateHealthBars method");
        // Updates the size
        currHealthBar.sizeDelta = new Vector2(newHealth * MAX_HEALTH_BAR_SIZE / PlayerData.MAX_HEALTH, currHealthBar.rect.size.y);
        
    }

    public IEnumerator ShowDeathMenu(string winner)
    {
        winnerText.text = winner + " WON";
        int iterations =250;
        float startY = deathMenu.anchoredPosition.y;
        for (int i = 0; i<iterations; i++)
        {
            float newY = Mathf.Lerp(startY, 0, (float)i/iterations);
            deathMenu.anchoredPosition = new Vector2(deathMenu.anchoredPosition.x, newY);
            yield return null;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
