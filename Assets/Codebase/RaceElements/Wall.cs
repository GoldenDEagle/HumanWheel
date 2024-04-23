using Assets.Codebase.Mechanics.Controller;
using System.Collections;
using UnityEngine;

namespace Assets.Codebase.RaceElements
{
    [RequireComponent(typeof(BoxCollider))]
    public class Wall : MonoBehaviour
    {
        [SerializeField] private Transform _attachedUnitTransform;

        private Collider _collider;
        private bool _isOccupied = false;

        public bool IsOccupied => _isOccupied;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerWheel = other.GetComponent<PlayerController>();
            if (playerWheel)
            {
                Debug.Log("Climbing a wall...");
                _collider.enabled = false;
                _isOccupied = true;
                var unit = playerWheel.GrabAUnit();
                if (unit == null)
                {
                    Debug.Log("Not enough units to climb the wall!");
                    return;
                }

                unit.transform.SetParent(transform, true);
                Vector3 unitPosition = new Vector3(playerWheel.transform.position.x, _attachedUnitTransform.position.y + 1f, _attachedUnitTransform.position.z);
                unit.transform.SetPositionAndRotation(unitPosition, _attachedUnitTransform.transform.rotation);
                playerWheel.ReactToWallCollision();
            }
        }
    }
}