using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Evidence : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private GameObject playerObj;

    [SerializeField] private List<string> evidenceDeductions = new List<string>();

    [SerializeField] private GameObject options;
    [SerializeField] private Text optionText;
    [SerializeField] private GameObject investigateText;
    [SerializeField] private GameObject reinvestigateText;

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject objectCamera;

    [SerializeField] private GameObject particles;
    [SerializeField] private AudioSource interact;

    [SerializeField] private float correctValue;
    public float score;
    public bool evidenceFound;

    private bool playerInRange = false;
    private bool evidenceInvestigated = false;
    private bool investigatingObject;

    private float selectedOption = 0;
    private bool valueEntered;



    void Start()
    {
        interact.Stop();
    }

    void Update()
    {
        CheckEvidence();
        selectDeduction();
    }


    // Checks to see if the player has collided with any of the evidence pieces.
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") && player.checkingEvidence == false)
        {
            playerInRange = true;


            // Activates the investigate/ reinvestigate prompts.
            if (evidenceInvestigated == false)
            {
                investigateText.SetActive(true);
            }
            else
            {
                reinvestigateText.SetActive(true);
            }
        }
    }

    // Hides the prompt text if the player is not in range.
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            investigateText.SetActive(false);
            reinvestigateText.SetActive(false);
        }
    }


    private void CheckEvidence()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange == true)
        {

            evidenceFound = true;
            particles.SetActive(false);

            if (player.checkingEvidence == false)
            {
                interact.Play();

                // Switches from the main camera to the object camera.
                mainCamera.SetActive(false);
                objectCamera.SetActive(true);

                // Deactivates the player so he doesn't hide the object.
                playerObj.SetActive(false);

                // Ensures that the player is investigating the correct piece of evidence.
                player.SetCurrentEvidence(this);

                player.checkingEvidence = true;
                investigatingObject = true;


                // Checks and deactivates the relevant prompt text.
                // Activates the objects options menu.
                if (evidenceInvestigated == true)
                {
                    reinvestigateText.SetActive(false);
                    options.SetActive(true);
                    ShowOptions();
                } 
                else
                {
                    evidenceInvestigated = true;
                    investigateText.SetActive(false);
                    options.SetActive(true);
                    ShowOptions();
                } 
                
            }
            // Comes out of the options menu if the player is in it and pressed 'E'.
            else
            {
                // Switches back to the main camera and activates the player again.
                mainCamera.SetActive(true);
                objectCamera.SetActive(false);
                playerObj.SetActive(true);

                player.checkingEvidence = false;
                options.SetActive(false);

                // Reactivates the relevant prompt text.
                if (evidenceInvestigated == true)
                {
                    reinvestigateText.SetActive(true);
                }
                else
                {
                    investigateText.SetActive(true);
                }
            }
        }
    }


    public void ShowOptions()
    {
        options.SetActive(true);
        optionText.text = "";
        string showText = "";


        // Displays an option if one is selected or displays no selected option.
        if (selectedOption != 0)
        {
            showText = "Selected Option: " + selectedOption + "\n" + "Choose an option:" + "\n";
        }
        else
        {
            showText = "Selected Option: " + "No Option Chosen Yet." + "\n" + "Choose an option:" + "\n";
        }

        // Selects and displays the options for the item thats currently being intetacted with.
        for (int i = 0; i < evidenceDeductions.Count; i++)
        {
            showText += evidenceDeductions[i] + "\n";
        }

        optionText.text += showText + "\n";
    }


    // Checks and stores the players selected option.
    // Updates the score relative to the answer.
    public void selectDeduction()
    {

        if (player.checkingEvidence == true && player.GetCurrentEvidence() == this)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedOption = 1;


            } else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedOption = 2;

            } else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedOption = 3;
                
            }

            scoreUpdate();
        }  
    }


    // Checks to see if the player option is equal to the correct value and updates the score based on that.
    // Updates the game UI.
    public void scoreUpdate()
    {
        if (selectedOption == correctValue && valueEntered == false)
        {
            score = 1;
            valueEntered = true;

        } else if (selectedOption != correctValue && valueEntered == true)
        {
            score = 0;
            valueEntered = false;
        }

        gameUI.updateScore();
    }
}

