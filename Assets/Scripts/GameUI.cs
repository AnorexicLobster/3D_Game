using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Text scoreUpdate;
    [SerializeField] private Text evidenceFound;

    private float totalScore;
    private float evidenceCount;
    private float totalEvidence;

    void Start()
    {
        totalScore = 0;
        updateScore();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    // Updates and displays the score.
    public void updateScore()
    {
        Evidence[] allEvidence = FindObjectsOfType<Evidence>();

        totalScore = 0;
        evidenceCount = 0;
        totalEvidence = 0;

        foreach (Evidence evidence in allEvidence)
        {
            totalEvidence++;
            totalScore += evidence.score;

            if (evidence.evidenceFound == true)
            {
                evidenceCount++;
            }
        }

        scoreUpdate.text = "SCORE: " + totalScore + "/" + totalEvidence;
        evidenceFound.text = "EVIDENCE FOUND: " + evidenceCount + "/" + totalEvidence;
    }
}
