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

        private float               m_timeSinceAttack = 0.0f;
        private bool                m_rolling = false;
        private Rigidbody2D         m_body2d;
        private bool                m_isWallSliding = false;
        private float               m_rollCurrentTime;
        
        private float               m_rollDuration = 8.0f / 14.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Spacebar) && grounded)
        {
            Jump();
        }

        if (Input.GetKey(L))
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);

            if(GetComponent<SpriteRenderer>()!=null)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            
           
        }

         if (Input.GetKey(R))
        {
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().linearVelocity.y);
        
            if(GetComponent<SpriteRenderer>()!=null)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            
        }
            anim.SetFloat("Speed",Mathf.Abs(GetComponent<Rigidbody2D>().linearVelocity.x));
            anim.SetFloat("Height", GetComponent<Rigidbody2D>().linearVelocity.y);
            anim.SetBool("Grounded", grounded);




             m_timeSinceAttack += Time.deltaTime;

        // Increase timer that checks roll duration
        if(m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        // Disable rolling if timer extends duration
        if(m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

            //Attack
         if(Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            currentAttack++;

            // Loop back to one after third attack
            if (currentAttack > 3)
                currentAttack = 1;

            // Reset Attack combo if time since last attack is too large
            if (m_timeSinceAttack > 1.0f)
                currentAttack = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            anim.SetTrigger("Attack" + currentAttack);

            // Reset timer
            m_timeSinceAttack = 0.0f;
        }

        // Block
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            anim.SetTrigger("Block");
            anim.SetBool("IdleBlock", true);
        }

        else if (Input.GetMouseButtonUp(1))
            anim.SetBool("IdleBlock", false);


             // Roll
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding)
        {
            m_rolling = true;
            anim.SetTrigger("Roll");
            
            
        }
            


    }

    void Jump()
    {  

      GetComponent<Rigidbody2D>().linearVelocity = new Vector2(GetComponent<Rigidbody2D>().linearVelocity.x, jumpHeight) ;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
   
}
