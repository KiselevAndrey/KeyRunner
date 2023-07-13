using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Gameplay
{
    public class SimpleLetterDisplay : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _letter;

        public void Init(string letter)
        {
            _letter.text = letter;
        }
    }
}