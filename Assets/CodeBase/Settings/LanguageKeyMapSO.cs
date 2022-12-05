using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Settings
{
    [CreateAssetMenu(fileName = nameof(LanguageKeyMapSO), menuName = nameof(CodeBase) + "/" + nameof(LanguageKeyMapSO))]
    public class LanguageKeyMapSO : ScriptableObject
    {
        [field:SerializeField] public List<KeyLetter> KeyLeters { get; private set; }

        public LetterInfo GetInfo(KeyCode key)
            => KeyLeters.FirstOrDefault(k => k.Key == key).Info;

        public SimpleLetterInfo GetSimpleInfo(KeyCode key, bool isShifted)
        {
            var info = GetInfo(key);
            return new SimpleLetterInfo(key, isShifted ? info.ShiftLetter: info.Letter, isShifted);
        }
    }

    [System.Serializable]
    public class KeyLetter
    {
        [field:SerializeField] public KeyCode Key { get; private set; }
        [field:SerializeField] public LetterInfo Info { get; private set; }
    }

    [System.Serializable]
    public class LetterInfo
    {
        [field: SerializeField] public string Letter { get; private set; }
        [field: SerializeField] public string ShiftLetter { get; private set; }
        [field: SerializeField] public bool IsDouble { get; private set; }
    }

    public class SimpleLetterInfo
    {
        public KeyCode Key { get; private set; }
        public string Letter { get; private set; }
        public bool IsShifted{ get; private set; }

        public SimpleLetterInfo(KeyCode key, string letter, bool isShifted)
        {
            Key = key;
            Letter = letter;
            IsShifted = isShifted;
        }
    }
}