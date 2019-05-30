using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public int ships;
    public GameBoundary gameBoundary;
    public Earth earth;
    public string scenesName;

    // Parent UI
    public Plane planeGame;
    public Plane planeScore;

    // Game UI
    public Text shipsDisplay;
    public Text levelDisplay;

    // Score UI


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