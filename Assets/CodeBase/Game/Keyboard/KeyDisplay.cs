using CodeBase.Game.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.Keyboard
{
    public class KeyDisplay : MonoBehaviour
    {
        [field: SerializeField] public KeyCode Key { get; private set; }
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _singleLetter;
        [SerializeField] private GameObject _dobleLetterBase;
        [SerializeField] private TMP_Text _firstLetter;
        [SerializeField] private TMP_Text _secondLetter;

        private LetterInfo _info;

        public void Init(LetterInfo info)
        {
            _info = info;

            if (_info.IsDouble)
            {
                _singleLetter.gameObject.SetActive(false);
                _dobleLetterBase.SetActive(true);
                _firstLetter.text = _info.Letter;
                _secondLetter.text = _info.ShiftLetter;
            }
            else
            {
                _singleLetter.gameObject.SetActive(true);
                _dobleLetterBase.SetActive(false);
                _singleLetter.text = _info.Letter;
            }
        }

        public void ChangeColor(Color color)
        {
            _background.color = color;
        }
    }
}