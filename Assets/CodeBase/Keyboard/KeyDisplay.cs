using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Keyboard
{
    public class KeyDisplay : MonoBehaviour
    {
        [field: SerializeField] public KeyCode Key { get; private set; }
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _background;

        public void Init(string leter)
        {
            _nameText.text = leter;
        }

        public void ChangeColor(Color color)
        {
            _background.color = color;
        }

        private void Awake()
        {
            _nameText.text = Key.ToString();
        }
    }
}