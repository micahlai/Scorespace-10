using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour
{
    public GameObject[] vechiles;
    public Vector2[] spawnPositions;
    public Quaternion rotate1;
    public Quaternion rotate2;
    public List<GameObject> currentVechiles;
    public float vechileSpeed = 5;
    public float spawnFrequency = 2;
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
            currentVechiles.Add(Instantiate(vechiles[r], new Vector3(FindObjectOfType<Car>().transform.position.x + 70, spawnPositions[Mathf.RoundToInt(Random.Range(-0.5f, spawnPositions.Length - 0.51f))].y, spawnPositions[0].x), rotate1));
        }
        else
        {
            currentVechiles.Add(Instantiate(vechiles[r], new Vector3(FindObjectOfType<Car>().transform.position.x - 70, spawnPositions[Mathf.RoundToInt(Random.Range(-0.5f, spawnPositions.Length - 0.51f))].y, spawnPositions[1].x), rotate2));
        }       

    }
}
