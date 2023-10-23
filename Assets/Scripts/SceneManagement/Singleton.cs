using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour  where T : SingletonMono<T>
{
    private static T _instance;
    public static T Instance { get => _instance; }

    protected virtual void Awake()
    {
        if (_instance != null && gameObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
