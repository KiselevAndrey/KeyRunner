using CodeBase.Settings;
using CodeBase.Utility;
using CodeBase.Utility.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Letter
{
    [RequireComponent(typeof(LocalPool))]
    public class GameLettersBehaviour : MonoBehaviour
    {
        private readonly int _maxLettersCount = 34;
        private readonly int _maxCountSameLettersSequentially = 4;

        [SerializeField] private GameLetter _letterPrefab;
        [SerializeField, Min(0)] private float _spaceX = 0.1f;

        private float _xPos;

        private LocalPool _pool;
        private List<SimpleLetterInfo> _generatedLevelLetters = new();

        private SimpleLetterInfo LastLetter => _generatedLevelLetters[LettersLeft - 1];
        private float SpaceToNextLetter => _letterPrefab.Width + _spaceX;

        public int LettersLeft => _pool.SpawnedCount;
        public KeyCode LastKey => LastLetter.Key;
        public float LastKeyPositionX => _xPos + transform.position.x;

        #region Public
        public void CreateLevel(List<SimpleLetterInfo> simpleLetters)
        {
            _generatedLevelLetters.Clear();
            int countSameLettersSequentially = 0;
            KeyCode lastGeneratedLetter = KeyCode.None;

            for (int i = 0; i < _maxLettersCount; i++)
            {
                SimpleLetterInfo nextLetter;
                do
                {
                    nextLetter = simpleLetters.Random();
                    if (nextLetter.Key == lastGeneratedLetter)
                    {
                        if (++countSameLettersSequentially < _maxCountSameLettersSequentially)
                            break;
                    }
                    else
                    {
                        countSameLettersSequentially = 0;
                        lastGeneratedLetter = nextLetter.Key;
                        break;
                    }
                } while (true);

                _generatedLevelLetters.Add(nextLetter);
            }

            RespawnLetters();
        }

        public bool IsLastLetter(KeyCode key, bool isShifted)
            => LastLetter.Key == key && LastLetter.IsShifted == isShifted;

        public void NextLetter()
        {
            _pool.DespawnLast();
            _xPos += SpaceToNextLetter;
        }

        public void Hide()
        {
            _pool.DespawnAll();
        }
        #endregion Public

        private void Awake()
        {
            _pool = GetComponent<LocalPool>();
        }

        private void RespawnLetters()
        {
            _xPos = 0;
            _pool.DespawnAll();

            for (int i = 0; i < _generatedLevelLetters.Count; i++)
            {
                var letter = _pool.Spawn(_letterPrefab, new Vector3(_xPos, 0, 0));
                letter.Init(_generatedLevelLetters[i].Letter);

                _xPos -= SpaceToNextLetter;
            }
        }
    }
}