using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUI : MonoBehaviour
{
    
    public void Play()
    {
        SceneManager.LoadScene("Floor1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
