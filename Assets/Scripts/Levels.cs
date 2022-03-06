using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    int currentLevel = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene() {
        yield return new WaitForSecondsRealtime(1);
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }
}
