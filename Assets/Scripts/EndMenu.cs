using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void MainScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -4);
    }
    public void QuitGme()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
