using CodeBase.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Letter
{
    public abstract class BaseGameLettersBehaviour : MonoBehaviour, IGameLettersBehaviour
    {
        protected List<SimpleLetterInfo> GeneratedLevelLetters = new();

        protected SimpleLetterInfo LastLetter => GeneratedLevelLetters[LettersLeft - 1];

        public abstract int LettersLeft { get; }
        public abstract float LastKeyPositionX { get; }

        public abstract void CreateLevel(List<SimpleLetterInfo> simpleLetters);
        public abstract void NextLetter();

        public KeyCode LastKey => LastLetter.Key;

        public bool IsLastLetter(KeyCode key, bool isShifted)
            => LastLetter.Key == key && LastLetter.IsShifted == isShifted;
    }
}