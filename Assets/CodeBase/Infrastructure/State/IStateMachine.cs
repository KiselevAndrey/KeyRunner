using CodeBase.Infrastructure.Service;

namespace CodeBase.Infrastructure.State
{
    public interface IStateMachine : IService
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }

    public interface IGameStateMachine : IStateMachine { }
}