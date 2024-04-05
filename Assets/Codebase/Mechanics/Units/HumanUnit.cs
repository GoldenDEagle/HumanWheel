using Assets.Codebase.Mechanics.Controller;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Units
{
    [RequireComponent(typeof(Collider))]
    public class HumanUnit : MonoBehaviour
    {
        [SerializeField] private bool _isConnected;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();

            if ( _isConnected )
            {
                _collider.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                _collider.enabled = false;
                _isConnected = true;
                player.AttachNewUnit(this);
            }
        }

        public void DisableInteractions()
        {
            _collider.enabled = false;
            _isConnected = false;
        }
    }
}