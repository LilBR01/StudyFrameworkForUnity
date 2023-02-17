using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; 

public class sceneManager : Singleton<sceneManager>
{
    public void LoadScene(string name, UnityAction func)
    {
        SceneManager.LoadScene(name);
        func();
    }

    public void LoadSceneAsyn(string name, UnityAction func)
    {
        MonoManager.Instance.mStartCoroutine(ReallyLoadSceneAsyn(name, func));
    }

    private IEnumerator ReallyLoadSceneAsyn(string name, UnityAction func)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        while(!ao.isDone)
        {
            EventCenter.Instance.EventTrigger("Loading", ao.progress);
            yield return ao.progress;
        }

        func();
    }
}
