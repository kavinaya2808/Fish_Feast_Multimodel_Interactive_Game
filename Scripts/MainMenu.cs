using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject startCanvas;  // Assign the start UI canvas here
    public GameObject gameplayRoot; // Assign the gameplay root object here

    public void StartGame()
    {
        // Enable gameplay elements
        gameplayRoot.SetActive(true);

        // Disable the start menu UI
        startCanvas.SetActive(false);

        Debug.Log("🎮 Game Started");
    }

    public void QuitGame() {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
