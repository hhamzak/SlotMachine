using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.UnloadScene(index);
        SceneManager.LoadScene(index);
    }
}
