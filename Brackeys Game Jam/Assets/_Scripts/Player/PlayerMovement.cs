using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float jumpForce = 16f;

    [Header("Dash")]
    public float dashForce = 20f;
    public float dashCooldown = 1f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("Jump Buffer & Coyote Time")]
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float lastDashTime;
    private float moveInput;

    private float lastGroundedTime;
    private float lastJumpInputTime;
    private int facingDirection = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput != 0)
        {
            facingDirection = (int)Mathf.Sign(moveInput);
            transform.localScale = new Vector3(facingDirection, 1, 1);
        }

        if (IsGrounded())
            lastGroundedTime = Time.time;

        if (Input.GetButtonDown("Jump"))
            lastJumpInputTime = Time.time;

        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
            StartCoroutine(Dash());

        if (!isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

            if (Time.time - lastJumpInputTime <= jumpBufferTime &&
                Time.time - lastGroundedTime <= coyoteTime)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                lastJumpInputTime = -1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        float dashDirection = Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(dashDirection * dashForce, 0f);

        yield return new WaitForSeconds(0.2f);
        isDashing = false;
    }
}
