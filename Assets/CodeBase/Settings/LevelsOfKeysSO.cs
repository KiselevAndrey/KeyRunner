using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Settings
{
    [CreateAssetMenu(fileName = nameof(LevelsOfKeysSO), menuName = nameof(CodeBase) + "/" + nameof(LevelsOfKeysSO))]
    public class LevelsOfKeysSO : ScriptableObject
    {
        private readonly LevelInfo _spaceInfo = new (KeyCode.Space, KeyCode.None, false);

        [field: SerializeField] private List<LevelInfo> Levels { get; set; }

        public int MaxLevel => Levels.Count - 1;

        public List<LevelInfo> GenerateKeys(int level)
        {
            level = ApprovedLevel(level);
            var infos = Levels.GetRange(0, level + 1);
            infos.Add(_spaceInfo);
            return infos;
        }

        public List<LevelInfo> GetNewKeys(int level)
        {
            level = ApprovedLevel(level);
            var infos = new List<LevelInfo>();
            infos.Add(Levels[level]);
            infos.Add(_spaceInfo);
            return infos;
        }

        public LevelInfo GetNewKeysInfo(int level)
        {
            level = ApprovedLevel(level);
            return Levels[level];
        }

        public int ApprovedLevel(int level) 
            => Mathf.Min(level, MaxLevel);
    }

    [System.Serializable]
    public class LevelInfo
    {
        [field: SerializeField] public KeyCode FirstKey { get; private set; }
        [field: SerializeField] public KeyCode SecondKey { get; private set; }
        [field: SerializeField] public bool IsShifted { get; private set; }

        public LevelInfo(KeyCode firstKeys, KeyCode secondKeys, bool isShifted)
        {
            FirstKey = firstKeys;
            SecondKey = secondKeys;
            IsShifted = isShifted;
        }
    }
}