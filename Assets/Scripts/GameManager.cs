using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;
    int score = 0;
    public Text scoreText;

    private void Update()
    {
        scoreText.text = score.ToString();
    }
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score = " + score);
    }

    public void EndGame()
    {
        if(gameHasEnded == false)
        {
            Debug.Log("Game Over");
            gameHasEnded = true;

            // Restart the game
            //
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
