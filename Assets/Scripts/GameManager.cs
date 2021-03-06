﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 3f;
    int score = 0;
    public Text scoreText;
    public GameObject winMessage;

    private void Update()
    {
        scoreText.text = score.ToString();
    }
    public void AddScore(int amount)
    {
        score += amount;
        //Debug.Log("Score = " + score);
    }

    public void EndGame(bool isWin)
    {
        if(gameHasEnded == false)
        {
            if(isWin)
            {
                //Debug.Log("You Win! Final Score = " + score);
                // Win Message
                //
                if(winMessage != null)
                    winMessage.SetActive(true);
                gameHasEnded = true;

                // Return to Main Menu
                Invoke("ReturnToMainMenu", restartDelay);
            }
            else
            {
                Debug.Log("You lose. Final Score = " + score);
                gameHasEnded = true;

                // Restart the game
                //
                Invoke("Restart", restartDelay);
            }
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void setLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
