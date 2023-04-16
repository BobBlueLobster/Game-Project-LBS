using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUI : MonoBehaviour
{
    public AudioSource audioSource;

    public void Play()
    {
        SceneManager.LoadScene("L1F1");
        audioSource.Play();

    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Quit");
        audioSource.Play();
    }
}
