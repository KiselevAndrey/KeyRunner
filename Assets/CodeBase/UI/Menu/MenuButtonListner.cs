using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    [System.Serializable]
    public class MenuButtonListner
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _leaderboardButton;

        private IMenuSwitcher _menuSwitcher;

        public void Init(IMenuSwitcher menuSwitcher)
        {
            _menuSwitcher = menuSwitcher;
        }

        public void OnEnable()
        {
            _startButton.onClick.AddListener(_menuSwitcher.OnClickStartGame);
            _optionsButton.onClick.AddListener(_menuSwitcher.OnClickOptions);
            _leaderboardButton.onClick.AddListener(_menuSwitcher.OnClickLeaderboard);
        }

        public void OnDisable()
        {
            _startButton.onClick.RemoveListener(_menuSwitcher.OnClickStartGame);
            _optionsButton.onClick.RemoveListener(_menuSwitcher.OnClickOptions);
            _leaderboardButton.onClick.RemoveListener(_menuSwitcher.OnClickLeaderboard);
        }
    }
}