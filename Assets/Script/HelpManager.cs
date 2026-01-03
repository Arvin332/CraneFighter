using UnityEngine;

public class HelpManager : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;

    private bool isOpen = false;

    public void OpenHelp()
    {
        helpPanel.SetActive(true);
        isOpen = true;
    }

    public void CloseHelp()
    {
        helpPanel.SetActive(false);
        isOpen = false;
    }

    public void ToggleHelp()
    {
        isOpen = !isOpen;
        helpPanel.SetActive(isOpen);
    }
}
