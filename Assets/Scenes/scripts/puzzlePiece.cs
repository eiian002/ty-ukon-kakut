using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class puzzlePiece : MonoBehaviour
{
    //is puzzle piece being dragged?
    private bool isDragged, placed;

    //offset, original puzzle piece position
    private Vector2 offset, originalPosition;

    //puzzlepiece slot
    [SerializeField] private GameObject puzzleSlot;

    //minigamemanager
    [SerializeField] private GameObject minigamemanager;

    //audioclip for piece placed
    [SerializeField] private AudioClip placedAudio;

    //audioclip for piece grabbed
    [SerializeField] private AudioClip grabbedAudio;

    //audiosource
    private AudioSource audioPlayer;

    private void Awake()
    {
        originalPosition = transform.position;
        //get audiosource
        audioPlayer = GetComponent<AudioSource>();
    }


    void Update()
    {
        //if puzzle piece not placed
        if (placed) return;
        //if puzzle piece is not dragged
        if (!isDragged) return;

        //get mouse position on screen using a function
        var mousePosition = GetMousePos();

        //transform puzzle piece position
        transform.position = mousePosition - offset;

    }

    //if mouse button is pressed/held down
    private void OnMouseDown()
    {
        isDragged = true;
        //play sound
        audioPlayer.PlayOneShot(grabbedAudio);

        offset = GetMousePos() - (Vector2)transform.position;

    }

    //returns puzzle piece to original position when mousebutton is not pressed
    private void OnMouseUp()
    {
        if (Vector2.Distance(transform.position, puzzleSlot.transform.position) < 3)
        {
            transform.position = puzzleSlot.transform.position;
            //play sound
            audioPlayer.PlayOneShot(placedAudio);
            placed = true;
            //tell minigamemanager the puzzle piece is on it's place
            minigamemanager.SendMessage("AddPoint");
        }
        else
        {
            //play sound effect (vittu perkele)
            transform.position = originalPosition;
            isDragged = false;
        }

    }

    //Function used to get mouse/cursor position on game world
    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void ResetPuzzle()
    {
        float x = Random.Range(-2, 15);
        float y = Random.Range(-2, 4);
        Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
        transform.position = randomPositionOnScreen;
        isDragged = false;
        placed = false;
    }

}

