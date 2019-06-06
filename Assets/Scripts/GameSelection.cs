using UnityEngine.SceneManagement;

public static class GameSelection
{
    public static string SelectedLevel { get; set; }

    public static void StartLevel()
    {
        SceneManager.LoadScene(SelectedLevel);
    }
}