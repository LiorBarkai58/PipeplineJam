    using System;
    using DefaultNamespace.Audio;
    using Input;
using UnityEngine;
using Utilities;

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

        [SerializeField] private AudioClip jumpSound;
        [Header("Project References")] [SerializeField]
        protected InputReader input;

        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        private bool _isJumping = false;

        protected bool _inputEnabled = true;

        private bool _bufferJump = false;
        
        private CountdownTimer _jumpBufferTimer;
        protected virtual void Start()
        {
            _jumpBufferTimer = new CountdownTimer(movementData.JumpBufferTime);
            _jumpBufferTimer.OnTimerStop += () => _bufferJump = false;
            input.Jump += HandleJump;
            input.JumpReleased += ReleaseJump;
        }

        protected virtual void OnDestroy()
        {
            input.Jump -= HandleJump;
            input.JumpReleased -= ReleaseJump;
            
        }

        protected virtual void FixedUpdate()
        {
            if (_inputEnabled)
            {
                HandleMovement();
            }
            HandleGravity();
            _jumpBufferTimer?.Tick(Time.fixedDeltaTime);
            if(_bufferJump) HandleJump();
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
            Vector2 boxSize = new Vector2(collider.bounds.size.x, collider.bounds.size.y);

            boxSize.y *= 0.05f; 

            Vector2 checkPosition = new Vector2(collider.bounds.center.x, collider.bounds.min.y + boxSize.y * 0.5f);

            bool grounded = Physics2D.OverlapBox(checkPosition, new Vector2(boxSize.x, boxSize.y), 0f, movementData.GroundLayer);

            if (grounded) _isJumping = false;

            return grounded;
        }

        protected virtual void HandleMovement()
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
                SfxManager.Instance?.PlaySFX(jumpSound, 0.7f);
                rb.linearVelocityY = movementData.JumpPower;
                _isJumping = true;
                _bufferJump = false;
                if(!input.JumpHeld) ReleaseJump();
                
            }
            else
            {
                _jumpBufferTimer.Reset();
                _jumpBufferTimer.Start();
                _bufferJump = true;
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