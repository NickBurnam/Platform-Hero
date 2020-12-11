using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EndLevelDoor : MonoBehaviour
{
    public bool isOpen;
    public bool inRange = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public Text interactText;

    [Range(0, 4)]
    public int nextLevel;
    public GameManager gameManager;
    public PlayerController playerController;
    

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // End Level screen
            //

        }
        if (inRange && !isOpen)
        {
            // Display UI text: "Press E to Traverse"
            //
            interactText.text = "Press E to Traverse";
            // Check if key pressed
            //
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
        else
        {
            interactText.text = "";
        }
    }

    public void EnterDoor()
    {
        if (!isOpen)
        {
            isOpen = true;

            // Disable player movement
            //
            playerController.DisableInput();

            // Change level
            //
            if (nextLevel == 0)
                gameManager.EndGame(true);
            else
                gameManager.setLevel(nextLevel);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
