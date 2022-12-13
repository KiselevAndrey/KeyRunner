using CodeBase.Game.Character;
using CodeBase.Game.Letter;
using CodeBase.Settings;
using CodeBase.Settings.Singleton;
using CodeBase.UI;
using CodeBase.UI.Keyboard;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Game.Behaviours
{
    [RequireComponent(typeof(PressEscBehaviour))]
    public class GameBehaviour : MonoBehaviour
    {
        private readonly int _maxRoundsInLVL = 2;

        [Header("UI")]
        [SerializeField] private KeyboardBehaviour _keyboard;
        [SerializeField] private LifeBehaviour _life;
        [SerializeField] private Button _escButton;
        [SerializeField] private PopupWindow _escPopupWindow;

        [Header("Behaviours")]
        [SerializeField] private GameLettersBehaviour _gameLetter;
        [SerializeField] private CharacterBehaviour _character;
        [SerializeField] private EnemyBehaviour _enemy;

        [Header("SO")]
        [SerializeField] private LevelsOfKeysSO _levelsOfKeysSO;
        [SerializeField] private LanguageKeyMapSO _languageKeyMapSO;

        private int _round;
        private bool _isPauseNow;

        private PressEscBehaviour _pressEscBehaviour;

        public event UnityAction EndGame;

        public void ChangeKeyboardLayout()
        {
            _keyboard.InitDisplays(_languageKeyMapSO);
        }

        public void StartNewGame()
        {
            ChangeKeyboardLayout();
            _round = 0;
            InitLevel();
            _life.StartNewGame();
            _keyboard.enabled = true;
            _pressEscBehaviour.enabled = true;
            _isPauseNow = false;
        }

        #region Unity Lifecycle
        private void Awake()
        {
            _pressEscBehaviour = GetComponent<PressEscBehaviour>();
            _pressEscBehaviour.Init(OnEscButtonPressed);
            _pressEscBehaviour.enabled = false;
        }

        private void OnEnable()
        {
            _keyboard.PressedKey += OnKeyPressed;
            _enemy.CharacterCaught += OnCharacterCaught;
            _escButton.onClick.AddListener(OnEscButtonPressed);
        }

        private void OnDisable()
        {
            _keyboard.PressedKey -= OnKeyPressed;
            _enemy.CharacterCaught -= OnCharacterCaught;
            _escButton.onClick.AddListener(OnEscButtonPressed);
        }
        #endregion Unity Lifecycle

        #region Private
        #region Subscribtions
        private void OnKeyPressed(KeyCode key, bool isShifted)
        {
            if (_gameLetter.IsLastLetter(key, isShifted))
            {
                _gameLetter.NextLetter();
                _character.NextPositionX(_gameLetter.LastKeyPositionX);
                _enemy.NextPositionX(_character.Position.x);

                if (_gameLetter.LettersLeft > 0)
                {
                    _keyboard.HighlightDisplay(_gameLetter.LastKey);
                }
                else
                {
                    _round++;
                    InitLevel();
                }
            }
            else
                Hit(1);
        }

        private void OnEscButtonPressed()
        {
            _isPauseNow = !_isPauseNow;

            Time.timeScale = _isPauseNow ? 0 : 1;
            _keyboard.enabled = _isPauseNow == false;

            if (_isPauseNow)
                _escPopupWindow.Show("Leave game", "Do you want to leave the game?", GameEnded, OnEscButtonPressed);
            else
                _escPopupWindow.Hide();
        }

        private void OnCharacterCaught(int damage)
        {
            Hit(damage);

            if (_life.IsLive)
                InitLevel();
        }
        #endregion Subscribtions

        private void InitLevel()
        {
            int level = PlayerInfoSO.SelectedLVL;
            if (_round >= _maxRoundsInLVL)
            {
                _round = 0;
                level = _levelsOfKeysSO.ApprovedLevel(level + 1);
                PlayerInfoSO.SelectedLVL = level;
            }

            var keys = _round == 0
                ? _levelsOfKeysSO.GetNewKeys(level)
                : _levelsOfKeysSO.GenerateKeys(level);

            List<SimpleLetterInfo> simpleLetters = new();

            foreach (var key in keys)
            {
                AddInfo(key.FirstKeys, key.IsShifted, ref simpleLetters);
                AddInfo(key.SecondKeys, key.IsShifted, ref simpleLetters);
            }

            _gameLetter.CreateLevel(simpleLetters);
            _keyboard.HighlightDisplay(_gameLetter.LastKey);
            InitCharacter();
            InitEnemy();
        }

        private void AddInfo(KeyCode key, bool isShifted, ref List<SimpleLetterInfo> leters)
        {
            if (key == KeyCode.None)
                return;

            var info = _languageKeyMapSO.GetSimpleInfo(key, isShifted);
            leters.Add(info);
        }

        private void InitCharacter()
        {
            var letterPos = _gameLetter.transform.position;
            letterPos.x = _gameLetter.LastKeyPositionX;
            _character.Init(letterPos);
        }

        private void InitEnemy()
        {
            _enemy.Init(_character.Position, _character.Body);
            _enemy.NextPositionX(_character.Position.x);
        }

        private void Hit(int damage)
        {
            _life.HitMe(damage);

            if (_life.IsLive == false)
                GameEnded();
        }

        private void GameEnded()
        {
            _pressEscBehaviour.enabled = false;
            _keyboard.enabled = false;
            _keyboard.DeselectAllDisplays();
            _gameLetter.Hide();
            _character.Hide();
            _enemy.Hide();
            EndGame?.Invoke();
        }
        #endregion Private
    }
}