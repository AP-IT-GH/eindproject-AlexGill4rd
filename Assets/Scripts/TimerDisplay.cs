using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    public GameObject endScreenPanel;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    private int aiScore;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();

        if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager not found. Display UI will not update.");
            return;
        }

        GameManager.instance.onTimerUpdated.AddListener(UpdateTimerText);
        GameManager.instance.onRoundEnded.AddListener(ShowEndScreen);
        endScreenPanel.SetActive(false); // Ensure the end screen is hidden at start
    }

    private void UpdateTimerText()
    {
        float roundTime = GameManager.instance.GetRoundTime();
        int minutes = Mathf.FloorToInt(roundTime / 60);
        int seconds = Mathf.FloorToInt(roundTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ShowEndScreen()
    {
        endScreenPanel.SetActive(true);
        playerScoreText.text = "Player Score: " + GameManager.instance.GetScore();
        aiScore = CalculateAIScore(); // Replace with actual AI score calculation
        aiScoreText.text = "AI Score: " + aiScore;
    }

    private int CalculateAIScore()
    {
        // Replace this with your actual AI score calculation logic
        return Random.Range(0, 200); // Example AI score calculation
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed
        if (GameManager.instance != null)
        {
            GameManager.instance.onTimerUpdated.RemoveListener(UpdateTimerText);
            GameManager.instance.onRoundEnded.RemoveListener(ShowEndScreen);
        }
    }
}
