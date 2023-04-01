using UnityEngine;

using ANappTestTask.Levels.Cards;
using ANappTestTask.Levels.Models;


namespace ANappTestTask.Levels
{
    public class LevelsSetController : MonoBehaviour
    {
        [SerializeField] private LevelCard[] _levels;

        [Space(10)]
        [SerializeField] private SetCard _nextSet;

        private SetInfo _info;

        private bool _isInitialized;

        public void Initialize(int setID, SetInfo? info)
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            if (info == null)
                _info = new SetInfo(_levels.Length);
            else
                _info = (SetInfo)info;

            for (int i = 0; i < _levels.Length; i++)
                _levels[i].Initialize(setID, i, _info.Levels[i]);

            _nextSet.Initialize();

            Rescale();
        }

        private void Rescale()
        {
            Vector2 targetSize = transform.parent.parent.GetComponent<RectTransform>().rect.size;
            float scaleRatio = targetSize.y / GetComponent<RectTransform>().sizeDelta.y;

            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, targetSize.y);

            Transform levelsHolder = transform.GetChild(0);
            for (int i = 0; i < levelsHolder.childCount; i++)
            {
                RectTransform tempRT = levelsHolder.GetChild(i).GetComponent<RectTransform>();
                tempRT.anchoredPosition = new Vector2(tempRT.anchoredPosition.x, tempRT.anchoredPosition.y * scaleRatio);
            }
        }

        public void UpdateSet(int levelID, int starsEarned)
        {
            _info.Levels[levelID].StarsCount = starsEarned;
            _info.LevelDone(levelID);

            for (int i = 0; i < _levels.Length; i++)
                _levels[i].UpdateCard(_info.Levels[i]);
        }

        public int GetStarsCount()
        {
            return _info.GetStarsCount();
        }

        public int GetStarsCount(int levelID)
        {
            return _info.Levels[levelID].StarsCount;
        }

        public SetInfo GetInfo()
        {
            return _info;
        }
    }
}