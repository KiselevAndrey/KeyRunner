using CodeBase.Settings;
using CodeBase.Utility.Extension;
using GD.MinMaxSlider;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Letter
{
    public class TrialGameLettersService : BaseGameLettersService
    {
        // 1 more, because it is necessary to start the character
        private readonly int _maxLettersCount = 11;

        [SerializeField, MinMaxSlider(-12, 0)] private Vector2 _path;

        private int _lettersLeft;

        public override int LettersLeft => _lettersLeft;

        public override float LastKeyPositionX => transform.position.x 
            + (LettersLeft == _maxLettersCount ? _path.x : _path.y);

        public override void CreateLevel(List<SimpleLetterInfo> simpleLetters)
        {
            _lettersLeft = _maxLettersCount;
            GeneratedLevelLetters.Clear();

            for (int i = 1; i <= _maxLettersCount; i++)
                GeneratedLevelLetters.Add(simpleLetters.Ind(i));
        }

        public override void NextLetter() 
            => _lettersLeft--;
    }
}