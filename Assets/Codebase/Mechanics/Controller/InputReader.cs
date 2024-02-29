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
            var direction = context.ReadValue<Vector2>();
            _playerController.SetDirection(direction);
        }
    }
}
