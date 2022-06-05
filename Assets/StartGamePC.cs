using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGamePC : MonoBehaviour
{
    
    public void StartThrGame()
    {
        SceneManager.LoadScene("GamePC");
    }
    public void CloseTheGame()
    {
        Application.Quit();
    }
}
