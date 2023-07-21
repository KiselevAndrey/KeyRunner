using CodeBase.Settings.Singleton;
using UnityEngine;

namespace CodeBase.Game.Character
{
    public class EnemyMover : CharacterMover
    {
        private readonly float _minSpeed = 0.05f;

        [SerializeField, Range(0, 0.2f)] private float _speedEndedRoundMultiplier = 0.1f;
        [SerializeField, Range(0, 0.5f)] private float _speedCaughtMultiplier = 0.3f;

        protected override float Speed => Mathf.Max(
            base.Speed + PlayerInfoSO.RoundsEnded * _speedEndedRoundMultiplier - PlayerInfoSO.CaughtTimes * _speedCaughtMultiplier, 
            _minSpeed);
    }
}