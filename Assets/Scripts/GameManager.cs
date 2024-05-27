using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int score = 0;

    public UnityEvent onScoreUpdated = new UnityEvent();

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

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
        onScoreUpdated.Invoke();
    }

    public int GetScore()
    {
        return score;
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
