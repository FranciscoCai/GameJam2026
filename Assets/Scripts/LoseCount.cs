using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoseCount : MonoBehaviour
{
    [SerializeField] private float timeLeft = 90f; 
    [SerializeField] private TextMeshProUGUI timerText; 

    private bool isCounting = true;

    void Start()
    {
        UpdateTimerUI();
    }

    void Update()
    {
        if (!isCounting) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0f)
        {
            timeLeft = 0f;
            isCounting = false;
            UpdateTimerUI();
            SceneManager.LoadScene("LoseScene");
            return;
        }
        Debug.Log(timeLeft);
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60f);
            int seconds = Mathf.FloorToInt(timeLeft % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
