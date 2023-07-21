using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    [RequireComponent(typeof(MenuMediator), typeof(PressEscService))]
    public class MenuForm : UIForm
    {
        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _leaderboardButton;

        private MenuMediator _mediator;
        private PressEscService _pressEscBehaviour;

        private bool _isGameStarted;

        public event UnityAction StartGame;

        public void StartingGame()
        {
            ShowMenu();
            _isGameStarted = true;
            StartGame?.Invoke();
        }

        public void ShowMenu()
        {
            _mediator.Show(MenuWindow.Buttons);
            _pressEscBehaviour.enabled = false;
        }

        public void EnableEsc()
            => _pressEscBehaviour.enabled = true;

        #region Protected
        protected override void OnShowed()
        {
            if (_isGameStarted)
                ShowResult();
            else
                ShowMenu();
        }

        protected override void OnAwake()
        {
            _mediator = GetComponent<MenuMediator>();
            _pressEscBehaviour = GetComponent<PressEscService>();
        }

        protected override void OnStarted() 
            => _pressEscBehaviour.Init(ShowMenu);

        protected override void Subscribe()
        {
            _startButton.onClick.AddListener(OnClickStartGame);
            _optionsButton.onClick.AddListener(OnClickOptions);
            _leaderboardButton.onClick.AddListener(OnClickLeaderboard);
        }

        protected override void Unsubscribe()
        {
            _startButton.onClick.RemoveListener(OnClickStartGame);
            _optionsButton.onClick.RemoveListener(OnClickOptions);
            _leaderboardButton.onClick.RemoveListener(OnClickLeaderboard);
        }
        #endregion Protected

        private void OnClickStartGame() 
            => _mediator.Show(MenuWindow.SelectLVL);

        private void OnClickOptions()
        {
            _mediator.Show(MenuWindow.Options);
            print("OnClickOptions");
        }

        private void OnClickLeaderboard()
            => _mediator.Show(MenuWindow.Leaderboard);

        private void ShowResult()
        {
            _isGameStarted = false;
            _mediator.Show(MenuWindow.Leaderboard);
        }
    }
}