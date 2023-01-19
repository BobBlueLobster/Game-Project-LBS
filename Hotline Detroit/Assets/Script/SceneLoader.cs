using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator crossFade;
    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(LoadNextScene());
    }

    void Update()
    {
        
            
    }

    IEnumerator LoadNextScene()
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        Scene scene = SceneManager.GetActiveScene();
        int nextLevelBuildIndex = 1 + scene.buildIndex;
        SceneManager.LoadScene(nextLevelBuildIndex);
    }
}