using CodeBase.Infrastructure.Service;
using CodeBase.UI.Logic;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.State.Game
{
    public class GameStateMachine : AbstractStateMachine, IGameStateMachine
    {
        public GameStateMachine(ICoroutineRunner coroutineRunner, LoadingCurtain curtain, AllServices services)
        {
            SceneLoader sceneLoader = new(coroutineRunner);

            States = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, curtain, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, sceneLoader),
                [typeof(MenuState)] = new MenuState(this, curtain)
            };
        }
    }
}