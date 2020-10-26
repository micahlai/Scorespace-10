using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class Loading : MonoBehaviour
{
    public GameObject loadingScreen;
    public Animator anim;
    public PauseUI pause;
    public UnityEvent loadingEvent;


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }
    public void LoadSameLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex));
    }


    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        
        loadingEvent.Invoke();
        if (!(SceneManager.GetActiveScene().buildIndex == 0))
        {
            pause.boolPause(false);
        }

        
        if(sceneIndex == 2)
        {
            FindObjectOfType<MusicManager>().ChangeMusicVolume("A", 0);
            FindObjectOfType<MusicManager>().ChangeMusicVolume("B", 1);
        }
        else
        {
            FindObjectOfType<MusicManager>().ChangeMusicVolume("A", 1);
            FindObjectOfType<MusicManager>().ChangeMusicVolume("B", 0);
        }

        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().color = new Color(0,0,0,0);
        if (pause != null)
        {
            pause.canPause = false;
        }
        Debug.Log("Switch scene to " + sceneIndex);
        anim.SetTrigger("Out");
        yield return new WaitForSeconds(2);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);



        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            
            yield return null;
        }
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        loadingScreen.SetActive(false);
        if (pause != null)
        {
            pause.canPause = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(wait(1));
    }

    // Update is called once per frame
    public void Quit()
    {
        Application.Quit();
    }
}
