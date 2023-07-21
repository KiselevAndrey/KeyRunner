using System;
using UnityEngine;

namespace CodeBase.Game.Character
{
    public class EnemyHunting : MonoBehaviour
    {
        private Action _endHuntingAction;

        public void StartHunting(Action endAction)
        {
            _endHuntingAction = endAction;

            Invoke(nameof(EndHunting), 2);
        }

        private void EndHunting()
            =>_endHuntingAction.Invoke();
    }
}