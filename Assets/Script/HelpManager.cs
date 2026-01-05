using System.Collections;
using UnityEngine;

public class HelpManager : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;

    private bool isOpen = false;

    public void OpenHelp()
    {
        AudioManager.Instance.PlaySFX(SFXType.Click);
        StartCoroutine(HoldAnimation(0.5f));
        helpPanel.SetActive(true);
        isOpen = true;
    }

    public void CloseHelp()
    {
        AudioManager.Instance.PlaySFX(SFXType.Click);
        StartCoroutine(HoldAnimation(0.5f));
        helpPanel.SetActive(false);
        isOpen = false;
    }

    public void ToggleHelp()
    {
        isOpen = !isOpen;
        helpPanel.SetActive(isOpen);
    }

    IEnumerator HoldAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
