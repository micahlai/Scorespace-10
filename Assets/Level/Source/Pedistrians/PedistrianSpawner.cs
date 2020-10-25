using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedistrianSpawner : MonoBehaviour
{
    public GameObject[] people;
    public Vector2 freq;
    public Vector2 zPos;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = GetFreq(freq);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 0)
        {
            Instantiate(people[Mathf.RoundToInt(Random.Range(-0.5f, people.Length - 0.5f))], new Vector3(FindObjectOfType<Car>().transform.position.x + 50, 0, GetFreq(zPos)), Quaternion.identity);
            timer = GetFreq(freq);
        }
        timer -= Time.deltaTime;
    }
    float GetFreq(Vector2 range)
    {
        return Random.Range(range.x, range.y);
    }
}
