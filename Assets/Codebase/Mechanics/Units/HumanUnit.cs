using Assets.Codebase.Data.Audio;
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
        [SerializeField] private SoundId _collectionSound;

        private Collider _collider;

        public SoundId CollectionSound => _collectionSound;

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
                SetConnected();
                player.AttachNewUnit(this);
            }
        }

        public void SetConnected()
        {
            _collider.enabled = false;
            _isConnected = true;
            _humanAnimator.SetBool(WavingAnimationKey, false);
        }

        public void DisableInteractions()
        {
            _collider.enabled = false;
            _isConnected = false;
        }
    }
}