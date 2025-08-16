using DefaultNamespace.Audio;
using UnityEngine;
using Utilities;

namespace Player.FishWithLegs
{
    public class FishWithLegsController : CharacterMovement
    {
        private static readonly int IsStomping = Animator.StringToHash("isStomping");
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");

        [Header("Stomping references")]
        [SerializeField] private LegStomp legStompHandler;

        [SerializeField] private AudioClip hopSound;
        [Header("Stomping data")] [SerializeField]
        private float stompingGravityMultiplier = 7;

        [SerializeField] private float stompingBounceStrength = 5;
        [SerializeField] private float initialStompJumpStrength = 3;
        

        private bool _isStomping = false;
        protected override void Start()
        {
            base.Start();
            input.Action += StartStomp;
            legStompHandler.OnStomp += Stomped;
            legStompHandler.gameObject.SetActive(false);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if(_isStomping && CheckGround() && rb.linearVelocityY <= 0) Stomped();
            if (_isStomping) legStompHandler.CanBreak = rb.linearVelocityY < 0;
            animator.SetFloat(YVelocity, rb.linearVelocityY);
        }

        private void StartStomp()
        {
            if (_isStomping || !_inputEnabled) return;
            rb.linearVelocity = new Vector2(0, initialStompJumpStrength);
            legStompHandler.CanBreak = rb.linearVelocityY < 0;
            _isStomping = true;
            SfxManager.Instance?.PlaySFX(hopSound, 0.7f);
            animator.SetBool(IsStomping, _isStomping);
            legStompHandler.gameObject.SetActive(true);
            Debug.Log("Start stomp");
        }

        protected override void HandleMovement()
        {
            if (_isStomping) return;
            base.HandleMovement();
        }

        private void Stomped()
        {
            Debug.Log("End stomp");
            
            _isStomping = false;
            animator.SetBool(IsStomping, _isStomping);
            rb.linearVelocityY = stompingBounceStrength;
            legStompHandler.gameObject.SetActive(false);
            

        }

        protected override void HandleGravity()
        {
            if (_isStomping )
            {
                if (rb.linearVelocityY < 0)
                {
                    rb.gravityScale = movementData.FallingGravityMultiplier * stompingGravityMultiplier;
                }
                else
                {
                    rb.gravityScale = movementData.FallingGravityMultiplier;
                }
            }
            else if (rb.linearVelocityY < 0) // Falling
            {
                rb.gravityScale = movementData.FallingGravityMultiplier;
            }
            else 
            {
                rb.gravityScale = movementData.NormalGravityMultiplier;
            }
        }
    }
}