using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    public class MenuSubform : UIForm
    {
        [SerializeField] protected MenuForm Menu;
        [SerializeField] private Button _escButton;

        protected override void OnShowing()
            => Menu.EnableEsc();

        protected override void Subscribe()
            => _escButton.onClick.AddListener(OnEscButtonClick);

        protected override void Unsubscribe()
            => _escButton.onClick.RemoveListener(OnEscButtonClick);

        private void OnEscButtonClick()
        {
            Menu.ShowMenu();
        }
    }
}