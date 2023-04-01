using UnityEngine;
using UnityEngine.UI;

using ANappTestTask.Levels;


namespace ANappTestTask.Pages.PageScripts
{
    [RequireComponent(typeof(LevelsController))]
    public class PageScript_Levels : PageScript
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _previousSetButton;

        private LevelsController _levelController;

        public override void OpenPage()
        {
            base.OpenPage();

            _levelController.ResetField();
        }

        public override void OpenPageImmediately()
        {
            base.OpenPageImmediately();

            _levelController.ResetField();
        }

        public override void Initialize()
        {
            if (_isInitialized)
                return;

            _homeButton.onClick.AddListener(() => PagesController.OnOpenPage?.Invoke(PageType.Menu));
            _previousSetButton.onClick.AddListener(() => LevelsController.OnChangeSet?.Invoke(-1));

            _levelController = GetComponent<LevelsController>();
            _levelController.Initialize();

            base.Initialize();
        }
    }
}