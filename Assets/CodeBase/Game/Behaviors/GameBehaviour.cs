using CodeBase.Game.Character;
using CodeBase.Game.Letter;
using CodeBase.Settings;
using CodeBase.Settings.Singleton;
using CodeBase.UI;
using CodeBase.UI.Game;
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
        private readonly int _trialRound = -1;
        private readonly int _maxRoundsInLVL = 3;

        [Header("UI")]
        [SerializeField] private KeyboardBehaviour _keyboard;
        [SerializeField] private LifeBehaviour _life;
        [SerializeField] private NewLetterDisplayBehaviour _newLetterDisplay;
        [SerializeField] private Button _escButton;
        [SerializeField] private PopupWindow _escPopupWindow;

        [Header("Behaviours")]
        [SerializeField] private GameLettersBehaviour _gameLetter;
        [SerializeField] private TrialGameLettersBehaviour _trialGameLetter;
        [SerializeField] private CharacterBehaviour _character;
        [SerializeField] private EnemyBehaviour _enemy;

        [Header("SO")]
        [SerializeField] private LevelsOfKeysSO _levelsOfKeysSO;
        [SerializeField] private LanguageKeyMapSO _languageKeyMapSO;

        private int _round;
        private bool _isPauseNow;
        private bool _isTrial;

        private PressEscBehaviour _pressEscBehaviour;
        private IGameLettersBehaviour _usedGameLetter;

        public event UnityAction EndGame;

        public void ChangeKeyboardLayout()
        {
            _keyboard.InitDisplays(_languageKeyMapSO);
        }

        public void StartNewGame()
        {
            ChangeKeyboardLayout();
            _round = _trialRound;
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
            _character.EndMoving += OnEndMoving;
            _enemy.CharacterCaught += OnCharacterCaught;
            _escButton.onClick.AddListener(OnEscButtonPressed);
        }

        private void OnDisable()
        {
            _keyboard.PressedKey -= OnKeyPressed;
            _character.EndMoving -= OnEndMoving;
            _enemy.CharacterCaught -= OnCharacterCaught;
            _escButton.onClick.AddListener(OnEscButtonPressed);
        }
        #endregion Unity Lifecycle

        #region Private
        #region Subscribtions
        private void OnKeyPressed(KeyCode key, bool isShifted)
        {
            if (_usedGameLetter.IsLastLetter(key, isShifted))
            {
                _usedGameLetter.NextLetter();
                _character.NextPositionX(_usedGameLetter.LastKeyPositionX);

                if(_isTrial == false)
                    _enemy.NextPositionX(_character.Position.x);

                if (_usedGameLetter.LettersLeft > 0)
                {
                    _keyboard.HighlightDisplay(_usedGameLetter.LastKey);
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

        private void OnEndMoving()
        {
            if (_isTrial == false)
                return;

            _round++;
            InitLevel();
        }

        private void OnCharacterCaught(int damage)
        {
            if (_isTrial)
                return;

            Hit(damage);

            // Wait Die Animation
            if (_life.IsLive)
                InitLevel();
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
        #endregion Subscribtions

        private void InitLevel()
        {
            print("Init level");
            int level = GiveLVL(PlayerInfoSO.SelectedLVL);

            // check is over last LVL
            if(level > _levelsOfKeysSO.MaxLevel)
            {
                GameEnded();
                return;
            }

            List<SimpleLetterInfo> simpleLetters 
                = _round == _trialRound
                ? InitTrialLevelKeys(level)
                : InitNormalLevelKeys(level);

            _usedGameLetter.CreateLevel(simpleLetters);
            InitCharacter();

            if (_isTrial)
            {
                _enemy.Hide();
                _usedGameLetter.NextLetter();
                _character.NextPositionX(_usedGameLetter.LastKeyPositionX);
            }
            else
                InitEnemy();

            _keyboard.HighlightDisplay(_usedGameLetter.LastKey);
        }

        private int GiveLVL(int currentLVL)
        {
            // check is new lvl
            if (_round >= _maxRoundsInLVL)
            {
                currentLVL++;
                _round = _trialRound;
                int level = _levelsOfKeysSO.ApprovedLevel(currentLVL);
                PlayerInfoSO.SelectedLVL = level;
            }

            return currentLVL;
        }

        private List<SimpleLetterInfo> InitTrialLevelKeys(int level)
        {
            _isTrial = true;
            _usedGameLetter = _trialGameLetter;

            var keyInfo = _levelsOfKeysSO.GetNewKeysInfo(level);
            List<SimpleLetterInfo> simpleLetters = new();
            AddInfo(keyInfo.FirstKey, keyInfo.IsShifted, ref simpleLetters);
            AddInfo(keyInfo.SecondKey, keyInfo.IsShifted, ref simpleLetters);

            _newLetterDisplay.Show(keyInfo, _languageKeyMapSO);
         
            return simpleLetters;
        }

        private List<SimpleLetterInfo> InitNormalLevelKeys(int level)
        {
            _isTrial = false;
            _usedGameLetter = _gameLetter;
            _newLetterDisplay.Hide();

            var keys = _round == _trialRound + 1
                ? _levelsOfKeysSO.GetNewKeys(level)
                : _levelsOfKeysSO.GenerateKeys(level);

            List<SimpleLetterInfo> simpleLetters = new();

            foreach (var key in keys)
            {
                AddInfo(key.FirstKey, key.IsShifted, ref simpleLetters);
                AddInfo(key.SecondKey, key.IsShifted, ref simpleLetters);
            }

            return simpleLetters;
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
            letterPos.x = _usedGameLetter.LastKeyPositionX;
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