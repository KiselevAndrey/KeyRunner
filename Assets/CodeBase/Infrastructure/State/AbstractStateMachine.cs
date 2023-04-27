using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.State
{
    public abstract class AbstractStateMachine : IStateMachine
    {
        protected Dictionary<Type, IExitableState> States;

        private IExitableState _activeState;

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
          States[typeof(TState)] as TState;
    }
}