﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public GameBoundary gameBoundary;
    public LaunchBase launchBase;
    public Earth earth;
    public string sceneName;
    public string nextScene;
    public bool guide;


    // Parent UI
    public GameObject planeGame;
    public GameObject planeScore;

    // Game UI
    public Text shipsDisplay;
    public Text levelDisplay;

    // Score UI
    public Button nextButton;
    public TextMeshProUGUI shipsCountDisplay;

    // components
    private CameraFollow cameraFollow;

    private bool loadScene;
    private int ships = 0;


    public int Ships
    {
        get => ships;
        set
        {
            ships = value;
            if (shipsDisplay != null)
                shipsDisplay.text = $"Ships: {ships}";

            if (shipsCountDisplay != null)
                shipsCountDisplay.text = ships.ToString();
        }
    }

    void Start()
    {
        Ships = 0;
        levelDisplay.text = sceneName;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        if (!guide) {
            // start game
            StartGuide();
            earth.StartBlink(1.5f);
            Invoke(nameof(StartGame), 1.5f);
        }
    }
    
    public void StartGuide()
    {
        launchBase.enabled = false;
        planeGame.transform.localScale = Vector3.zero;
        cameraFollow.Follow<Earth>();
    }

    public void StartGame()
    {
        launchBase.enabled = true;
        planeGame.transform.localScale = new Vector3(1, 1);
        cameraFollow.Follow<SpaceShip>();
    }

    public void FinishGame()
    {
        planeGame.transform.localScale = Vector3.zero;
        planeScore.transform.localScale = new Vector3(1, 1);

        nextButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(nextScene);
        });
    }
}