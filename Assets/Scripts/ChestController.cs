using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    public bool inRange = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public Text interactText;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (inRange && !isOpen)
        {
            // Display UI text: "Press E to open"
            //
            interactText.text = "Press E to open";
            // Check if key pressed
            //
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                Debug.Log("Player opened chest");
            }
        }
        else
        {
            interactText.text = "";
        }
    }

    public void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
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
