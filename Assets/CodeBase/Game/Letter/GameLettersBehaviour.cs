using CodeBase.Settings;
using CodeBase.Utility;
using CodeBase.Utility.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Letter
{
    [RequireComponent(typeof(LocalPool))]
    public class GameLettersBehaviour : BaseGameLettersBehaviour
    {
        private readonly int _maxLettersCount = 34;
        private readonly int _maxCountSameLettersSequentially = 2;

        [SerializeField] private GameLetter _letterPrefab;
        [SerializeField, Min(0)] private float _spaceX = 0.1f;

        private float _xPos;

        private LocalPool _pool;

        private float SpaceToNextLetter => _letterPrefab.Width + _spaceX;

        public override int LettersLeft => _pool.SpawnedCount;
        public override float LastKeyPositionX => _xPos + transform.position.x;

        #region Public
        public override void CreateLevel(List<SimpleLetterInfo> simpleLetters)
        {
            GeneratedLevelLetters.Clear();
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

                GeneratedLevelLetters.Add(nextLetter);
            }

            RespawnLetters();
        }

        public override void NextLetter()
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

            for (int i = 0; i < GeneratedLevelLetters.Count; i++)
            {
                var letter = _pool.Spawn(_letterPrefab, new Vector3(_xPos, 0, 0));
                letter.Init(GeneratedLevelLetters[i].Letter);

                _xPos -= SpaceToNextLetter;
            }
        }
    }
}