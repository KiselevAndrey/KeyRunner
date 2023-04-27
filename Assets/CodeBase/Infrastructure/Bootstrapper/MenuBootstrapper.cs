using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.State.Menu;
using CodeBase.UI.Menu;
using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class MenuBootstrapper : MonoBehaviour
    {
        [SerializeField] private MenuMediator _menuMediator;

        private void Awake()
        {
            new MenuStateMachine(_menuMediator, AllServices.Container);
        }
    }
}