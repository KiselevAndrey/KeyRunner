using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.State.Game;
using CodeBase.UI.Logic;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class Game
    {
        public GameStateMachine StateMachine { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain) => 
            StateMachine = new GameStateMachine(coroutineRunner, curtain, AllServices.Container);
    }
}
