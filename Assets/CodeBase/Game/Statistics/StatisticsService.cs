using CodeBase.Settings.Singleton;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.Statistics
{
    public class StatisticsService : MonoBehaviour
    {
        [SerializeField, Range(1, 20)] private int _maxCorrectlyPressInRow = 5;
        [SerializeField] private GameStatisticsInfoSO _statisticsSO;
        [SerializeField] private TMP_Text _scoreText;

        private float _startRoundTime;
        private int _pressedKeyCount;
        private int _wrongKeyCount;
        private int _pressedCorrectlyInRow;
        private int _inRowBonus;
        private float _score;
        private bool _isRoundStatisticUpdated;

        #region Public
        public void StartNewGame()
        {
            _statisticsSO.StartNewGame();
            ResetBonus();
            SetScore(0);
        }

        public void StartNewRound()
        {
            _startRoundTime = Time.time;
            _pressedKeyCount = 0;
            _wrongKeyCount = 0;
            _isRoundStatisticUpdated = false;
        }

        public void CorrectKey(bool isTrial)
        {
            _pressedKeyCount++;
            CalculateInRowBonus(isTrial);
            CalculateScore(isTrial);
        }

        public void WrongKey()
        {
            _pressedKeyCount++;
            _wrongKeyCount++;
            ResetBonus();
        }

        public void ResetBonus()
        {
            _pressedCorrectlyInRow = 0;
            _inRowBonus = 0;
        }

        public void EndRound()
        {
            if (_isRoundStatisticUpdated)
                return;

            _statisticsSO.EndRound(_pressedKeyCount, _wrongKeyCount, Time.time - _startRoundTime);
            _isRoundStatisticUpdated = true;
        }

        public void EndGame()
        {
            EndRound();
            _statisticsSO.EndGame(_score);
        }
        #endregion Public

        #region Private
        private void SetScore(float score)
        {
            _score = score;
            _scoreText.text = _score.ToString();
        }

        private void CalculateInRowBonus(bool isTrial)
        {
            if (isTrial)
                return;

            _pressedCorrectlyInRow++;
            if(_pressedCorrectlyInRow >= _maxCorrectlyPressInRow)
            {
                _inRowBonus++;
                _pressedCorrectlyInRow = 0;
            }
        }

        private void CalculateScore(bool isTrial)
        {
            _score += 1 
                + (isTrial 
                ? 0 
                : (PlayerInfoSO.SelectedLVL + _inRowBonus) / 10f);
            SetScore((float)System.Math.Round(_score, 1));
        }
        #endregion Private
    }
}