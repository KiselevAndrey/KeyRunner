using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(CharacterMover))]
    public class CharacterBehaviour : BaseBehaviour
    {
        public Vector3 Position => Body.position;

        public event UnityAction EndMoving;

        protected override void OnAwake()
        {
            Mover = GetComponent<CharacterMover>();
        }

        protected override void OnEndMoving()
        {
            base.OnEndMoving();

            EndMoving?.Invoke();
        }
    }
}