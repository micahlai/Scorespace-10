using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Street : MonoBehaviour
{
    public GameObject[] vechiles;
    public Vector2[] spawnPositions;
    public Quaternion rotate1;
    public Quaternion rotate2;
    public List<GameObject> currentVechiles;
    public float vechileSpeed = 5;
    public float spawnFrequency = 2;
    public AudioClip honk;
    public AudioMixerGroup carMixerGroup;
    [Range(0,5)]
    public float chanceOfHonk;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = spawnFrequency; 
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject g in currentVechiles)
        {
            g.transform.Translate(Vector3.forward * vechileSpeed * Time.deltaTime);
            if(Vector3.Distance(FindObjectOfType<Car>().transform.position, g.transform.position) < 20)
            {
                if(Random.Range(0, 5) < chanceOfHonk && g.GetComponent<AudioSource>().enabled)
                {
                    g.GetComponent<AudioSource>().Play();
                    g.GetComponent<AudioSource>().enabled = false;
                }
            }
        }
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = spawnFrequency;
            startCar();
        }
    }
    public void startCar()
    {
        int r = Mathf.RoundToInt(Random.Range(-0.5f, vechiles.Length - 0.51f));
        if (Random.value < 0.5)
        {
            GameObject g = Instantiate(vechiles[r], new Vector3(FindObjectOfType<Car>().transform.position.x + 70, spawnPositions[Mathf.RoundToInt(Random.Range(-0.5f, spawnPositions.Length - 0.51f))].y, spawnPositions[0].x), rotate1);
            currentVechiles.Add(g);
            AudioSource s = g.AddComponent<AudioSource>();
            s.clip = honk;
            s.volume = s.volume * (1f + Random.Range(-1 / 2f, 1 / 2f));
            s.pitch = s.pitch * (1f + Random.Range(-1 / 2f, 1 / 2f));
            s.outputAudioMixerGroup = carMixerGroup;
            s.spatialBlend = 1;
        }
        else
        {
            GameObject g = Instantiate(vechiles[r], new Vector3(FindObjectOfType<Car>().transform.position.x - 70, spawnPositions[Mathf.RoundToInt(Random.Range(-0.5f, spawnPositions.Length - 0.51f))].y, spawnPositions[1].x), rotate2);
            currentVechiles.Add(g);
            AudioSource s = g.AddComponent<AudioSource>();
            s.clip = honk;
            s.volume = s.volume * (1f + Random.Range(-1 / 2f, 1 / 2f));
            s.pitch = s.pitch * (1f + Random.Range(-1 / 2f, 1 / 2f));
            s.outputAudioMixerGroup = carMixerGroup;
            s.spatialBlend = 1;
        }
        

    }
}
