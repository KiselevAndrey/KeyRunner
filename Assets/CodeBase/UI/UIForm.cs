using CodeBase.UI.Visibility;
using UnityEngine;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroupController))]
    public abstract class UIForm : MonoBehaviour
    {
        private CanvasGroupController _formVisibility;

        protected virtual void OnShowed() { }
        protected virtual void OnShowing() { }
        protected virtual void OnAwake() { }
        protected virtual void OnStarted() { }
        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }

        private void Awake()
        {
            _formVisibility = GetComponent<CanvasGroupController>();
            OnAwake();
        }

        private void Start()
            => OnStarted();


        private void OnEnable()
        {
            _formVisibility.Showed += OnShowed;
            _formVisibility.Showing += OnShowing;
            Subscribe();
        }

        private void OnDisable()
        {
            _formVisibility.Showed -= OnShowed;
            _formVisibility.Showing -= OnShowing;
            Unsubscribe();
        }
    }
}