using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float climbSpeed = 5.0f;
    [SerializeField] private float jumpSpeed = 20.0f;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    
    private Vector2 moveInput;
    private Rigidbody2D playerBody;
    private Animator playerAnimator;
    private Collider2D playerCollider;
    private float startingGravity; 
    private bool isAlive = true;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
        startingGravity = playerBody.gravityScale;
    }

    void OnMove(InputValue inputValue)
    {
        if (isAlive) {
            moveInput = inputValue.Get<Vector2>();
        }
    }

    void OnFire(InputValue inputValue) {
        if (isAlive) {
            Instantiate(bullet, gun.position, bullet.transform.rotation);
        }
    }
    private void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void Run()
    {
        float movementDelta = moveInput.x * playerSpeed;
        Vector2 playerVelocity = new Vector2(movementDelta, playerBody.velocity.y);
        playerBody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerBody.velocity.x), 1f);
        }
    }

    private void ClimbLadder()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 playerClimb = new Vector2(playerBody.velocity.x, moveInput.y * climbSpeed );
            playerBody.gravityScale = 0f;
            playerBody.velocity = playerClimb;
            
            bool playerHasVeritcalSpeed = Mathf.Abs(playerBody.velocity.y) > Mathf.Epsilon;
            playerAnimator.SetBool("isClimbing", playerHasVeritcalSpeed);
        } else {
            playerBody.gravityScale = startingGravity;
            playerAnimator.SetBool("isClimbing", false);
        }
    }

    void OnJump(InputValue jumpValue) 
    {
        if (isAlive && jumpValue.isPressed && playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Hazard") && isAlive) {
            Die();
        }
    }

    private void Die() {
        isAlive = false;
        playerBody.velocity += new Vector2(0f, jumpSpeed);
        playerBody.rotation = 45f;
        playerBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        playerAnimator.SetTrigger("Dead");
        gameObject.layer = LayerMask.NameToLayer("Dead");
    }


}
