using UnityEngine;

public class UITool
{
    GameObject activePanel;
    public UITool(GameObject Panel)
    {
        activePanel = Panel;
    }

    //给当前的活动面板获取或者添加一个组件
    public T GetOrAddComponent<T>() where T : Component
    {
        if(activePanel.GetComponent<T>() == null)
            activePanel.AddComponent<T>();
        return activePanel.GetComponent<T>();
    }

    //根据名称查找一个子对象
    public GameObject FindChildGameObject(string name)
    {
        Transform[] trans = activePanel.GetComponentsInChildren<Transform>();

        foreach(Transform item in trans)
        {
            if(item.name == name)
                return item.gameObject;
        }
        Debug.LogWarning($"{activePanel.name}里找不到{name}的子对象");
        return null;
    }

    //根据名称获取一个子对象的组件
    public T GetOrAddComponentInChildren<T>(string name) where T : Component
    {
        GameObject child = FindChildGameObject(name);
        if(child)
        {
            if(child.GetComponent<T>() == null)
                child.AddComponent<T>();
            return child.GetComponent<T>();
        }
        return null;
    }
}
