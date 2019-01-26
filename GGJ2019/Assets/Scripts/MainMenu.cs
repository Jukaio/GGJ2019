using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void Start(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
