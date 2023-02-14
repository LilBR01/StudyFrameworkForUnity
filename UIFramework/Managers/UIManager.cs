using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Dictionary<UIType, GameObject> dicUI = new Dictionary<UIType, GameObject>();
    private Stack<BasePanel> stackPanel = new Stack<BasePanel>();
    private BasePanel panel;
    private UIManager(){}

    //获取一个UI对象
    public GameObject GetSingleUI(UIType type)
    {
        GameObject parent = GameObject.Find("Canvas");
        if(!parent)
        {
            Debug.LogError("Canvas不存在");
            return null;
        }

        if(dicUI.ContainsKey(type))
            return dicUI[type];
        
        GameObject ui = GameObject.Instantiate(Resources.Load<GameObject>(type.Path), parent.transform);
        ui.name = type.name;
        dicUI.Add(type, ui);
        return ui;
    }

    //销毁一个UI对象
    public void DestoryUI(UIType type)
    {
        if(dicUI.ContainsKey(type))
        {
            GameObject.Destroy(dicUI[type]);
            dicUI.Remove(type);
        }
    }

    //UI的入栈操作，此操作会显示一个面板
    public void Push(BasePanel nextPanel)    
    {
        if(stackPanel.Count > 0)
        {
            panel = stackPanel.Peek();
            panel.OnPause();
        }
        stackPanel.Push(nextPanel);
        GameObject panelGo = GetSingleUI(nextPanel.UIType);
        nextPanel.Initialize(new UITool(panelGo));
        nextPanel.OnEnter();
    }

    //执行面板的出栈操作，此操作会执行面板的OnExit方法
    public void Pop()
    {
        if(stackPanel.Count > 0)
            stackPanel.Pop().OnExit();
        
        //弹出后的栈顶的panel继续
        if(stackPanel.Count > 0)
            stackPanel.Peek().OnResume();
    }

    //执行所有面板的onExit()
    public void PopAll()
    {
        while(stackPanel.Count > 0)
            stackPanel.Pop().OnExit();
    }
}
