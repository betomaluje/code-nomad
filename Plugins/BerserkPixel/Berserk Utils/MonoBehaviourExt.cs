using UnityEngine;

public static class MonoBehaviourExt
{
    public static bool TryGetComponentInChildren<T>(this MonoBehaviour mono, out T component)
    {
        component = mono.GetComponentInChildren<T>();
        return component != null;
    }

    public static bool TryGetComponentInChildren<T>(this GameObject go, out T component)
    {
        component = go.GetComponentInChildren<T>();
        return component != null;
    }
}