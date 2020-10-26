using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseUI : MonoBehaviour
{
    public bool canPause = false;
    public bool isPaused = false;
    public GameObject pauseUI;
    public AudioMixer audioGroup;
    public int lowPassNormal = 5000;
    public int lowPassPaused = 150;


    private void Start()
    {
        boolPause(false);

        audioGroup.SetFloat("PauseLowpass", lowPassNormal);
    }
    // Update is called once per frame
    void Update()
    {
        if (canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                if (isPaused)
                {
                    pauseUI.SetActive(true);
                    Time.timeScale = 0;
                    FindObjectOfType<AudioManager>().Play("Pause1");
                    audioGroup.SetFloat("PauseLowpass", lowPassPaused);
                }
                else
                {
                    pauseUI.SetActive(false);
                    Time.timeScale = 1;
                    FindObjectOfType<AudioManager>().Play("Pause2");
                    audioGroup.SetFloat("PauseLowpass", lowPassNormal);
                }
            }
        }
        else
        {
            isPaused = false;
        }
    }
    
    public void boolPause(bool pauser)
    {
        isPaused = pauser;
        if (isPaused)
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
            audioGroup.SetFloat("PauseLowpass", lowPassPaused);

        }
        else
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
            audioGroup.SetFloat("PauseLowpass", lowPassNormal);
        }
    }
}
