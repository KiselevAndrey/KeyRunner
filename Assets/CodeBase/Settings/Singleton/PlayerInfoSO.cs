using UnityEngine;

namespace CodeBase.Settings.Singleton
{
    public enum Language { Ru, Eng }
    public enum KeyboardLayout { Ru, Eng }

    [CreateAssetMenu(fileName = nameof(PlayerInfoSO), menuName = nameof(CodeBase) + "/" + nameof(Singleton) +"/" + nameof(PlayerInfoSO))]
    public class PlayerInfoSO : SingletonScriptableObject<PlayerInfoSO>
    {
        [SerializeField] private int _selectedLVL;
        [SerializeField] private int _roundsEnded;
        [SerializeField] private int _caughtTimes;
        [SerializeField] private Language _language;
        [SerializeField] private KeyboardLayout _keyboard;

        public static int SelectedLVL { get => Instance._selectedLVL; set => Instance._selectedLVL = value; }
        public static int RoundsEnded { get => Instance._roundsEnded; set => Instance._roundsEnded = value; }
        public static int CaughtTimes { get => Instance._caughtTimes; set => Instance._caughtTimes = value; }

        public Language Language
        {
            get => _language;
            set
            {
                _language = value;
            }
        }

        public KeyboardLayout Keyboard
        {
            get => _keyboard;
            set
            {
                _keyboard = value;
            }
        }

        public static void StartNewGame()
        {
            RoundsEnded = 0;
            CaughtTimes = 0;
        }
    }
}