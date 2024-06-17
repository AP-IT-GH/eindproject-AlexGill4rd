using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0;
    private int aiScore = 0;
    public float roundDuration = 10f; // 1 minute round time
    private float roundTime = 10f; // 1 minute round time
    private bool isTiming = false;

    public UnityEvent onScoreUpdated = new();
    public UnityEvent onAiScoreUpdated = new();
    public UnityEvent onTimerUpdated = new();
    public UnityEvent onRoundEnded = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isTiming)
        {
            roundTime -= Time.deltaTime;
            onTimerUpdated.Invoke();

            if (roundTime <= 0)
            {
                roundTime = 0;
                isTiming = false;
                EndRound();
            }
        }
    }

    public void StartRound()
    {
        ResetTimer();
        StartTimer();
    }

    public void StartTimer()
    {
        isTiming = true;
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public void ResetTimer()
    {
        roundTime = roundDuration;
    }

    public float GetRoundTime()
    {
        return roundTime;
    }
    public void AddAiScore(int points)
    {
        aiScore += points;
        onAiScoreUpdated.Invoke();
    }

    public int GetAiScore()
    {
        return aiScore;
    }
    public void AddScore(int points)
    {
        score += points;
        onScoreUpdated.Invoke();
    }

    public int GetScore()
    {
        return score;
    }

    private void EndRound()
    {
        Debug.Log("Round ended!");
        onRoundEnded.Invoke();
        // Here you can implement additional logic for when the round ends
    }

    public void SaveScore()
    {
        Debug.Log("Score saved!");
    }

    public void LoadScore()
    {
        Debug.Log("Score loaded!");
    }
}
