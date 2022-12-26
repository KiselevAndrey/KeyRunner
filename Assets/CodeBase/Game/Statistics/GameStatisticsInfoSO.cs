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

        private float _startGameTime;

        public void StartNewGame()
        {
            _startGameTime = Time.time;
            _pressedKeyCount = 0;
            _wrongKeyCount = 0;
            _typingDuration = 0;
            PressedKeysPerMin = 0;
            MistakesPercent = 0;
        }

        public void EndRound(int pressedCount, int wrongCount, float roundTime)
        {
            _pressedKeyCount += pressedCount;
            _wrongKeyCount += wrongCount;
            _typingDuration += roundTime;
        }

        public void EndGame()
        {
            GameDuration = Time.time - _startGameTime;
            PressedKeysPerMin = _pressedKeyCount / _typingDuration * 60;
            MistakesPercent = _wrongKeyCount * 100f / _pressedKeyCount;
        }
    }
}