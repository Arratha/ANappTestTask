using UnityEngine;
using UnityEngine.UI;

using ANappTestTask.UI;
using ANappTestTask.Levels.Models;


namespace ANappTestTask.Levels.Cards
{
    [RequireComponent(typeof(Button))]
    public class LevelCard : MonoBehaviour
    {
        [SerializeField] private GameObject _starsHolder;
        private SpriteSwapToggle[] _starsSpriteSwiper;

        private bool _isLocked;

        private bool _isInitialized;

        private int _setID;
        private int _levelID;

        public void Initialize(int setID, int levelID, LevelInfo info)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _setID = setID;
            _levelID = levelID;

            _starsSpriteSwiper = new SpriteSwapToggle[_starsHolder.transform.childCount];
            for (int i = 0; i < _starsSpriteSwiper.Length; i++)
                _starsSpriteSwiper[i] = _starsHolder.transform.GetChild(i).GetComponent<SpriteSwapToggle>();

            UpdateCard(info);

            Button levelButton = GetComponent<Button>();
            levelButton.onClick.AddListener(OnLevelButtonClick);
        }

        public void UpdateCard(LevelInfo info)
        {
            _isLocked = info.IsLocked;

            _starsHolder.SetActive(!_isLocked);

            for (int i = 0; i < _starsSpriteSwiper.Length; i++)
                _starsSpriteSwiper[i].SetToggleActive(i < info.StarsCount);
        }

        private void OnLevelButtonClick()
        {
            if (_isLocked)
                return;

            LevelsController.OnStartLevel?.Invoke(_setID, _levelID);
        }
    }
}