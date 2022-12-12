using UnityEngine;

namespace CodeBase.Game.Character
{
    public class CharacterMover : MonoBehaviour
    {
        private Transform _character;

        public void Init(Vector2 startPosition, Transform character)
        {
            _character = character;
            _character.position = startPosition;
        }

        public void StartMoveToNextPointX(float x)
        {
            var finish = _character.position;
            finish.x = x;
            _character.position = finish;
        }
    }
}