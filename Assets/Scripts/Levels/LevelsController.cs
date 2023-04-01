using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

using DG.Tweening;

using ANappTestTask.Pages;
using ANappTestTask.Levels.Models;

using Random = UnityEngine.Random;


namespace ANappTestTask.Levels
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private LevelsSetController[] _setControllers;

        [Space(10)]
        [SerializeField] private RectTransform _setHolder;

        public static Action<int, int> OnStartLevel;
        public static Action<int> OnUpdateStarsCount;
        public static Action<int> OnChangeSet;

        private int _currentSet;
        private float _setHeight;

        private const string SaveFileName = "/save.bin";

        private bool _isInitialized;

        public void Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            _currentSet = 0;

            RectTransform tempRT = _setHolder.GetChild(0).GetComponent<RectTransform>();
            _setHeight = tempRT.rect.size.y;

            SetInfo?[] tempInfo = GetInfo();
            for (int i = 0; i < _setControllers.Length; i++)
                _setControllers[i].Initialize(i, tempInfo[i]);

            UpdateStarsCount();

            OnStartLevel += FakePlayLevel;
            OnChangeSet += ChangeSet;
        }

        private void FakePlayLevel(int setID, int levelID)
        {
            _setControllers[setID].UpdateSet(levelID, Random.Range(_setControllers[setID].GetStarsCount(levelID), 4));
            UpdateStarsCount();

            SaveInfo();
        }

        private void UpdateStarsCount()
        {
            int starsCount = 0;
            for (int i = 0; i < _setControllers.Length; i++)
                starsCount += _setControllers[i].GetStarsCount();

            OnUpdateStarsCount?.Invoke(starsCount);
        }

        private SetInfo?[] GetInfo()
        {
            List<SetInfo?> result = null;

            if (File.Exists(Application.persistentDataPath + SaveFileName))
            {
                FileStream file = File.Open(Application.persistentDataPath + SaveFileName, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    result = (List<SetInfo?>)formatter.Deserialize(file);
                    file.Close();
                }
                catch
                {
                    file.Close();
                    File.Delete(Application.persistentDataPath + SaveFileName);
                }
            }

            if (result == null)
                result = new List<SetInfo?>();

            for (int i = result.Count; i < _setControllers.Length; i++)
                result.Add(null);

            return result.ToArray();
        }

        private void SaveInfo()
        {
            List<SetInfo?> infoToSave = new List<SetInfo?>();
            
            for (int i = 0; i < _setControllers.Length; i++)
                infoToSave.Add(_setControllers[i].GetInfo());

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + SaveFileName, FileMode.OpenOrCreate);
            formatter.Serialize(fileStream, infoToSave);
            fileStream.Close();
        }

        public void ResetField()
        {
            _currentSet = 0;
            _setHolder.anchoredPosition = Vector2.zero;
        }

        private void ChangeSet(int change)
        {
            if (_currentSet + change < 0 || _currentSet + change >= _setControllers.Length)
                return;

            PagesController.OnSetLock(true);

            _currentSet += change;
            Vector2 endValue = _setHolder.anchoredPosition + new Vector2(0, -1 * change * _setHeight);
            
            _setHolder.DOAnchorPos(endValue, 0.5f).OnComplete(() => PagesController.OnSetLock(false));
        }

        private void OnDestroy()
        {
            OnStartLevel = null;
            OnUpdateStarsCount = null;
            OnChangeSet = null;
        }
    }
}