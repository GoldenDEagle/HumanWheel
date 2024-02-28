using Assets.Codebase.Data.Audio;
using Assets.Codebase.Infrastructure.ServicesManagment;
using Assets.Codebase.Infrastructure.ServicesManagment.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Codebase.Utils.UI
{
    public class CustomButton : Button
    {
        [SerializeField] private bool _playSoundOnClick = false;
        [SerializeField] private SoundId _soundId;

        private IAudioService _audioService;

        protected override void Awake()
        {
            base.Awake();

            _audioService = ServiceLocator.Container.Single<IAudioService>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (_playSoundOnClick)
            {
                _audioService.PlaySfxSound(_soundId);
            }
        }
    }
}
