using UnityEngine;
using UnityEngine.UI;

using TMPro;

using ANappTestTask.UI;


namespace ANappTestTask.Levels.Cards
{
    [RequireComponent(typeof(Button))]
    public class SetCard : MonoBehaviour
    {
        [SerializeField] private int _starsRequired;

        [Space(10)]
        [SerializeField] private SpriteSwapToggle _imageSpriteSwiper;
        [SerializeField] private GameObject _starsCounter;

        private TextMeshProUGUI _counter;

        private bool _isLocked;

        private bool _isInitialized;

        public void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _counter = _starsCounter.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            LevelsController.OnUpdateStarsCount += UpdateCard;

            Button setButton = GetComponent<Button>();
            setButton.onClick.AddListener(OnSetButtonClick);
        }

        private void UpdateCard(int starsCount)
        {
            _isLocked = starsCount < _starsRequired;

            _starsCounter.SetActive(_isLocked);
            _imageSpriteSwiper.SetToggleActive(!_isLocked);

            _counter.text = $"{starsCount}/{_starsRequired}";
        }

        private void OnSetButtonClick()
        {
            if (_isLocked)
                return;

            LevelsController.OnChangeSet?.Invoke(1);
        }

        private void OnDestroy()
        {
            LevelsController.OnUpdateStarsCount -= UpdateCard;
        }
    }
}