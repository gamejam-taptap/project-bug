using UnityEngine;

public class BaseManager<T> : MonoBehaviour where T : class
{
    public static T Instance;
    
    protected void SetDoNotDestroyOnLoad()
    {
        if (Instance != null && Instance != this as T)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
    }
}