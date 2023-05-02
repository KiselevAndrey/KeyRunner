using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.State.Menu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    [RequireComponent(typeof(MenuMediator), typeof(PressEscService))]
    public class MenuForm : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _leaderboardButton;

        private MenuStateMachine _stateMachine;
        private PressEscService _pressEscService;
        private MenuMediator _mediator;

        private bool _isGameStarted;

        public event UnityAction StartGame;

        public void Init()
        {
            _stateMachine = new MenuStateMachine(_mediator, _pressEscService, AllServices.Container);
            ShowMenu();
        }

        public void StartingGame()
        {
            ShowMenu();
            _isGameStarted = true;
            StartGame?.Invoke();
        }

        public void ShowMenu()
        {
            //_mediator.Show(MenuWindow.Buttons);
            _stateMachine.Enter<ButtonState>();
            _pressEscService.enabled = false;
        }

        public void EnableEsc()
            => _pressEscService.enabled = true;

        #region Protected
        //protected override void OnShowed()
        //{
        //    if (_isGameStarted)
        //        ShowResult();
        //    else
        //        ShowMenu();
        //}

        private void Awake()
        {
            _mediator = GetComponent<MenuMediator>();
            _pressEscService = GetComponent<PressEscService>();
        }

        private void Start()
            => _pressEscService.Init(ShowMenu);

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnClickStartGame);
            _optionsButton.onClick.AddListener(OnClickOptions);
            _leaderboardButton.onClick.AddListener(OnClickLeaderboard);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnClickStartGame);
            _optionsButton.onClick.RemoveListener(OnClickOptions);
            _leaderboardButton.onClick.RemoveListener(OnClickLeaderboard);
        }
        #endregion Protected

        private void OnClickStartGame()
        {
            //_mediator.Show(MenuWindow.SelectLVL);
        }

        private void OnClickOptions()
        {
            //_mediator.Show(MenuWindow.Options);
            print("OnClickOptions");
        }

        private void OnClickLeaderboard()
        {
            //_mediator.Show(MenuWindow.Leaderboard);
        }

        private void ShowResult()
        {
            _isGameStarted = false;
            //_mediator.Show(MenuWindow.Leaderboard);
        }
    }
}