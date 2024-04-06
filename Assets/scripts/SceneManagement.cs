using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneManager.LoadScene("cutsceneScene");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
