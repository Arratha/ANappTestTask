using UnityEngine;
using UnityEngine.UI;


namespace ANappTestTask.Pages.PageScripts
{
    public class PageScript_Menu : PageScript
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _optionsButton;


        public override void Initialize()
        {
            if (_isInitialized)
                return;

            _playButton.onClick.AddListener(() => PagesController.OnOpenPage?.Invoke(PageType.Levels));
            _optionsButton.onClick.AddListener(() => PagesController.OnOpenPage?.Invoke(PageType.Options));

            base.Initialize();
        }
    }
}