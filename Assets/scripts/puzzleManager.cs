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

    //sounds
    private AudioSource audioPlayer;
    public AudioClip[] happy;
    public AudioClip[] humm;

    public bool keepPlaying = true;
    private void Start()
    {
        StartCoroutine(Humming());
    }
    private void Awake()
    {
        currentPoints = 0;
        timeron = true;
        endScreen.SetActive(false);
        GUI.SetActive(true);
        audioPlayer = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (currentPoints >= pointsToWin)
        {
            //Win puzzle game
            //reset puzzle
            AudioClip randomhappy = happy[Random.Range(0, happy.Length)];
            audioPlayer.PlayOneShot(randomhappy);
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
                endScreen.SetActive(true);
                endText.text = "You've made " + score + " cakes!";
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

    IEnumerator Humming()
    {
        while (keepPlaying)
        {
            AudioClip randomhumm = humm[Random.Range(0, humm.Length)];
            GetComponent<AudioSource>().PlayOneShot(randomhumm);
            Debug.Log("ChOO-ChOO");
            yield return new WaitForSeconds(10);
        }
    }
}
//1702