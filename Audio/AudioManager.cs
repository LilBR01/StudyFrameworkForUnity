using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource bgm = null;
    private float bgmValue = 1;
    private float soundValue = 1;
    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();

    public AudioManager()
    {
        MonoManager.Instance.AddUpdateListener(update);
    }

    private void update()
    {
        for(int i = soundList.Count-1; i>= 0; i--)
        {
            if(!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }

    public void PlayBgm(string name)
    {
        if(bgm == null)
        {
            GameObject obj = new GameObject("bgm");
            bgm = obj.AddComponent<AudioSource>();
        }

        ResourceManager.Instance.LoadAsyn<AudioClip>("Music/bgm/"+name, (clip)=>{
            bgm.clip = clip;
            bgm.loop = true;
            bgm.volume = bgmValue;
            bgm.Play();
        });
    }

    public void ChangeBgmValue(float v)
    {
        bgmValue = v;
        if(bgm == null) return;
        bgm.volume = bgmValue;
    }

    public void PauseBgm() 
    {
        if (bgm == null) return;
        bgm.Pause();
    }

    public void StopBgm()
    {
        if (bgm == null) return;
        bgm.Stop();
    }

    public void PlaySound(string name,bool isLoop,UnityAction<AudioSource> callback=null)
    {
        if(soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }
        AudioSource source = soundObj.AddComponent<AudioSource>();
        ResourceManager.Instance.LoadAsyn<AudioClip>("Music/Sounds/"+name, (clip)=>
        {
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            soundList.Add(source);
            if (callback != null)   callback(source);
        });
    }

    public void ChangeSoundValue(float value)
    {
        soundValue = value;
        for (int i = 0; i < soundList.Count; ++i)
        {
            soundList[i].volume = value;
        }
    }

    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
}
