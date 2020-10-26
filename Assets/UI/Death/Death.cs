using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public string[] DeathMessages;
    public Text DeathText;
    public Text scoreText;
    public Animator anim;
    bool Played;
    public string score;
    public GameObject ui;
    public bool Complete;

    private void Start()
    {
        ui.SetActive(false);
        Complete = false;
    }
    private void Update()
    {
        
    }

    public void Die()
    {
        if (!Played)
        {
            ui.SetActive(true);
            
            FindObjectOfType<MusicManager>().ChangeMusicVolume("A", 0);
            FindObjectOfType<MusicManager>().ChangeMusicVolume("B", 0);
            FindObjectOfType<AudioManager>().Play("End");
            scoreText.text = "Your score: " + score;
            int r = Mathf.RoundToInt(Random.Range(0.5f, DeathMessages.Length - 0.51f));
            if (Complete)
            {
                DeathText.text = "Course Completed!";
            }
            else
            {
                DeathText.text = DeathMessages[r];
            }
            FindObjectOfType<ScoreSystem>().gameObject.SetActive(false);
            anim.SetTrigger("Death");
            Played = true;
        }
    }
}
