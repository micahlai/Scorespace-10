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
            }
            m.currentVolume = m.volume;
        }
    }
    private void Start()
    {
        foreach (Music m in music)
        {
            foreach (MusicPart mp in m.parts)
            {
                if (m.PlayOnAwake)
                {
                    mp.source.Play();
                }
            }
        }
        
    }
    private void Update()
    {
        foreach(Music m in music)
        {
            m.currentVolume = Mathf.Lerp(m.currentVolume, m.volume, Time.deltaTime * 2);
            foreach(MusicPart mp in m.parts)
            {
                mp.source.volume = mp.volume * m.currentVolume;
            }
            
        }
    }
    public void ChangeMusicVolume(string musicName, float value)
    {
        foreach(Music m in music)
        {
            if(m.name == musicName)
            {
                m.volume = value;
            }
        }
    }
    public float GetMusicVolume(string musicName)
    {
        foreach (Music m in music)
        {
            if (m.name == musicName)
            {
                
                return m.volume;
            }
        }
        return 0;
    }
    public void ChangeInstrumentVolume(string musicName, string instrumentName, float value)
    {
        foreach (Music m in music)
        {
            if (m.name == musicName)
            {
                foreach (MusicPart mp in m.parts)
                {
                    if(mp.name == instrumentName)
                    {
                        mp.volume = value;
                    }
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
