using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInput;
namespace Input
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
    public class InputReader : ScriptableObject, PlayerInput.IPlayerActions
    {
        public event UnityAction Jump = delegate { };
        public event UnityAction JumpReleased = delegate { };
        public Vector3 Direction => new Vector3(inputActions.Player.Move.ReadValue<Vector2>().x, 0, 0);
        
        private PlayerInput inputActions;
        
        void OnEnable() {
            if (inputActions == null) {
                inputActions = new PlayerInput();
                inputActions.Player.SetCallbacks(this);
            }
            EnablePlayerActions();
    
        }

        void OnDisable()
        {
            DisablePlayerActions();
        }

        public void EnablePlayerActions() {
            inputActions.Enable();
        }

        public void DisablePlayerActions() {
            inputActions.Disable();
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            //NOOP
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            //NOOP
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            //NOOP
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            //NOOP
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Jump?.Invoke();
            }
            else if (context.canceled)
            {
                JumpReleased?.Invoke();
                
            }
        }
    }
}