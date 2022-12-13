using CodeBase.UI.Visibility;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroupController))]
    public class PopupWindow : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] Button _leftButton;
        [SerializeField] Button _rightButton;

        [Header("Text")]
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _body;

        private CanvasGroupController _visibility;

        private UnityAction _onLeftAction;
        private UnityAction _onRightAction;

        public void Show(string header, string body, UnityAction onLeftAction, UnityAction onRightAction)
        {
            _header.text = header;
            _body.text = body;
            _onLeftAction = onLeftAction;
            _onRightAction = onRightAction;

            _visibility.ShowSmooth();
        }

        public void Hide()
        {
            _visibility.HideSmooth();
        }

        #region Unity Lifecycle
        private void Awake()
        {
            _visibility = GetComponent<CanvasGroupController>();
        }

        private void OnEnable()
        {
            if (_leftButton)
                _leftButton.onClick.AddListener(OnLeftButtonClick);

            if (_rightButton)
                _rightButton.onClick.AddListener(OnRightButtonClick);
        }

        private void OnDisable()
        {
            if (_leftButton)
                _leftButton.onClick.RemoveListener(OnLeftButtonClick);

            if (_rightButton)
                _rightButton.onClick.RemoveListener(OnRightButtonClick);
        }
        #endregion Unity Lifecycle

        #region Private
        private void OnLeftButtonClick()
        {
            Hide();
            _onLeftAction?.Invoke();
        }

        private void OnRightButtonClick()
        {
            Hide();
            _onRightAction?.Invoke();
        }
        #endregion Private
    }
}