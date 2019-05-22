﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public int ships;
    public Text shipsDisplay;
    public Text levelDisplay;
    public GameBoundary gameBoundary;
    public Earth earth;

    public string scenesName;

    void Start()
    {
        ships = 0;
        levelDisplay.text = scenesName;
    }

    void Update()
    {
        if (shipsDisplay != null)
            shipsDisplay.text = $"Ships: {ships}";
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}