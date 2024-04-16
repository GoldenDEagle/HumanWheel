using Assets.Codebase.Mechanics.Controller;
using UnityEngine;

namespace Assets.Codebase.RaceElements
{
    [RequireComponent(typeof(Collider))]
    public class Gap : MonoBehaviour
    {
        [SerializeField] private Transform _attachedUnitTransform;

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
                var unit = playerWheel.GrabAUnit();
                if (unit == null)
                {
                    Debug.Log("Couldn't fill the gap! No units!");
                    return;
                }

                unit.transform.SetParent(transform, false);
                Vector3 unitPosition = new Vector3(playerWheel.transform.position.x, _attachedUnitTransform.position.y + 1f, _attachedUnitTransform.position.z);
                unit.transform.SetPositionAndRotation(unitPosition, _attachedUnitTransform.transform.rotation);
            }
        }
    }
}