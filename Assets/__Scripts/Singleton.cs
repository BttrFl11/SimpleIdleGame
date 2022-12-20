using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance => GetInstance();

    private static T GetInstance()
    {
        if (_instance != null)
            return _instance;

        var instances = FindObjectsOfType<T>();
        var count = instances.Length;
        if (count > 0)
        {
            var instance = instances[0];
            for (int i = 1; i < count; i++)
                Destroy(instances[i].gameObject);

            return _instance = instance;
        }
        else
        {
            var instance = new GameObject(typeof(T).Name).AddComponent<T>();
            return _instance = instance;
        }
    }
}
