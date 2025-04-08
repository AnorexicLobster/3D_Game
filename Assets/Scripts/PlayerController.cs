using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform character;
    
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject ExitScene;

    [SerializeField] private GameObject Journal;
    [SerializeField] private GameObject JournalTxt;
    [SerializeField] private Timer Timer;
    
    public bool journalOpen;
    public bool checkingEvidence;
    public bool gameOver;

    private bool nearExit;
    private Evidence currentEvidence;



    void Start()
    {
        checkingEvidence = false;
        gameOver = false;
    }


    void Update()
    {
        // Hides the cursor while the player is playing the game.
        if (gameOver == false) 
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        
        gameOverActive();
        openJournal();
    }


    // Setter and Getter for the current evidence.
    public void SetCurrentEvidence(Evidence evidence)
    {
        currentEvidence = evidence;
    }

    public Evidence GetCurrentEvidence()
    {
        return currentEvidence;
    }

    // Checks to see if the player leaves the game.
    // Closes and opens all relevant UI.
    public void gameOverActive()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.X) && nearExit == true)
        {
            ExitScene.SetActive(false);
            GameOverScreen.SetActive(true);
            gameOver = true;

            // Enables the mouse again.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        } else if (Timer.EndGame == true)
        {
            GameOverScreen.SetActive(true);
            gameOver = true;

            // Enables the mouse again.
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // Checks to see if the player is near the exit.
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Exit"))
        {
           ExitScene.SetActive(true);
           nearExit = true;
        }
    }

    // Checks to see if the player is no longer near the exit.
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Exit"))
        {
            ExitScene.SetActive(false);
            nearExit = false;
        }
    }

    // Toggles between opening and closing the journal.
    public void openJournal()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.R))
        {
            if (journalOpen == false)
            {
                Journal.SetActive(true);
                JournalTxt.SetActive(false);
                journalOpen = true;
            }
            else
            {
                Journal.SetActive(false);
                JournalTxt.SetActive(true);
                journalOpen = false;
            }
        }
    }
}


