using Assets.Codebase.Mechanics.Controller;
using UnityEngine;

namespace Assets.Codebase.Mechanics.Units
{
    [RequireComponent(typeof(Collider))]
    public class HumanUnit : MonoBehaviour
    {
        private int WavingAnimationKey = Animator.StringToHash("isWaving");

        [SerializeField] private Animator _humanAnimator;
        [SerializeField] private bool _isConnected;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            if (_isConnected)
            {
                _collider.enabled = false;
                _humanAnimator.SetBool(WavingAnimationKey, false);
            }
            else
            {
                _humanAnimator.SetBool(WavingAnimationKey, true);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player))
            {
                _collider.enabled = false;
                _isConnected = true;
                _humanAnimator.SetBool(WavingAnimationKey, false);
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