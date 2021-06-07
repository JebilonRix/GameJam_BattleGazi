using UnityEngine;
using UnityEngine.Audio;
using System;

[RequireComponent(typeof(AudioSource))]
public class SesEfendisi : MonoBehaviour
{
    public AudioMixerGroup mixer;
    public SesEfendisi_Kaynak[] sounds;
    public static SesEfendisi instance;
  
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (SesEfendisi_Kaynak s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void PlayAudio(int index)
    {
        // FindObjectOfType<SesEfendisi>().PlayAudio(number);

        SesEfendisi_Kaynak s = Array.Find(sounds, sound => sound.Number == index);

        if (s == null)
        {
            Debug.LogWarning(index + " bulunamadı.");
            return;
        }
        if (!s.source.isPlaying)
        {
            s.source.Play();
            s.source.outputAudioMixerGroup = mixer;
        }

    }
    public void PlayRandomAudio(int startNumber, int lastNumber)
    {
        // FindObjectOfType<SesEfendisi>().PlayRandomAudio(number);

        int a = UnityEngine.Random.Range(startNumber, lastNumber + 1);

        SesEfendisi_Kaynak s = Array.Find(sounds, sound => sound.Number == a);

        if (s == null)
        {
            Debug.LogWarning(a + " bulunamadı.");
            return;
        }

        if (!s.source.isPlaying)
        {
            s.source.Play();
            s.source.outputAudioMixerGroup = mixer;
        }
    }
}