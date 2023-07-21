using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static string _assetpath = typeof(T).Name;

    private static T _instance = null;

    protected static T Instance => _instance ??= Resources.Load<T>(_assetpath);

#if UNITY_EDITOR
    private void Awake()
    {
        var instances = Resources.FindObjectsOfTypeAll<T>();
        var count = instances.Length;

        if (count > 1)
        {
            Debug.Log($"[{typeof(T)}]: There should never be more than one in the project, but [{count}] were found. The first instance found will be used, and all others will be destroyed.");

            for (int i = 1; i < count; i++)
                DestroyImmediate(instances[i], true);
        }

        OnAwake();
    }
#endif

    protected virtual void OnAwake() { }
}