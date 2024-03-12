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

        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerController playerWheel = other.GetComponent<PlayerController>();
            if (playerWheel)
            {
                _collider.enabled = false;
                var unit = playerWheel.GrabAUnit();
                if (unit == null)
                {
                    Debug.Log("No available units!");
                    return;
                }

                unit.transform.SetParent(transform, false);
                Vector3 unitPosition = new Vector3(playerWheel.transform.position.x, _attachedUnitTransform.position.y, _attachedUnitTransform.position.z);
                unit.transform.SetPositionAndRotation(unitPosition, _attachedUnitTransform.transform.rotation);
                playerWheel.ReactToWallCollision();

                Debug.Log("Wall contacted!");
            }
        }
    }
}