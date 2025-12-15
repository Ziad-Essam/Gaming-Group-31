using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public KeyCode Spacebar;
    public KeyCode L;
    public KeyCode R;
    public KeyCode Block;
    public KeyCode Attack;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private bool grounded;
    private Animator anim;
    private int currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private bool m_rolling = false;
    private Rigidbody2D m_body2d; // You weren't using this variable, but I kept it
    private bool m_isWallSliding = false;
    private float m_rollCurrentTime;
    private float m_rollDuration = 8.0f / 14.0f;

    // --- NEW VARIABLE ---
    private bool isFacingRight = true; // Tracks which way we are looking

    void Start()
    {
        anim = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>(); // Initialized this just in case
    }

    void Update()
    {
        if (Input.GetKeyDown(Spacebar) && grounded)
        {
            Jump();
        }

        // --- MOVEMENT LEFT ---
        if (Input.GetKey(L))
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);

            // If we are looking right, FLIP to look left
            if (isFacingRight)
            {
                Flip();
            }
        }
        // --- MOVEMENT RIGHT ---
        else if (Input.GetKey(R))
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);

            // If we are looking left, FLIP to look right
            if (!isFacingRight)
            {
                Flip();
            }
        }

        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().linearVelocity.x));
        anim.SetFloat("Height", GetComponent<Rigidbody2D>().linearVelocity.y);
        anim.SetBool("Grounded", grounded);

        m_timeSinceAttack += Time.deltaTime;

        // Roll Logic
        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        // --- ATTACK ---
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            currentAttack++;
            if (currentAttack > 3) currentAttack = 1;
            if (m_timeSinceAttack > 1.0f) currentAttack = 1;

            anim.SetTrigger("Attack" + currentAttack);
            m_timeSinceAttack = 0.0f;
        }

        // --- BLOCK ---
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            anim.SetTrigger("Block");
            anim.SetBool("IdleBlock", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("IdleBlock", false);
        }

        // --- ROLL ---
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            anim.SetTrigger("Roll");
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, jumpHeight);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // --- NEW HELPER FUNCTION ---
    void Flip()
    {
        isFacingRight = !isFacingRight;
        // Rotate the player 180 degrees. This moves the Weapon Hitbox to the correct side!
        transform.Rotate(0f, 180f, 0f);
    }
}