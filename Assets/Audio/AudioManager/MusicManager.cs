using UnityEngine.Audio;
using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Music[] music;
    public AudioMixer master;
    void Awake()
    {

        foreach (Music m in music)
        {
            foreach(MusicPart mp in m.parts)
            {
                mp.source = gameObject.AddComponent<AudioSource>();
                mp.source.clip = mp.clip;
                mp.source.loop = m.loop;
                mp.source.outputAudioMixerGroup = mp.mixerGroup;
                mp.source.volume = mp.volume * m.volume;
                if (m.PlayOnAwake)
                {
                    mp.source.Play();
                }
            }
        }
    }
    public void SetVolume(string musicInstrument, float value)
    {
        master.SetFloat(musicInstrument, value);
    }
    public float GetVolume(string musicInstrument)
    {
        float value;
        master.GetFloat(musicInstrument, out value);
        return value;
    }
    
}
