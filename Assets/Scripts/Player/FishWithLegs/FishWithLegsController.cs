using UnityEngine;
using Utilities;

namespace Player.FishWithLegs
{
    public class FishWithLegsController : CharacterMovement
    {
        [Header("Stomping references")]
        [SerializeField] private LegStomp legStompHandler;

        [Header("Stomping data")] [SerializeField]
        private float stompingGravityMultiplier = 7;

        [SerializeField] private float stompingBounceStrength = 5;
        
        

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
            if(_isStomping && CheckGround()) Stomped();
        }

        private void StartStomp()
        {
            if (CheckGround() || _isStomping) return;
            ToggleInput(false);
            rb.linearVelocity = Vector2.zero;
            _isStomping = true;
            legStompHandler.gameObject.SetActive(true);
        }

        private void Stomped()
        {
            _isStomping = false;
            ToggleInput(true);
            rb.linearVelocityY = stompingBounceStrength;
            legStompHandler.gameObject.SetActive(false);
            

        }

        protected override void HandleGravity()
        {
            if (_isStomping)
            {
                rb.gravityScale = movementData.FallingGravityMultiplier * stompingGravityMultiplier;
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