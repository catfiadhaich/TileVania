using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI LivesText;
    [SerializeField] TextMeshProUGUI ScoreText;

    ScenePersist scenePersist;
    int score = 0;
    void Awake()
    {
        scenePersist = FindObjectOfType<ScenePersist>();
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        LivesText.text = playerLives.ToString();
        ScoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath() {
        if (playerLives > 1) {
            TakeLife();
        } else {
            scenePersist.ResetPersistence();
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        LivesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession() {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void UpdateScore(int addToScore) {
        score += addToScore;
        ScoreText.text = score.ToString();
    }
}
