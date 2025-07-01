using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{

    public void Quit_Game() {

        Debug.Log("Quit!");
        Application.Quit();

    }

    public void Main_Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public TMP_Text finalScoreText;

    void OnEnable()
    {
        if (finalScoreText != null)
        {
            int score = GameManager.Instance.GetFinalScore();
            finalScoreText.text = "Final Score: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("⚠️ finalScoreText or GameManager.Instance is null!");
        }
    }
}
