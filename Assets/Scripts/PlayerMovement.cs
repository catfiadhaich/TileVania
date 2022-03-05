using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10.0f;
    [SerializeField] private float jumpSpeed = 5f;
    Vector2 moveInput;
    Rigidbody2D playerBody;
    Animator playerAnimator;
    Collider2D playerCollider;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        Run();
        FlipSprite();
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

    void OnJump(InputValue jumpValue) 
    {
        if (jumpValue.isPressed && playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            playerBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

}
