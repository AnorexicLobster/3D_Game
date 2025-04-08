using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField] private Text timeLeft;
    [SerializeField] private float seconds;

    [SerializeField] private GameObject timer;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] PlayerController player;

    public bool EndGame;

    void Start()
    {
        timer.SetActive(true);
        EndGame = false;
    }
    // Update is called once per frame
    void Update()
    {
        updateTime();
    }


    // Decreases the time limit and ends the game when the timer hits 0.
    public void updateTime()
    {
        if (player.gameOver == false)
        {
            seconds -= Time.deltaTime;

            int displaySeconds = Mathf.FloorToInt(seconds);
            timeLeft.text = "Time Left: " + displaySeconds.ToString("00");

            if (seconds <= 0)
            {
                player.gameOver = true;
                timer.SetActive(false);
                EndGame = true;

            }
        } else if (player.gameOver == true)
        {
            timer.SetActive(false);
        }
    }
}
