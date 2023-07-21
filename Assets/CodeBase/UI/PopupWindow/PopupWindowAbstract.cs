using CodeBase.UI.Visibility;
using UnityEngine;

namespace CodeBase.UI.PopupWindow
{
    [RequireComponent(typeof(CanvasGroupController))]
    public abstract class PopupWindowAbstract : MonoBehaviour
    {
        private CanvasGroupController _visibility;

        public void Hide()
        {
            enabled = false;
            _visibility.HideSmooth();
        }

        protected void Show()
        {
            enabled = true;
            _visibility.ShowSmooth();
        }

        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }
        protected virtual void OnShowing() { }
        protected virtual void OnShowed() { }

        private void Awake()
        {
            _visibility = GetComponent<CanvasGroupController>();
            enabled = false;
        }

        private void OnEnable()
        {
            _visibility.Showing += OnShowing;
            _visibility.Showed += OnShowed;
            Subscribe();
        }

        private void OnDisable()
        {
            _visibility.Showing -= OnShowing;
            _visibility.Showed -= OnShowed;
            Unsubscribe();
        }
    }
}