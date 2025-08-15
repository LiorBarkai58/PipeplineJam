using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "MovementData", menuName = "Player/MovementData")]
    public class MovementData : ScriptableObject
    {
        [SerializeField] private float speed;
        
        public float Speed => speed;

        [SerializeField] private float jumpPower;
        
        public float JumpPower => jumpPower;

        [SerializeField] private float minJumpVelocity = 2;
        
        public float MinJumpVelocity => minJumpVelocity;

        [SerializeField] private float normalGravityMultiplier = 1;
        
        public float NormalGravityMultiplier => normalGravityMultiplier;
        
        [SerializeField] private float fallingGravityMultiplier = 2;
        
        public float FallingGravityMultiplier => fallingGravityMultiplier;
        
        
        [Header("Ground Check Settings")]
        [SerializeField] private float groundCheckRadius = 0.1f;
        public float GroundCheckRadius => groundCheckRadius;
        [SerializeField] private LayerMask groundLayer;
        public LayerMask GroundLayer => groundLayer;
    }
}