using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButton : MonoBehaviour
{
    public string sceneName;
    public Button button;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    void Update()
    {

    }
}
