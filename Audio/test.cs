using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class test : MonoBehaviour
{
    [System.Serializable]
    public class Sound : MonoBehaviour
    {
        [Header("音频")]
        public AudioClip clip;
        public AudioMixerGroup outputGroup;

        [Range(0,1)]
        public float volume = 1;

        public bool playOnAwake;

        public bool loop;
    }
    //存储音频信息
    public List<AudioMixerGroup> sounds;
    private Dictionary<string, AudioSource> audioDic;
}
