using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Game
{
    public class LifeBehaviour : MonoBehaviour
    {
        [SerializeField] private Image _lifeFiller;

        private int _maxLife;
        private float _currentLife;

        private float CurrentLife
        {
            get => _currentLife;
            set
            {
                if(value < 0)
                    value = 0;

                _currentLife = value;
                _lifeFiller.fillAmount = _currentLife/_maxLife;
            }
        }

        public bool IsLive => CurrentLife > 0;

        public void StartNewGame()
        {
            _maxLife = 30;
            CurrentLife = 30;
        }

        public void HitMe(int value)
            => CurrentLife -= value;
    }
}