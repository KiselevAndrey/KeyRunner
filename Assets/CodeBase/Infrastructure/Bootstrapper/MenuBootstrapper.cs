using CodeBase.UI.Menu;
using UnityEngine;

namespace CodeBase.Infrastructure.Bootstrapper
{
    public class MenuBootstrapper : MonoBehaviour
    {
        [SerializeField] private MenuForm _menuForm;

        private void Awake()
        {
            Instantiate(_menuForm, transform);
        }
    }
}