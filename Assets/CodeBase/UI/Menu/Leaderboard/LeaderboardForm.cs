using CodeBase.DataPersistence;
using CodeBase.Game.Statistics;
using CodeBase.Utility;
using UnityEngine;

namespace CodeBase.UI.Menu.Leaderboard
{
    public class LeaderboardForm : MenuSubform
    {
        [Header("Pool")]
        [SerializeField, Min(0)] private int _maxCountOfLeader = 10;
        [SerializeField] private LocalPool _pool;
        [SerializeField] private LeaderboardDisplay _prefab;
        [SerializeField] private LeaderboardDisplay _currentScoreDisplay;

        [Header("Others")]
        [SerializeField] private GameStatisticsInfoSO _gameStatisticSO;
        [SerializeField] private DataService _dataService;
        [SerializeField] private NewLeaderPopupWindow _newLeaderPopupWindow;

        private bool _needCheckNewResult;

        #region Protected
        protected override void OnShowing()
        {
            base.OnShowing();

            ShowLeaders();
            _currentScoreDisplay.gameObject.SetActive(false);
        }

        protected override void OnStarted()
        {
            base.OnStarted();

            _gameStatisticSO.ResetInfo();
        }

        protected override void Subscribe()
        {
            base.Subscribe();

            Menu.StartGame += OnGameStarted;
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();

            Menu.StartGame -= OnGameStarted;
        }
        #endregion Protected

        #region Private
        private void ShowLeaders()
        {
            _pool.DespawnAll();

            foreach (var leader in _dataService.Data.Leaders)
            {
                var display = _pool.Spawn(_prefab);
                display.Init(leader);
            }

            if(_needCheckNewResult)
                CheckResultLastGame();
        }

        private void CheckResultLastGame()
        {
            _needCheckNewResult = false;

            if(_dataService.Data.Leaders.Count < _maxCountOfLeader
                || _dataService.Data.Leaders[_maxCountOfLeader - 1].Score < _gameStatisticSO.Score)
            {
                _newLeaderPopupWindow.Show(CreateNewLeadersName);
                _currentScoreDisplay.gameObject.SetActive(false);
            }
            else
            {
                _currentScoreDisplay.gameObject.SetActive(true);
                _currentScoreDisplay.Init(_gameStatisticSO.GetLeaderData());
            }
        }

        private void CreateNewLeadersName(string playerName)
        {
            _gameStatisticSO.PlayerName = playerName;

            var leader = _gameStatisticSO.GetLeaderData();
            if (_dataService.Data.Leaders.Count < _maxCountOfLeader)
                _dataService.AddLeader(leader);
            else
                _dataService.ChangeLeader(leader);

            ShowLeaders();
        }

        private void OnGameStarted()
            => _needCheckNewResult = true;
        #endregion Private
    }
}