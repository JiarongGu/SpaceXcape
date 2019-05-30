using UnityEngine.SceneManagement;

public static class GameSelection
{
    public static bool DisplayScore { get; set; }

    public static string NextLevel { get; set; }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}