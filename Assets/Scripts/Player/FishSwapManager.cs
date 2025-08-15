using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public class FishSwapManager : MonoBehaviour
    {
        [SerializeField] private CharacterMovement breakingFish;

        [SerializeField] private CharacterMovement attackingFish;

        [SerializeField] private CinemachineCamera camera;

        private void OnEnable()
        {
            breakingFish.ToggleInput(true);
            attackingFish.ToggleInput(false);
            FocusCamera(breakingFish.transform);
        }

        [ContextMenu("Check fish swapping")]
        public void OnBreakingCompleted()
        {
            breakingFish.ToggleInput(false);
            attackingFish.ToggleInput(true);
            FocusCamera(attackingFish.transform);
        }
        private void OnAttackingCompleted()
        {
            breakingFish.ToggleInput(true);
            attackingFish.ToggleInput(false);
        }

        private void FocusCamera(Transform target)
        {
            CameraTarget camTarget = new CameraTarget()
            {
                CustomLookAtTarget = false,
                TrackingTarget = target
            };
            camera.Target = camTarget;
        }
    }
}