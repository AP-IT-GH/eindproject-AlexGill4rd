using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

        if (GameManager.instance == null)
        {
            Debug.LogWarning("GameManager not found. Score UI will not update.");
            return;
        }

        GameManager.instance.onScoreUpdated.AddListener(UpdateScoreText);

        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + GameManager.instance.GetScore();
        }
    }
}
