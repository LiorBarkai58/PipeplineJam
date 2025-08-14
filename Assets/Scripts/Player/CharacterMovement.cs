using System;
using Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Data")] [SerializeField] private MovementData movementData;
        [Header("Prefab references")]
        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private Collider2D collider;

        [SerializeField] private Animator animator;

        [SerializeField] private SpriteRenderer sprite;

        [Header("Project References")] [SerializeField]
        private InputReader input;

        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        private bool _isJumping = false;
        private void Start()
        {
            input.Jump += HandleJump;
            input.JumpReleased += ReleaseJump;
        }

        private void FixedUpdate()
        {
            // Vector3 newPosition = transform.position + input.Direction * (speed * Time.fixedDeltaTime);
            // rb.MovePosition(newPosition);
            rb.linearVelocityX = input.Direction.x * movementData.Speed;
            animator.SetFloat(XVelocity, Mathf.Abs(rb.linearVelocityX) > 0 ? 1 : -1);
            sprite.flipX = rb.linearVelocityX > 0;
            HandleGravity();
        }

        private bool CheckGround()
        {
            Vector2 centerBottom = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
            bool grounded =Physics2D.OverlapCircle(centerBottom, movementData.GroundCheckRadius, movementData.GroundLayer);
            if (grounded) _isJumping = false;
            return grounded;
        }

        private void HandleGravity()
        {
            if (rb.linearVelocityY < 0) // Falling
            {
                rb.gravityScale = movementData.FallingGravityMultiplier;
            }
            else // Going up or standing
            {
                rb.gravityScale = movementData.NormalGravityMultiplier;
            }
        }

        private void HandleJump()
        {
            if (CheckGround() && rb.linearVelocityY == 0)
            {
                rb.linearVelocityY = movementData.JumpPower;
                _isJumping = true;
            }
        }
        
        public void ReleaseJump(){
            if(rb.linearVelocityY > movementData.MinJumpVelocity){
                rb.linearVelocityY = movementData.MinJumpVelocity;
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        #endif
    }
}