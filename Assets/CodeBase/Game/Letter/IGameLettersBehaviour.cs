using CodeBase.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Letter
{
    public interface IGameLettersBehaviour
    {
        public float LastKeyPositionX { get; }
        public int LettersLeft { get; }
        public KeyCode LastKey { get; }

        public void CreateLevel(List<SimpleLetterInfo> simpleLetters);
        public bool IsLastLetter(KeyCode key, bool isShifted);
        public void NextLetter();
    }
}