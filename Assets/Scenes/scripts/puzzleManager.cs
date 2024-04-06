using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class puzzleManager : MonoBehaviour
{
    //current points
    private int currentPoints;
    //amount of points to win (same as puzzle pieces)
    private int pointsToWin = 3;

    private int score = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI endText;

    public GameObject GUI;
    public GameObject endScreen;

    //timer
    private float TimeLeft = 60.00f;
    private bool timeron;

    private void Awake()
    {
        currentPoints = 0;
        timeron = true;
    }
    void Update()
    {
        if (currentPoints >= pointsToWin)
        {
            //Win puzzle game
            //reset puzzle
            score++;
            BroadcastMessage("ResetPuzzle");
            currentPoints = 0;
            //Debug.Log(score);
            scoreText.text = "Score: " + score;

        }

        if (timeron)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                timeron = false;
                //end game!
                GUI.SetActive(false);
                endText.text = "You've made " + score + " cakes!";
                endScreen.SetActive(true);
            }
        }
    }

    //Add 1 point
    void AddPoint()
    {
        currentPoints++;
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
//1702