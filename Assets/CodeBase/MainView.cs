using CodeBase.Game;
using CodeBase.UI.Menu;
using CodeBase.UI.Visibility;
using UnityEngine;

namespace CodeBase
{
    public class MainView : MonoBehaviour
    {
        [SerializeField] private MainMediator _mediator;
        [SerializeField] private GameService _game;
        [SerializeField] private MenuForm _menu;

        private void Start()
        {
            _mediator.Show(Window.Menu);
        }

        private void OnEnable()
        {
            _game.EndGame += OnGameEnded;
            _menu.StartGame += OnGameStarted;
        }

        private void OnDisable()
        {
            _game.EndGame -= OnGameEnded;
            _menu.StartGame -= OnGameStarted;
        }

        private void OnGameEnded()
        {
            _mediator.Show(Window.Menu);
        }

        private void OnGameStarted()
        {
            _mediator.Show(Window.Game);
            _game.StartNewGame();
        }
    }
}