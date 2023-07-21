using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.Menu
{
    [RequireComponent(typeof(Button))]
    public class SelectLvlButton : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private TMP_Text _buttonName;

        private int _level;

        private Button _button;

        private Action<int> _highlightAction;
        private Action<int> _pressAction;

        public void Init(int level, Action<int> highlightAction, Action<int> pressAction)
        {
            _level = level;
            _buttonName.text = level.ToString();
            _highlightAction = highlightAction;
            _pressAction = pressAction;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _pressAction.Invoke(_level);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _highlightAction.Invoke(_level);
        }
    }
}