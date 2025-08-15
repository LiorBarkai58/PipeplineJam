    using System;
using Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Data")] [SerializeField] protected MovementData movementData;
        [Header("Prefab references")]
        [SerializeField] protected Rigidbody2D rb;

        [SerializeField] protected Collider2D collider;

        [SerializeField] protected Animator animator;

        [SerializeField] protected SpriteRenderer sprite;

        [Header("Project References")] [SerializeField]
        protected InputReader input;

        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        private bool _isJumping = false;

        private bool _inputEnabled = true;
        protected virtual void Start()
        {
            input.Jump += HandleJump;
            input.JumpReleased += ReleaseJump;
        }

        protected virtual void FixedUpdate()
        {
            if (_inputEnabled)
            {
                HandleMovement();
            }
            HandleGravity();
        }

        public void ToggleInput(bool value)
        {
            
            _inputEnabled = value;
            if (!value)
            {
                rb.linearVelocityX = 0;
            }
        }


        protected bool CheckGround()
        {
            Vector2 centerBottom = new Vector2(collider.bounds.center.x, collider.bounds.min.y);
            bool grounded =Physics2D.OverlapCircle(centerBottom, movementData.GroundCheckRadius, movementData.GroundLayer);
            if (grounded) _isJumping = false;
            return grounded;
        }

        private void HandleMovement()
        {
            rb.linearVelocityX = input.Direction.x * movementData.Speed;
            animator.SetFloat(XVelocity, Mathf.Abs(rb.linearVelocityX) > 0 ? 1 : -1);
            if (rb.linearVelocityX > 0) transform.localScale = new Vector3(-1, 1, 1);
            else if (rb.linearVelocityX < 0) transform.localScale = new Vector3(1, 1, 1);
        }

        protected virtual void HandleGravity()
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
            if (!_inputEnabled) return;
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