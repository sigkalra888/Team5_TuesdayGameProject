using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.Log(t + "をアタッチしているGameObjectはありません");
                }
            }
            return instance;
        }
    }

    [SerializeField]
    private protected bool dontDestroy = true;

    protected virtual void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        if (!dontDestroy) { return; }
        DontDestroyOnLoad(this.gameObject);
    }
}