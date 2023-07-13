using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.State.Game;
using UnityEngine;

namespace CodeBase.UI.Gameplay
{
    [RequireComponent(typeof(PressEscService))]
    public class GameplayForm : MonoBehaviour
    {
        private PressEscService _escService;
        private AllServices Container => AllServices.Container;

        private void Awake()
        {
            _escService = GetComponent<PressEscService>();
            _escService.Init(GoToMenu);
        }

        private void GoToMenu() =>
            Container.Single<GameStateMachine>().Enter<MenuState>();
    }
}
