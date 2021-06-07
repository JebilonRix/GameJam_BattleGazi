﻿using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SesEfendisi_Kaynak
{
    public int Number;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool loop;
    [HideInInspector] public AudioSource source;
}