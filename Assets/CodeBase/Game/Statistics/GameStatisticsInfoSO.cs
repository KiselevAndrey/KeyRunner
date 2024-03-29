using CodeBase.DataPersistence;
using System;
using UnityEngine;

namespace CodeBase.Game.Statistics
{
    [CreateAssetMenu(fileName = nameof(GameStatisticsInfoSO), menuName = nameof(CodeBase) + "/" + nameof(GameStatisticsInfoSO))]
    public class GameStatisticsInfoSO : ScriptableObject
    {
        [SerializeField] private int _pressedKeyCount;
        [SerializeField] private int _wrongKeyCount;
        [SerializeField] private float _typingDuration;

        [field: SerializeField] public float GameDuration { get; private set; }
        [field: SerializeField] public float PressedKeysPerMin { get; private set; }
        [field: SerializeField] public float MistakesPercent { get; private set; }
        [field: SerializeField] public float Score { get; private set; }
        [field: SerializeField] public string PlayerName { get; set; }

        private float _startGameTime;

        public void ResetInfo()
        {
            GameDuration = 0;
            PressedKeysPerMin = 0;
            MistakesPercent = 0;
            Score = 0;
        }

        public void StartNewGame()
        {
            _startGameTime = Time.time;
            _pressedKeyCount = 0;
            _wrongKeyCount = 0;
            _typingDuration = 0;
            ResetInfo();
        }

        public void EndRound(int pressedCount, int wrongCount, float roundTime)
        {
            _pressedKeyCount += pressedCount;
            _wrongKeyCount += wrongCount;
            _typingDuration += roundTime;
        }

        public void EndGame(float score)
        {
            GameDuration = Time.time - _startGameTime;
            PressedKeysPerMin = (float)Math.Round(_pressedKeyCount / _typingDuration * 60, 1);
            MistakesPercent = (float)Math.Round(_wrongKeyCount * 100f / _pressedKeyCount, 2);
            Score = score;
        }

        public LeaderData GetLeaderData()
        {
            return new LeaderData(
                PlayerName,
                Score,
                PressedKeysPerMin,
                MistakesPercent,
                DateTime.Today.ToString("d")
                );
        }
    }
}