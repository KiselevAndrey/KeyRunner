using CodeBase.Settings;
using CodeBase.UI.Visibility;
using UnityEngine;

namespace CodeBase.UI.Game
{
    [RequireComponent(typeof(CanvasGroupController))]
    public class NewLetterDisplayBehaviour : MonoBehaviour
    {
        [SerializeField] private SimpleLetterDisplay _firstLetter;
        [SerializeField] private SimpleLetterDisplay _secondLetter;

        private CanvasGroupController _visibility;

        public void Show(LevelInfo info, LanguageKeyMapSO keyMapSO)
        {
            InitLetterDisplay(_firstLetter, keyMapSO.GetInfo(info.FirstKey), info.IsShifted);
            InitLetterDisplay(_secondLetter, keyMapSO.GetInfo(info.SecondKey), info.IsShifted);
            _visibility.ShowSmooth();
        }

        public void Hide()
        {
            _visibility.HideSmooth();
        }

        private void Awake()
        {
            _visibility = GetComponent<CanvasGroupController>();
        }

        private void InitLetterDisplay(SimpleLetterDisplay display, LetterInfo info, bool isShifted)
        {
            var letter = isShifted ? info.ShiftLetter : info.Letter;
            display.Init(letter);
        }
    }
}