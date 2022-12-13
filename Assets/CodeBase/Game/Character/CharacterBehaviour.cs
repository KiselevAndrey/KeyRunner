using UnityEngine;

namespace CodeBase.Game.Character
{
    [RequireComponent(typeof(CharacterMover))]
    public class CharacterBehaviour : BaseBehaviour
    {
        public Vector3 Position => Body.position;

        protected override void OnEndMoving()
        {
        }
    }
}