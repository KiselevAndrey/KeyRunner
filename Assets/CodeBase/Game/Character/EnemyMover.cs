using CodeBase.Settings.Singleton;
using UnityEngine;

namespace CodeBase.Game.Character
{
    public class EnemyMover : CharacterMover
    {
        [SerializeField, Range(0, 0.2f)] private float _speedEndedLevelMultiplier = 0.1f;

        protected override float Speed => base.Speed + PlayerInfoSO.LevelsEnded * _speedEndedLevelMultiplier;
    }
}