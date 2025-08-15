using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public class FishSwapManager : MonoBehaviour
    {
        [SerializeField] private FishManager breakingFish;

        [SerializeField] private FishManager attackingFish;


        [SerializeField] private CinemachineCamera camera;

        
        private void OnEnable()
        {
            breakingFish.ToggleControls(true);
            attackingFish.ToggleControls(false);
            FocusCamera(breakingFish.transform);
            
            breakingFish.OnEndPointReached += OnBreakingCompleted;
        }

        [ContextMenu("Check fish swapping")]
        public void OnBreakingCompleted()
        {
            breakingFish.ToggleControls(false);
            attackingFish.ToggleControls(true);
            FocusCamera(attackingFish.transform);
        }
        private void OnAttackingCompleted()
        {
            breakingFish.ToggleControls(true);
            attackingFish.ToggleControls(false);
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