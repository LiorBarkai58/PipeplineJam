using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class FishManager : MonoBehaviour
    {
        [SerializeField] private CharacterMovement fishController;

        [SerializeField] private EndPointCheck endPointCheck;
        
        public event UnityAction OnEndPointReached
        {
            add => endPointCheck.OnEndPointReached += value;
            remove => endPointCheck.OnEndPointReached -= value;
        }

        public void ToggleControls(bool value) => fishController.ToggleInput(value);
        
    }
}