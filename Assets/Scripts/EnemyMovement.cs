using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;

    Rigidbody2D enemyBody;

    private void Awake() {
        enemyBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyBody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }
    
    private void FlipEnemy() {
            transform.localScale = new Vector2(-(Mathf.Sign(enemyBody.velocity.x)), 1f);
    }
}
