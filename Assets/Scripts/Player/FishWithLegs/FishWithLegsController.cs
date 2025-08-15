using UnityEngine;

namespace Player.FishWithLegs
{
    public class FishWithLegsController : CharacterMovement
    {
        [Header("Stomping references")]
        [SerializeField] private LegStomp legStompHandler;

        private bool _isStomping = false;
        protected override void Start()
        {
            base.Start();
            input.Action += StartStomp;
            legStompHandler.OnStomp += Stomped;
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
        }

        private void Stomped()
        {
            _isStomping = false;
            ToggleInput(true);
            rb.linearVelocityY = 5;

        }

        protected override void HandleGravity()
        {
            if (_isStomping)
            {
                rb.gravityScale = movementData.FallingGravityMultiplier * 7;
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