using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0;
    private int aiScore = 0;
    public float roundDuration = 120f;
    private float roundTime = 120f;
    private bool isTiming = false;

    public RoombaAgentE1 roombaAgentE1;

    public UnityEvent onScoreUpdated = new();
    public UnityEvent onAiScoreUpdated = new();
    public UnityEvent onTimerUpdated = new();
    public UnityEvent onRoundEnded = new();

    public bool isTesting = true;

    private void Awake()
    {
        roombaAgentE1.enabled = false;
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
        if (isTiming) return;

        roombaAgentE1.enabled = true;
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
    public bool IsRoundStarted()
    {
        return isTiming;
    }

    public int GetScore()
    {
        return score;
    }

    private void EndRound()
    {
        Debug.Log("Round ended!");
        roombaAgentE1.enabled = false;
        onRoundEnded.Invoke();

        aiScore = 0;
        score = 0;
        isTiming = false;
        roundTime = 0;
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
