using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.State.Game;
using CodeBase.Infrastructure.State.Menu;
using UnityEngine;

namespace CodeBase.UI.Menu
{
    [RequireComponent(typeof(MenuMediator), typeof(PressEscService))]
    public class MenuForm : MonoBehaviour, IMenuSwitcher
    {
        [SerializeField] private MenuButtonListner _buttonListner;

        private MenuStateMachine _stateMachine;
        private AllServices Container => AllServices.Container;

        public void StartingGame() =>
            Container.Single<GameStateMachine>().Enter<GameplayState>();

        public void ShowMenu() =>
            _stateMachine.Enter<ButtonState>();

        public void ShowResult() =>
            _stateMachine.Enter<LeaderboardState>();

        #region IMenuSwitcher
        public void OnClickStartGame() =>
            _stateMachine.Enter<SelectLevelState>();

        public void OnClickOptions() =>
            _stateMachine.Enter<OptionsState>();

        public void OnClickLeaderboard() =>
            _stateMachine.Enter<LeaderboardState>();
        #endregion IMenuSwitcher

        #region Unity Lifecycle
        private void Awake()
        {
            var pressEscService = GetComponent<PressEscService>();
            pressEscService.Init(ShowMenu);

            _buttonListner.Init(this);

            _stateMachine = new MenuStateMachine(GetComponent<MenuMediator>(), pressEscService, Container);

            ShowMenu();
        }

        private void OnEnable() =>
            _buttonListner.OnEnable();

        private void OnDisable() =>
            _buttonListner.OnDisable();
        #endregion Unity Lifecycle
    }
}