using CodeBase.Gameplay;
using CodeBase.UI.Gameplay;
using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameplayForm _gameplayForm;
        [SerializeField] private GameplayService _gameplayService;

        private void Awake()
        {
            var service = Instantiate(_gameplayService);
            var form = Instantiate(_gameplayForm, transform);
            form.Init(service, service);
        }
    }
}
