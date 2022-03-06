using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextScene());
        }
    }

    private IEnumerator LoadNextScene()
    {

        yield return new WaitForSecondsRealtime(1);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        currentScene++;
        if (currentScene >= SceneManager.sceneCountInBuildSettings)
        {
            currentScene = 0;
        }
        SceneManager.LoadScene(currentScene);
    }
}
