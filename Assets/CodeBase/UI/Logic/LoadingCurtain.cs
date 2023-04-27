using CodeBase.UI.Visibility;
using UnityEngine;

namespace CodeBase.UI.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroupController _visibilityForm;

        public void Show() =>
            _visibilityForm.Show();

        public void Hide() =>
            _visibilityForm.Hide();

        private void Awake() =>
            DontDestroyOnLoad(gameObject);
    }
}