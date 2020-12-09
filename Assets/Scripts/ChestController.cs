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
    public Bow bow;
    public GameObject ammoUI;
    public GameObject unlockPopupUI;

    private void Awake()
    {
        bow = FindObjectOfType<Bow>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            unlockPopupUI.SetActive(false);
        }
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
            //dDebug.Log("Player opened chest");
            bow.unlockBow();
            ammoUI.SetActive(true);
            unlockPopupUI.SetActive(true);
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
