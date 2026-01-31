using UnityEngine;

public class InitialMenuFunctions : MonoBehaviour
{
    public GameObject[] startPanel;
    private GameObject panelOpened;
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.UI.Cancel.performed += ctx => OnCancel();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
    }

    private void OnDisable()
    {
        inputActions.UI.Disable();
    }

    private void OnDestroy()
    {
        inputActions.UI.Cancel.performed -= ctx => OnCancel();
        inputActions.Dispose();
    }

    private void OnCancel()
    {
        if (panelOpened != null)
        {
            ClosePanels();
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void ClosePanels()
    {
        panelOpened.SetActive(false);
        panelOpened = null;
        foreach (GameObject panel in startPanel)
        {
            panel.SetActive(true);
        }
    }
    public void OpenPanels(GameObject panelToOpen)
    {
        panelToOpen.SetActive(true);
        panelOpened = panelToOpen;
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
