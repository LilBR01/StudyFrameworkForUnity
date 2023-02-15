using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioType : MonoBehaviour
{
    public AudioClip clip;
    public AudioMixerGroup outputGroup;

    [Range(0,1)]
    public float volume = 1;

    public bool playOnAwake;

    public bool loop;
}
