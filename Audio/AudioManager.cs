using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    //存储音频信息
    public List<AudioType> sounds = new List<AudioType>();
    //每一个音频剪辑的名称对应一个音频组件
    private Dictionary<string, AudioSource> audioDic = new Dictionary<string, AudioSource>();

    public AudioMixer mixer;

    private void Awake()
    {
    
    }

    //生成音频并初始化
    private void Start()
    {
        foreach(var sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);

            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;
            source.volume = sound.volume;
            source.outputAudioMixerGroup = sound.outputGroup;

            if(sound.playOnAwake)
                source.Play();
            
            audioDic.Add(sound.clip.name, source);
        }
    }

    //播放音频
    public static void PlayAudio(string name, bool isWait = false)
    {
        if(Instance.audioDic.ContainsKey(name))
        {
            Debug.LogWarning($"名为{name}音频不存在");
        }

        if(isWait)
        {
            if(Instance.audioDic[name].isPlaying)
                Instance.audioDic[name].Play();
        }
        else
            Instance.audioDic[name].Play();
    }

    //停止音频
    public static void StopAudio(string name)
    {
        if(Instance.audioDic.ContainsKey(name))
        {
            Debug.LogWarning($"名为{name}音频不存在");
        }

        Instance.audioDic[name].Stop();
    }

    //调整背景音乐音量
    public void SetBGMVolume(float value)
    {
        mixer.SetFloat("BGM", value);
    }

    //调整音效音量
    public void SetSFXVolume(float value)
    {
        mixer.SetFloat("SFX", value);
    }
}
