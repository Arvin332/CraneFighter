using System.Collections;
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
        AudioManager.Instance.PlaySFX(SFXType.Click);
        StartCoroutine(HoldAnimation(0.5f));
        SceneManager.LoadScene(startSceneName);
    }

    public void GoToGame()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlaySFX(SFXType.Click);
        StartCoroutine(HoldAnimation(0.5f));
        SceneManager.LoadScene(CraneGameName);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlaySFX(SFXType.Click);
        StartCoroutine(HoldAnimation(0.5f));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        AudioManager.Instance.PlaySFX(SFXType.Click);
        StartCoroutine(HoldAnimation(0.5f));
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    IEnumerator HoldAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
