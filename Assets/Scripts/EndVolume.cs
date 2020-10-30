using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndVolume : MonoBehaviour
{
    [Range(0, 3)]
    public int nextLevel;
    public GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (nextLevel == 0)
            gameManager.EndGame(true);
        else
            gameManager.setLevel(nextLevel);
    }
}
