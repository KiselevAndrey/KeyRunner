using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Utility
{
    public class LocalPool : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _spawnedClones = new();
        [SerializeField] private List<GameObject> _despawnedClones = new();

        private List<GameObject> SpawnedClones => _spawnedClones;

        public int SpawnedCount => SpawnedClones.Count;

        #region Spawn
        public T Spawn<T>(T prefab) where T : Component
            => Spawn(prefab, Vector3.zero);

        public T Spawn<T>(T prefab, Vector3 localPosition) where T : Component
            => Spawn(prefab, localPosition, Quaternion.identity);

        public T Spawn<T>(T prefab, Vector3 localPosition, Quaternion localRotation) where T : Component
            => Spawn(prefab.gameObject, localPosition, localRotation).GetComponent<T>();

        public GameObject Spawn(GameObject gameObject)
            => Spawn(gameObject, Vector3.zero, Quaternion.identity);

        public GameObject Spawn(GameObject gameObject, Vector3 localPosition, Quaternion localRotation)
            => Spawn(gameObject, localPosition, localRotation, transform);

        public GameObject Spawn(GameObject gameObject, Vector3 localPosition, Quaternion localRotation, Transform parent)
        {
            GameObject clone;

            for (int i = _despawnedClones.Count - 1; i >= 0; i--)
            {
                clone = _despawnedClones[i];

                _despawnedClones.RemoveAt(i);

                if (clone != null)
                {
                    SpawnClone(clone, localPosition, localRotation, parent);

                    return clone;
                }
            }

            return CreateClone(gameObject, localPosition, localRotation, parent);
        }

        private void SpawnClone(GameObject clone, Vector3 localPosition, Quaternion localRotation, Transform parent)
        {
            SpawnedClones.Add(clone);

            Transform cloneTransform = clone.transform;
            cloneTransform.SetParent(null, false);
            cloneTransform.localPosition = localPosition;
            cloneTransform.localRotation = localRotation;
            cloneTransform.SetParent(parent, false);

            if (parent == null)
                UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(clone, UnityEngine.SceneManagement.SceneManager.GetActiveScene());

            clone.SetActive(true);
        }

        private GameObject CreateClone(GameObject gameObject, Vector3 localPosition, Quaternion localRotation, Transform parent)
        {
            GameObject clone = Instantiate(gameObject, localPosition, localRotation, parent);

            clone.transform.localPosition = localPosition;
            clone.transform.localRotation = localRotation;

            SpawnedClones.Add(clone);
            clone.SetActive(true);

            return clone;
        }
        #endregion Spawn

        #region Despawn
        public void DespawnAll()
        {
            for (int i = SpawnedCount - 1; i >= 0; i--)
                Despawn(SpawnedClones[i]);
        }

        public void Despawn(GameObject clone)
        {
            if (clone == null)
            {
                Debug.LogWarning("You're attempting to despawn a null gameObject", this);
                return;
            }

            TryDespawn(clone);
        }

        public void DespawnLast()
        {
            if(SpawnedCount > 0)
                TryDespawn(SpawnedClones[SpawnedCount - 1]);
        }

        private void TryDespawn(GameObject clone)
        {
            if (SpawnedClones.Remove(clone) == false)
            {
                Debug.LogWarning("You're attempting to despawn a GameObject that wasn't spawned from this pool, make sure your Spawn and Despawn calls match.", clone);
                return;
            }

            _despawnedClones.Add(clone);
            clone.SetActive(false);
            clone.transform.SetParent(transform);
        }
        #endregion Despawn

        private void OnDisable() =>
            DespawnAll();
    }
}