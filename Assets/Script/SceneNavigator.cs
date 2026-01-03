using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] private string startSceneName = "StartScene";
    [SerializeField] private string CraneGameName = "CraneGame";

    public void GoToStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(startSceneName);
    }

    public void GoToGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(CraneGameName);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
