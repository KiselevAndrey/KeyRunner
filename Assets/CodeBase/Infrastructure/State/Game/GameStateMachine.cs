using CodeBase.Infrastructure.Service;
using CodeBase.UI.Logic;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.State.Game
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, AllServices services)
        {
            SceneLoader sceneLoader = new(coroutineRunner);

            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, curtain, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, sceneLoader),
                [typeof(MenuState)] = new MenuState(this, curtain)
            };
        }

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
          _states[typeof(TState)] as TState;
    }
}