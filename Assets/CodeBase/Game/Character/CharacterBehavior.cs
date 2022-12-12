using UnityEngine;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(CharacterMover))]
    public class CharacterBehavior : MonoBehaviour
    {
        [SerializeField] private Transform _character;

        private CharacterMover _mover;

        public void Init(Vector2 lastLetterPos)
        {
            _mover.Init(lastLetterPos, _character);
            _character.gameObject.SetActive(true);
        }

        public void NextPositionX(float xPos)
        {
            _mover.StartMoveToNextPointX(xPos);
        }

        public void Hide()
        {
            _character.gameObject.SetActive(false);
        }

        private void Awake()
        {
            _mover = GetComponent<CharacterMover>();
            Hide();
        }
    }
}