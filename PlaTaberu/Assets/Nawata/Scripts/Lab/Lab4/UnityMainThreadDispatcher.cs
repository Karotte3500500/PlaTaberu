using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<Action> executionQueue = new Queue<Action>();

    public void Update()
    {
        lock (executionQueue)
        {
            while (executionQueue.Count > 0)
            {
                executionQueue.Dequeue().Invoke();
            }
        }
    }

    public void Enqueue(Action action)
    {
        lock (executionQueue)
        {
            executionQueue.Enqueue(action);
        }
    }

    private static UnityMainThreadDispatcher instance = null;

    public static bool Exists()
    {
        return instance != null;
    }

    public static UnityMainThreadDispatcher Instance()
    {
        if (!Exists())
        {
            throw new Exception("UnityMainThreadDispatcherÇ™ë∂ç›ÇµÇ‹ÇπÇÒÅB");
        }
        return instance;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public static UnityMainThreadDispatcher CreateInstance()
    {
        if (Exists())
        {
            return instance;
        }
        GameObject obj = new GameObject("UnityMainThreadDispatcher");
        instance = obj.AddComponent<UnityMainThreadDispatcher>();
        DontDestroyOnLoad(obj);
        return instance;
    }
}
