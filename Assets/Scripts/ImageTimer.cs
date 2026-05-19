using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ImageTimer : MonoBehaviour
{
    public float timeLeft = 60f; // Timer in seconds
    public Image digit0, digit1, digit2, digit3; // Assign in Inspector
    public bool timerIsRunning = true;
    public GameObject gameover; 

    public Sprite[] numberSprites; // 0-9 digit sprites in order

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeLeft > 1)
            {
                timeLeft -= Time.deltaTime;
                UpdateImageTimer();
            }
            else
            {
                timeLeft = 0;
                timerIsRunning = false;

                // ✅ Store the score before showing the game over screen
                BigFishController bigFish = FindObjectOfType<BigFishController>();

                if (bigFish != null && GameManager.Instance != null)
                {
                    GameManager.Instance.SetFinalScore(bigFish.fishEatenCount);
                }
                else
                {
                    Debug.LogWarning("⚠️ Could not set final score. BigFish or GameManager missing.");
                }
                
                gameover.SetActive(true);
            }
        }
    }


    void UpdateImageTimer()
    {
        int timeInt = Mathf.FloorToInt(timeLeft);
        int minutes = timeInt / 60;
        int seconds = timeInt % 60;

        int m1 = minutes / 10;
        int m2 = minutes % 10;
        int s1 = seconds / 10;
        int s2 = seconds % 10;

        // Safety check
        if (numberSprites.Length < 10)
        {
            Debug.LogError("❌ numberSprites array must contain at least 10 sprites (0–9).");
            return;
        }

        digit0.sprite = numberSprites[m1];
        digit1.sprite = numberSprites[m2];
        digit2.sprite = numberSprites[s1];
        digit3.sprite = numberSprites[s2];
    }

    void EndLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("GameOverScene");
    }
}
