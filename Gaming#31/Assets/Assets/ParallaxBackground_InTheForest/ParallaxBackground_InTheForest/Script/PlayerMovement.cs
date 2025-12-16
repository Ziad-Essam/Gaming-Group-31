using UnityEngine;

namespace InTheForest
{
    public class PlayerMovement : MonoBehaviour
    {
        public Rigidbody2D body;
        public BoxCollider2D groundCheck;
        public LayerMask groundMask;

        public float acceleration;

        [Range(0f, 1f)]
        public float groundDecay;
        public float maxXSpeed;

        public float jumpSpeed;

        public bool grounded;
        float xInput;
        float yInput;

        // Update is called once per frame
        void Update()
        {
            CheckInput();
            HandleJump();
        }

        void FixedUpdate()
        {
            CheckGround();
            HandleXMovement();
            ApplyFriction();

        }

        private void HandleXMovement()
        {
            if (Mathf.Abs(xInput) > 0)
            {
                float increment = xInput * acceleration;
                float newSpeed = Mathf.Clamp(body.linearVelocity.x + increment, -maxXSpeed, maxXSpeed);
                body.linearVelocity = new Vector2(newSpeed, body.linearVelocity.y);

                FaceInput();
            }
        }

        private void FaceInput()
        {
            float direction = Mathf.Sign(xInput);
            transform.localScale = new Vector3(direction, 1, 1);
        }

        private void CheckInput()
        {
            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");
        }

        void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && grounded)
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpSpeed);
            }
        }

        void CheckGround()
        {
            grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        }

        void ApplyFriction()
        {
            if (grounded && xInput == 0 && body.linearVelocity.y <= 0)
            {
                body.linearVelocity *= groundDecay;
            }
        }

    }

}
