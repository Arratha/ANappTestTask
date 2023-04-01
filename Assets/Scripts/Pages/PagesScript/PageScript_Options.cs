using UnityEngine;
using UnityEngine.UI;

using ANappTestTask.Audio;
using ANappTestTask.UI;


namespace ANappTestTask.Pages.PageScripts
{
    public class PageScript_Options : PageScript
    {
        [SerializeField] private Button _closeButton;

        [Space(10)]
        [SerializeField] private Button _soundsButton;
        [SerializeField] private Button _musicButton;

        public override void Initialize()
        {
            if (_isInitialized)
                return;

            _closeButton.onClick.AddListener(() => { PagesController.OnOpenPage?.Invoke(PageType.Menu); });

            _soundsButton.onClick.AddListener(() => AudioController.OnSetSoundsState?.Invoke());
            _musicButton.onClick.AddListener(() => AudioController.OnSetMusicState?.Invoke());

            _soundsButton.GetComponentInChildren<SpriteSwapToggle>().SetToggleActive(AudioController.IsSoundsActive);
            AudioController.OnChangeSoundsState += _soundsButton.GetComponentInChildren<SpriteSwapToggle>().SetToggleActive;

            _musicButton.GetComponentInChildren<SpriteSwapToggle>().SetToggleActive(AudioController.IsMusicActive);
            AudioController.OnChangeMusicState += _musicButton.GetComponentInChildren<SpriteSwapToggle>().SetToggleActive;

            base.Initialize();
        }
    }
}