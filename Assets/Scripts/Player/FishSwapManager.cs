using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class FishSwapManager : MonoBehaviour
    {
        //Events
        public event UnityAction OnEndLevelEvent; 
        
        [SerializeField] private FishManager breakingFish;

        [SerializeField] private FishManager attackingFish;
        
        [SerializeField] private Transform spawnPoint;


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
            
            attackingFish.OnEndPointReached += OnEndLevel;
        }
        private void OnAttackingCompleted()
        {
            breakingFish.ToggleControls(true);
            attackingFish.ToggleControls(false);
            FocusCamera(breakingFish.transform);
        }

        private void OnEndLevel()
        {
            breakingFish.ToggleControls(true);
            attackingFish.ToggleControls(false);
            FocusCamera(breakingFish.transform);
            
            breakingFish.transform.position = spawnPoint.position;
            attackingFish.transform.position = spawnPoint.position;
            
            OnEndLevelEvent?.Invoke();
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