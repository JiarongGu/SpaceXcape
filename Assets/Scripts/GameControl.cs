using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameControl : MonoBehaviour
    {
        public int ships;
        public Text shipsDisplay;
        public Text levelDisplay;

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
}
