using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 100;
    private AudioSource audioSource;
    GameSession gameSession;

    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioSource = FindObjectOfType<AudioSource>();
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            if (audioSource != null) {
                audioSource.Play();
            }
            gameSession.UpdateScore(value);
        }
    }
}
