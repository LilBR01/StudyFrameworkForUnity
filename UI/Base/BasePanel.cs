using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel
{
    public UIType UIType{get; private set;}
    public UITool UITool{get; private set;}
    public UIManager UIManager{get; private set;}

    public BasePanel(UIType uiType)
    {
        UIType = uiType;
    }

    public void Initialize(UITool tool)
    {
        UITool = tool;
    }

    //UI进入时执行的操作，只会执行一次
    public virtual void OnEnter(){}

    //UI暂停时执行的操作
    public virtual void OnPause()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
    }

    //UI继续时执行的操作
    public virtual void OnResume()
    {
        UITool.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
    }

    //UI退出时执行的操作
    public virtual void OnExit()
    {
        UIManager.Instance.DestoryUI(UIType);
    }

    //显示一个面板
    public void Push(BasePanel panel) => UIManager?.Pop();

    //退出一个面板
    public void Pop() => UIManager?.Pop();
}
