using CodeBase.Game.Letter;
using CodeBase.Settings;
using CodeBase.UI.Keyboard;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Behaviors
{
    public class GameBehavior : MonoBehaviour
    {
        [SerializeField] private KeyboardBehavior _keyboard;
        [SerializeField] private GameLettersBehavior _gameLetter;
        [SerializeField] private LifeBehavior _life;
        [SerializeField] private LevelsOfKeysSO _levelsOfKeysSO;
        [SerializeField] private LanguageKeyMapSO _languageKeyMapSO;
        [SerializeField, Min(0)] private int _selectedLevel;

        public event UnityAction EndGame;

        public void ChangeKeyboardLayout()
        {
            _keyboard.InitDisplays(_languageKeyMapSO);
        }

        public void StartNewGame()
        {
            ChangeKeyboardLayout();
            SelectLevel(_selectedLevel);
            _life.StartNewGame();
            _keyboard.enabled = true;
        }

        private void OnEnable()
        {
            _keyboard.PressedKey += OnKeyPressed;
        }

        private void OnDisable()
        {
            _keyboard.PressedKey -= OnKeyPressed;
        }

        private void SelectLevel(int level)
        {
            var keys = _levelsOfKeysSO.GenerateKeys(level);
            List<SimpleLetterInfo> simpleLetters = new();

            foreach (var key in keys)
            {
                AddInfo(key.FirstKeys, key.IsShifted, ref simpleLetters);
                AddInfo(key.SecondKeys, key.IsShifted, ref simpleLetters);
            }

            _gameLetter.CreateLevel(simpleLetters);
            _keyboard.HighlightDisplay(_gameLetter.LastKey);
        }

        private void AddInfo(KeyCode key, bool isShifted, ref List<SimpleLetterInfo> leters)
        {
            if (key == KeyCode.None)
                return;

            var info = _languageKeyMapSO.GetSimpleInfo(key, isShifted);
            leters.Add(info);
        }

        private void OnKeyPressed(KeyCode key, bool isShifted)
        {
            if(_gameLetter.IsLastLetter(key, isShifted))
            {
                _gameLetter.NextLetter();

                if (_gameLetter.LettersLeft > 0)
                {
                    _keyboard.HighlightDisplay(_gameLetter.LastKey);
                }
                else
                {
                    SelectLevel(_selectedLevel);
                }
            }
            else
            {
                _life.HitMe(1);

                if (_life.IsLive == false)
                    GameEnded();
            }
        }

        private void GameEnded()
        {
            _keyboard.enabled = false;
            _keyboard.DeselectAllDisplays();
            _gameLetter.Hide();
            EndGame?.Invoke();
        }
    }
}