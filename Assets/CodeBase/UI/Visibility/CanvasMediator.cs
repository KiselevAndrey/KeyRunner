using UnityEngine;

namespace CodeBase.UI.Visibility
{
    public abstract class CanvasMediator : MonoBehaviour
    {
        protected abstract void ShowHide(bool isShow);

        protected void ShowHide(CanvasGroupController controller, bool isShow)
        {
            if (isShow)
                controller.Show();
            else
                controller.Hide();
        }
    }
}