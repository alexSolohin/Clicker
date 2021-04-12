using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE
{
    GAME_START,
    GAME_PLAY,
    GAME_PAUSE,
    CREATE_LVLS,
    CREATE_ENEMY,
    STOP_RUN_ANIM,
    ATTACK,
};

public class EventManager : MonoBehaviour
{
    public static EventManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
        }
    }
    private static EventManager instance = null;
    
    public delegate void OnEvent(EVENT_TYPE eventType,
        Component sender,
        object Param = null);

    // Dict of listeners
    private Dictionary<EVENT_TYPE, List<OnEvent>>
        Listeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }

    }

    /// <summary>
    /// Add listener
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="listener"></param>
    public void AddListener(EVENT_TYPE eventType, OnEvent listener)
    {
        List<OnEvent> listenList = null;

        if (Listeners.TryGetValue(eventType, out listenList))
        {
            listenList.Add(listener);
            return;
        }

        listenList = new List<OnEvent>();
        listenList.Add(listener);
        Listeners.Add(eventType, listenList);
    }

    public void PostNotification(EVENT_TYPE eventType, Component sender, object param = null)
    {
        List<OnEvent> listenList = null;

        if (!Listeners.TryGetValue(eventType, out listenList))
        {
            return;
        }

        for (int i = 0; i < listenList.Count; i++)
        {
            if (!listenList[i].Equals(null))
            {
                listenList[i](eventType, sender, param);
            }
        }
    }

    public void RemoveEvent(EVENT_TYPE eventType)
    {
        Listeners.Remove(eventType);
    }

    public void RemoveRedundancies()
    {
        Dictionary<EVENT_TYPE, List<OnEvent>>
            TmpListeners = new Dictionary<EVENT_TYPE, List<OnEvent>>();

        foreach (KeyValuePair<EVENT_TYPE, List<OnEvent>> item in Listeners)
        {
            for (int i = item.Value.Count - 1; i >= 0 ; i--)
            {
                if (item.Value[i].Equals(null))
                {
                    item.Value.RemoveAt(i);
                }
            }

            if (item.Value.Count > 0)
            {
                TmpListeners.Add(item.Key, item.Value);
            }
        }

        Listeners = TmpListeners;
    }

    void OnLevelWasLoaded()
    {
        RemoveRedundancies();
    }
}
