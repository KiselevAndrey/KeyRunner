using CodeBase.Game.Character;
using CodeBase.Game.Letter;
using CodeBase.Game.Statistics;
using CodeBase.Settings;
using CodeBase.Settings.Singleton;
using CodeBase.UI;
using CodeBase.UI.Game;
using CodeBase.UI.Keyboard;
using CodeBase.UI.PopupWindow;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Game
{
    [RequireComponent(typeof(PressEscService), typeof(StatisticsService))]
    public class GameService : MonoBehaviour
    {
        private readonly int _trialRound = -1;
        private readonly int _maxRoundsInLVL = 3;

        [Header("UI")]
        [SerializeField] private KeyboardService _keyboard;
        [SerializeField] private LifeService _life;
        [SerializeField] private NewLetterDisplay _newLetterDisplay;
        [SerializeField] private Button _escButton;
        [SerializeField] private PopupWindowWithTwoButtonsAndText _escPopupWindow;
        [SerializeField] private StatisticsPopupWindow _statisticPopupWindow;

        [Header("Behaviours")]
        [SerializeField] private GameLettersService _gameLetter;
        [SerializeField] private TrialGameLettersService _trialGameLetter;
        [SerializeField] private CharacterStateSwitch _character;
        [SerializeField] private EnemyStateSwitch _enemy;
        [SerializeField] private WorldView _world;

        [Header("SO")]
        [SerializeField] private LevelsOfKeysSO _levelsOfKeysSO;
        [SerializeField] private LanguageKeyMapSO _languageKeyMapSO;

        private int _round;
        private bool _isPauseNow;
        private bool _isTrial;

        private StatisticsService _statistics;
        private PressEscService _pressEscBehaviour;
        private IGameLettersService _usedGameLetter;

        public event UnityAction EndGame;

        public void ChangeKeyboardLayout() 
            => _keyboard.InitDisplays(_languageKeyMapSO);

        public void StartNewGame()
        {
            ChangeKeyboardLayout();
            _round = _trialRound;
            InitLevel();
            _life.StartNewGame(30);
            _statistics.StartNewGame();
            PlayerInfoSO.StartNewGame();
            _keyboard.enabled = true;
            _pressEscBehaviour.enabled = true;
            _world.Show();
        }

        #region Unity Lifecycle
        private void Awake()
        {
            _statistics = GetComponent<StatisticsService>();
            _pressEscBehaviour = GetComponent<PressEscService>();
            _pressEscBehaviour.Init(OnEscButtonPressed);
            _pressEscBehaviour.enabled = false;
            _world.Hide();
        }

        private void OnEnable()
        {
            _keyboard.PressedKey += OnKeyPressed;
            _character.EndMoving += OnEndMoving;
            _enemy.CharacterCaught += OnCharacterCaught;
            _enemy.HuntingEnding += OnHuntingEnded;
            _escButton.onClick.AddListener(OnEscButtonPressed);
        }

        private void OnDisable()
        {
            _keyboard.PressedKey -= OnKeyPressed;
            _character.EndMoving -= OnEndMoving;
            _enemy.CharacterCaught -= OnCharacterCaught;
            _enemy.HuntingEnding -= OnHuntingEnded;
            _escButton.onClick.RemoveListener(OnEscButtonPressed);
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

                _statistics.CorrectKey(_isTrial);
                
                if (_isTrial)
                    _newLetterDisplay.ChangeRemainsCount(_usedGameLetter.LettersLeft);
                else
                    _enemy.NextPositionX(_character.Position.x);

                if (_usedGameLetter.LettersLeft > 0)
                    _keyboard.HighlightDisplay(_usedGameLetter.LastKey);
                else
                    EndRound();
            }
            else
            {
                _statistics.WrongKey();
                Hit(1);
            }
        }

        private void OnEndMoving()
        {
            if (_isTrial == false)
                return;

            _round++;
            InitLevel();
        }

        private void OnCharacterCaught()
        {
            if (_isTrial)
                return;

            _keyboard.enabled = false;
            _character.StopMoving();
            _enemy.StopMoving();
            _statistics.EndRound();
            _statistics.ResetBonus();
        }

        private void OnHuntingEnded(int damage)
        {
            Hit(damage);

            if (_life.IsLive)
            {
                PlayerInfoSO.CaughtTimes++;
                InitLevel();
                _keyboard.enabled = true;
            }
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

        private void EndRound()
        {
            _round++;
            PlayerInfoSO.RoundsEnded++;

            if(_isTrial == false)
                _statistics.EndRound();

            InitLevel();
        }

        private void InitLevel()
        {
            int level = GiveLVL(PlayerInfoSO.SelectedLVL);

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
                _newLetterDisplay.ChangeRemainsCount(_usedGameLetter.LettersLeft);
            }
            else
            {
                InitEnemy();
                _statistics.StartNewRound();
            }

            _keyboard.HighlightDisplay(_usedGameLetter.LastKey);
        }

        private int GiveLVL(int currentLVL)
        {
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
            _statistics.EndGame();
            _keyboard.DeselectAllDisplays();
            _character.StopMoving();
            _enemy.StopMoving();
            _statisticPopupWindow.Show(EndShowStatistics);
        }

        private void EndShowStatistics()
        {
            _gameLetter.Hide();
            _character.Hide();
            _enemy.Hide();
            _world.Hide();
            _isPauseNow = false;
            Time.timeScale = 1f;
            EndGame?.Invoke();
        }
        #endregion Private
    }
}