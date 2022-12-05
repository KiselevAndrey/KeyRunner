using CodeBase.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Keyboard
{
    public class KeyDisplay : MonoBehaviour
    {
        private readonly Color _firstColor = Color.black;
        private readonly Color _secondColor = new(0.6f, 0.6f, 0.6f);

        [field: SerializeField] public KeyCode Key { get; private set; }
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _singleLetter;
        [SerializeField] private GameObject _dobleLetterBase;
        [SerializeField] private TMP_Text _firstLetter;
        [SerializeField] private TMP_Text _secondLetter;

        private LetterInfo _info;
        private Color _basicColor;

        public void Init(LetterInfo info)
        {
            if (_basicColor == new Color(0, 0, 0, 0))
                _basicColor = _background.color;

            _info = info;

            if (_info.IsDouble)
            {
                _singleLetter.gameObject.SetActive(false);
                _dobleLetterBase.SetActive(true);
                _firstLetter.text = _info.Letter;
                _firstLetter.color = _firstColor;
                _secondLetter.text = _info.ShiftLetter;
                _secondLetter.color = _secondColor;
            }
            else
            {
                _singleLetter.gameObject.SetActive(true);
                _dobleLetterBase.SetActive(false);
                _singleLetter.text = _info.Letter;
            }
        }

        public void SetShift(bool value)
        {
            if (_info.IsDouble)
            {
                _firstLetter.color = value ? _secondColor : _firstColor;
                _secondLetter.color = value ? _firstColor : _secondColor;
            }
            else
                _singleLetter.text = value && _info.ShiftLetter.Length > 0
                    ? _info.ShiftLetter
                    : _info.Letter;
        }

        public void ChangeColor(Color color) => _background.color = color;

        public void ResetColor() => _background.color = _basicColor;
    }
}