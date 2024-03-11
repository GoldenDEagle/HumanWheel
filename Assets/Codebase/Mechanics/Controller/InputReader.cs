using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Codebase.Mechanics.Controller
{
    [RequireComponent(typeof(PlayerController))]
    public class InputReader : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        public void SetDirection(InputAction.CallbackContext context)
        {
            //var direction = context.ReadValue<Vector2>();
            //_playerController.SetDirection(direction);
        }

        public void OnTap(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    _playerController.SetTapStatus(true);
                    break;
                case InputActionPhase.Canceled:
                    _playerController.SetTapStatus(false);
                    break;
            }
        }
    }
}
