using CodeBase.Infrastructure.Service;
using CodeBase.UI.Logic;

namespace CodeBase.Infrastructure.State.Game
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly LoadingCurtain _curtain;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, LoadingCurtain curtain, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _curtain = curtain;
            _services = services;

            RegisterAllServices();
        }

        public void Enter()
        {
            _curtain.Show();
            _gameStateMachine.Enter<LoadProgressState>();
        }

        public void Exit() { }

        private void RegisterAllServices()
        {

        }
    }
}