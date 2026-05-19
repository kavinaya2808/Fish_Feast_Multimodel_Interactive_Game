using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int finalScore = 0;

    void Awake()
    {
        // Singleton pattern to keep this alive across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetFinalScore(int score)
    {
        finalScore = score;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }
}

