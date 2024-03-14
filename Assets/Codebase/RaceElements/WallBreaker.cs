using Assets.Codebase.Mechanics.Controller;
using UnityEngine;

namespace Assets.Codebase.RaceElements
{
    [RequireComponent(typeof(Collider))]
    public class WallBreaker : MonoBehaviour
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
                playerWheel.EscapeWall();
                Debug.Log("Wall passed!");
            }
        }
    }
}