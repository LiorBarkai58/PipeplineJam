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

        [SerializeField] private FishManager presidentFish;
        
        [SerializeField] private Transform spawnPoint;


        [SerializeField] private CinemachineCamera camera;
        
        private FishManager currentFish;

        
        private void OnEnable()
        {
            breakingFish.ToggleControls(true);
            attackingFish.ToggleControls(false);
            presidentFish.ToggleControls(false);
            FocusCamera(breakingFish.transform);
            
            breakingFish.OnEndPointReached += OnBreakingCompleted;
            attackingFish.OnEndPointReached += OnAttackingCompleted;
            presidentFish.OnEndPointReached += OnEndLevel;
            currentFish = breakingFish;
        }

        public void SwapFish()
        {
            if (currentFish == breakingFish)
                OnBreakingCompleted();
            else if (currentFish == attackingFish)
                OnAttackingCompleted();
        }

        public void OnBreakingCompleted()
        {
            breakingFish.ToggleControls(false);
            attackingFish.ToggleControls(true);
            FocusCamera(attackingFish.transform);
            
            
            currentFish = attackingFish;
        }
        private void OnAttackingCompleted()
        {
            attackingFish.ToggleControls(false);
            presidentFish.ToggleControls(true);
            FocusCamera(presidentFish.transform);
            
            currentFish = presidentFish;
        }

        private void OnEndLevel()
        {
            breakingFish.ToggleControls(true);
            attackingFish.ToggleControls(false);
            presidentFish.ToggleControls(false);
            FocusCamera(breakingFish.transform);
            
            breakingFish.transform.position = spawnPoint.position;
            attackingFish.transform.position = spawnPoint.position;
            presidentFish.transform.position = spawnPoint.position;
            
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