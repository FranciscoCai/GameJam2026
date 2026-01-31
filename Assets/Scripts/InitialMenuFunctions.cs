using UnityEngine;

public class InitialMenuFunctions : MonoBehaviour
{
    public GameObject[] startPanel;
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void OpenCredits(GameObject creditsPanel)
    {
        creditsPanel.SetActive(true);
        CloseStartPanel();
    }
    public void ClosePanels(GameObject panelToClose)
    {
        panelToClose.SetActive(false);
        foreach (GameObject panel in startPanel)
        {
            panel.SetActive(true);
        }
    }
    private void CloseStartPanel()
    {
        foreach (GameObject panel in startPanel)
        {
            panel.SetActive(false);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
