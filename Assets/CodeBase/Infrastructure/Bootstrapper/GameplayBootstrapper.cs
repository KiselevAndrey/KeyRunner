using CodeBase.UI.Gameplay;
using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class GameplayBootstrapper : MonoBehaviour
    {
        [SerializeField] private GameplayForm _gameplayForm;

        private void Awake()
        {
            Instantiate(_gameplayForm, transform);
        }
    }
}
