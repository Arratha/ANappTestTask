using System;

namespace ANappTestTask.Levels.Models
{
    [Serializable]
    public struct LevelInfo
    {
        public bool IsLocked;
        public int StarsCount;

        public LevelInfo(bool isLocked)
        {
            IsLocked = isLocked;
            StarsCount = 0;
        }
    }

    [Serializable]
    public struct SetInfo
    {
        public LevelInfo[] Levels;

        public SetInfo(int levelsCount)
        {
            Levels = new LevelInfo[levelsCount];

            for (int i = 0; i < levelsCount; i++)
                Levels[i] = new LevelInfo(i != 0);
        }

        public void LevelDone(int levelID)
        {
            if (levelID + 1 < Levels.Length)
                Levels[levelID + 1].IsLocked = false;
        }

        public int GetStarsCount()
        {
            int result = 0;

            for (int i = 0; i < Levels.Length; i++)
                result += Levels[i].StarsCount;

            return result;
        }
    }
}