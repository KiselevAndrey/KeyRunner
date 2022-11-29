using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Keyboard
{
    [CreateAssetMenu(fileName = nameof(LanguageKeyLetersSO), menuName = nameof(CodeBase) + "/" + nameof(LanguageKeyLetersSO))]
    public class LanguageKeyLetersSO : ScriptableObject
    {
        [field:SerializeField] public List<KeyLeter> KeyLeters { get; private set; }
    }

    [System.Serializable]
    public class KeyLeter
    {
        [field:SerializeField] public KeyCode Key { get; private set; }
        [field:SerializeField] public string Leter { get; private set; }
    }
}