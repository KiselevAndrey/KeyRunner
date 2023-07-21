using TMPro;
using UnityEngine;

namespace CodeBase.Game.Letter
{
    public class GameLetter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Transform _background;

        public float Width => _background.localScale.x;

        public void Init(string letter)
            => _text.text = letter;
    }
}