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

        void Start()
        {
            ships = 0;
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
