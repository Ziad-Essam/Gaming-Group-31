using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 10f;
    


    [Header("Wall Mechanics")]
    public float wallSlideSpeed = 2f;               // How fast you slide down
    public Vector2 wallJumpForce = new Vector2(5f, 10f); // Power of wall jump (X, Y)

    [Header("Controls")]
    public KeyCode Spacebar = KeyCode.Space;
    public KeyCode L = KeyCode.A; 
    public KeyCode R = KeyCode.D;
    public KeyCode Block;                           // Assign in Inspector (e.g. Mouse 1)
    public KeyCode Attack;                          // Assign in Inspector (e.g. Mouse 0)

    [Header("Checks")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    
    public Transform wallCheck;                     // Drag your "WallCheck" object here
    public float wallCheckRadius = 0.3f;            // Radius slightly larger to keep contact

    // --- Private Variables ---
    private bool grounded;
    private bool isTouchingWall;
    private bool isWallSliding;
    private bool isFacingRight = true; 
    

    private Animator anim;
    private Rigidbody2D m_body2d;
    private SpriteRenderer sr;

    // --- Combat Variables ---
    private int currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private bool m_rolling = false;
    private float m_rollCurrentTime;
    private float m_rollDuration = 8.0f / 14.0f;

    void Start()
    {
        anim = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
        // 1. TIMERS

        m_timeSinceAttack += Time.deltaTime;
        
        if (m_rolling)
        {
            m_rollCurrentTime += Time.deltaTime;
            if (m_rollCurrentTime > m_rollDuration) m_rolling = false;
        }

        // 2. INPUT & LOGIC
        CheckInput();
        CheckWallSlide();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        // Physics Checks
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        if (wallCheck != null)
            isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsGround);
    }

    void CheckInput()
    {
        // --- JUMP ---
        if (Input.GetKeyDown(Spacebar))
        {
            if (grounded)
            {
                Jump(Vector2.up); // Normal Jump
            }
            else if (isWallSliding)
            {
              
                // Jump AWAY from the wall
                if (isFacingRight)
                    Jump(new Vector2(-wallJumpForce.x, wallJumpForce.y)); 
                else
                    Jump(new Vector2(wallJumpForce.x, wallJumpForce.y));  
                
                Flip(); // Turn around immediately
            }
        }

        // --- MOVEMENT ---
        // Condition: Move only if NOT rolling AND Wall Jump Timer is finished
        if (!m_rolling)
        {
            if (Input.GetKey(L))
            {
                m_body2d.linearVelocity = new Vector2(-moveSpeed, m_body2d.linearVelocity.y);
                
                // FIX: Only Flip if NOT currently sliding on a wall (prevents "air slide" bug)
                if (isFacingRight && !isWallSliding) Flip();
            }
            else if (Input.GetKey(R))
            {
                m_body2d.linearVelocity = new Vector2(moveSpeed, m_body2d.linearVelocity.y);
                
                // FIX: Only Flip if NOT currently sliding on a wall
                if (!isFacingRight && !isWallSliding) Flip();
            }
            else
            {
                m_body2d.linearVelocity = new Vector2(0, m_body2d.linearVelocity.y);
            }
        }

        // --- COMBAT ---
        if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            currentAttack++;
            if (currentAttack > 3) currentAttack = 1;
            if (m_timeSinceAttack > 1.0f) currentAttack = 1;
            anim.SetTrigger("Attack" + currentAttack);
            m_timeSinceAttack = 0.0f;
        }
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            anim.SetTrigger("Block");
            anim.SetBool("IdleBlock", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("IdleBlock", false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && !m_rolling && !isWallSliding)
        {
            m_rolling = true;
            anim.SetTrigger("Roll");
        }
    }

    void CheckWallSlide()
    {
        // 1. Check if player is trying to move AWAY from the wall
        bool isMovingAway = false;
        if(isFacingRight && m_body2d.linearVelocity.x < -0.1f) isMovingAway = true;
        if(!isFacingRight && m_body2d.linearVelocity.x > 0.1f) isMovingAway = true;

        // 2. Wall Slide Logic
        // We allow sliding if: Touching Wall + Not Grounded + Falling (< 0.1f) + NOT trying to escape
        if (isTouchingWall && !grounded && m_body2d.linearVelocity.y < 0.1f && !isMovingAway)
        {
            isWallSliding = true;

            // FIX: Apply a TINY push into the wall to keep sensors touching, 
            // but effectively remove friction so you don't get stuck.
            float tinyPush = isFacingRight ? 0.1f : -0.1f;
            m_body2d.linearVelocity = new Vector2(tinyPush, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }
    }

    void Jump(Vector2 dir)
    {
        // Reset Y velocity for consistent jump height
        m_body2d.linearVelocity = new Vector2(m_body2d.linearVelocity.x, 0); 
        
        if(dir == Vector2.up)
            m_body2d.AddForce(dir * jumpHeight * 50f); // Normal Jump
        else
            m_body2d.AddForce(dir * 50f); // Wall Jump (already scaled)
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f); // Rotates the GameObject
    }

    void UpdateAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(m_body2d.linearVelocity.x));
        anim.SetFloat("Height", m_body2d.linearVelocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("WallSlide", isWallSliding);
    }
    
    // --- ERROR FIX ---
    // Catches the "SlideDust" event from the animation so the game doesn't crash
    public void AE_SlideDust()
    {
        // Optional: Add particle effects here later
    }

    void OnDrawGizmos()
    {
        if (wallCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        }
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}