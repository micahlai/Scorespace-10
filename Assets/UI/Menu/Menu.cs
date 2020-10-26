using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public float melodyVolume;
    float melodyVolumeTarget;
    public GameObject PlayScreen;
    
    
    // Start is called before the first frame update
    void Start()
    {
        melodyVolume = 0;
        FindObjectOfType<Loading>().loadingEvent.AddListener(increaseVolume);
    }

    // Update is called once per frame
    void Update()
    {
        melodyVolume = Mathf.Lerp(melodyVolume, melodyVolumeTarget, Time.deltaTime * 2);
        FindObjectOfType<MusicManager>().ChangeInstrumentVolume("A", "Melody", melodyVolume);
    }
    public void MenuBlip()
    {
        FindObjectOfType<AudioManager>().Play("MenuBlip");
    }
    public void increaseVolume()
    {
        melodyVolumeTarget = 1;
    }
    public void Play()
    {
        if(FindObjectOfType<AcrossSceneTransfer>().data[0] == 1)
        {
            FindObjectOfType<Loading>().LoadLevel(1);
        }
        else
        {
            PlayScreen.SetActive(true);
        }
    }
}
