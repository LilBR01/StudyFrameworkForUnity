using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : Singleton<EventCenter>
{
    private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();

    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if(eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo<T>(action));
        }
    }

    public void AddEventListener(string name, UnityAction action)
    {
        if(eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }

    public void EventTrigger<T>(string name, T info)
    {
        if(eventDic.ContainsKey(name))
        {
            //以name为键调用委托
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
        }
    }

    public void EventTrigger(string name)
    {
        if(eventDic.ContainsKey(name))
        {
            //以name为键调用委托
            (eventDic[name] as EventInfo).actions?.Invoke();
        }
    }

    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if(eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if(eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }

    public void Clear()
    {
        eventDic.Clear();
    }
}
