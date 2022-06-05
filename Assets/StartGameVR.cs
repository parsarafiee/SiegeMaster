using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameVR : MonoBehaviour
{
    
    public void StartThrGame()
    {
        SceneManager.LoadScene("GameVRWithMap");
    }
    public void CloseTheGame()
    {
        Application.Quit();
    }
}
