using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(LevelsOfKeysSO), menuName = nameof(CodeBase) + "/" + nameof(LevelsOfKeysSO))]
    public class LevelsOfKeysSO : ScriptableObject
    {
        private readonly KeyCode _spaceCode = KeyCode.Space;

        [field: SerializeField] public List<LevelInfo> Levels { get; private set; }
    }

    [System.Serializable]
    public class LevelInfo
    {
        [field: SerializeField] public KeyCode FirstKeys { get; private set; }
        [field: SerializeField] public KeyCode SecondKeys { get; private set; }
    }
}