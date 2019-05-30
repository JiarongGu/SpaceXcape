using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public int ships;
    public GameBoundary gameBoundary;
    public LaunchBase launchBase;
    public Earth earth;
    public string scenesName;

    // Parent UI
    public GameObject planeGame;
    public GameObject planeScore;

    // Game UI
    public Text shipsDisplay;
    public Text levelDisplay;

    // Score UI
    public Button nextButton;

    // components
    public CameraFollow cameraFollow;

    private bool loadScene;

    void Start()
    {
        ships = 0;
        levelDisplay.text = scenesName;
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        // start game
        StartGuide();
        earth.StartBlink(1.5f);
        Invoke(nameof(StartGame), 1.5f);
    }

    void Update()
    {
        if (shipsDisplay != null)
            shipsDisplay.text = $"Ships: {ships}";

        if (GameSelection.DisplayScore)
        {
            planeGame.transform.localScale = Vector3.zero;
            planeScore.transform.localScale = new Vector3(1, 1);

            nextButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(GameSelection.NextLevel);
            });

            GameSelection.DisplayScore = false;
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

    public void LoadScene(string sceneName)
    {
        GameSelection.DisplayScore = true;
        GameSelection.NextLevel = sceneName;
    }
}