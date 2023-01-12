using CodeBase.Game.Statistics;
using CodeBase.Utility;
using UnityEngine;

namespace CodeBase.UI.Menu
{
    public class LeaderboardForm : MenuSubform
    {
        [SerializeField] private LocalPool _pool;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private GameStatisticsInfoSO _lastGameStatisticSO;

        public void StartGame() { }
        public void EndGame() { }

        protected override void OnShowing()
        {
            base.OnShowing();

            ShowLastResults();
        }

        protected override void OnStarted()
        {
            base.OnStarted();

            _lastGameStatisticSO.ResetInfo();
        }

        private void ShowLastResults()
        {

        }
    }
}