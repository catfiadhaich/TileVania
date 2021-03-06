using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 1.0f;
    private Rigidbody2D bulletBody;
    private PlayerMovement playerMovement;
    private GameSession gameSession;
    private float xSpeed;

    private void Awake() {
        bulletBody = GetComponent<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        xSpeed = playerMovement.transform.localScale.x * bulletSpeed;
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        bulletBody.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            Destroy(other.gameObject);
            gameSession.UpdateScore(50);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }
}
