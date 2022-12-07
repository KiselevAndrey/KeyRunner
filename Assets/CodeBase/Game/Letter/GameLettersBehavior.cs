using CodeBase.Settings;
using CodeBase.Utility;
using CodeBase.Utility.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Letter
{
    [RequireComponent(typeof(LocalPool))]
    public class GameLettersBehavior : MonoBehaviour
    {
        private readonly int _maxLetters = 34;

        [SerializeField] private GameLetter _letterPrefab;
        [SerializeField, Min(0)] private float _spaceX = 0.1f;

        private float _xPos;

        private LocalPool _pool;
        private List<SimpleLetterInfo> _generatedLevelLetters = new();

        private SimpleLetterInfo LastLetter => _generatedLevelLetters[LettersLeft - 1];

        public int LettersLeft => _pool.SpawnedCount;
        public KeyCode LastKey => LastLetter.Key;

        public void CreateLevel(List<SimpleLetterInfo> simpleLetters)
        {
            _generatedLevelLetters.Clear();

            for (int i = 0; i < _maxLetters; i++)
                _generatedLevelLetters.Add(simpleLetters.Random());

            RespawnLetters();
        }

        public bool IsLastLetter(KeyCode key, bool isShifted)
            => LastLetter.Key == key && LastLetter.IsShifted == isShifted;

        public void NextLetter()
        {
            _pool.DespawnLast();
        }

        public void Hide()
        {
            _pool.DespawnAll();
        }

        private void Awake()
        {
            _pool = GetComponent<LocalPool>();
        }

        private void RespawnLetters()
        {
            _xPos = 0;
            var letterWidth = _letterPrefab.Width;

            _pool.DespawnAll();

            for (int i = 0; i < _generatedLevelLetters.Count; i++)
            {
                var letter = _pool.Spawn(_letterPrefab, new Vector3(_xPos, 0, 0));
                letter.Init(_generatedLevelLetters[i].Letter);

                _xPos -= letterWidth + _spaceX;
            }
        }
    }
}