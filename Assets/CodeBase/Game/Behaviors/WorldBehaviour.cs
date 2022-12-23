using UnityEngine;

namespace CodeBase.Game.Behaviours
{
    public class WorldBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _world;

        public void Hide()
        {
            _world.SetActive(false);
        }

        public void Show()
        {
            _world.SetActive(true);
        }
    }
}