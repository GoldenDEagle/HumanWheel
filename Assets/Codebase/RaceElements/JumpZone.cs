using Assets.Codebase.Mechanics.Controller;
using UnityEngine;

namespace CodeBase.RaceElements
{
    [RequireComponent(typeof(Collider))]
    public class JumpZone : MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerWheel = other.GetComponent<PlayerController>();
            if (playerWheel)
            {
                _collider.enabled = false;
                playerWheel.DoJump();
            }
        }
    }
}