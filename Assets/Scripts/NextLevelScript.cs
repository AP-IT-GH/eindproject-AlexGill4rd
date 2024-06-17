using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public string nextLevel; // The name of the next level to load

    // Start is called before the first frame update
    void Start()
    {
        // Optional: Any initialization code
    }

    // Update is called once per frame
    void Update()
    {
        // Optional: Any update code
    }

    public void LoadNextLevel()
    {
        Debug.LogWarning("Clicked");
        if (!string.IsNullOrEmpty(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogWarning("Next level name is not set or empty.");
        }
    }
}
