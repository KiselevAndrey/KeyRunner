using UnityEngine;

namespace CodeBase.Game.Statistics
{
    public class StatisticsBehaviour : MonoBehaviour
    {
        [SerializeField] private GameStatisticsInfoSO _statisticsSO;

        private float _startRoundTime;
        private int _pressedKeyCount;
        private int _wrongKeyCount;
        private bool _isRoundStatisticUpdated;

        public void StartNewGame() 
            => _statisticsSO.StartNewGame();

        public void StartNewRound()
        {
            _startRoundTime = Time.time;
            _pressedKeyCount = 0;
            _wrongKeyCount = 0;
            _isRoundStatisticUpdated = false;
        }

        public void CorrectKey() 
            => _pressedKeyCount++;

        public void WrongKey()
        {
            _pressedKeyCount++;
            _wrongKeyCount++;
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
            _statisticsSO.EndGame();
        }
    }
}