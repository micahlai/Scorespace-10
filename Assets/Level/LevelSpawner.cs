using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public LevelObject[] objects;
    public float[] respawn;
    public float[] relationToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        respawn = new float[objects.Length ];
        relationToPlayer = new float[objects.Length];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            relationToPlayer[i] = FindObjectOfType<Car>().transform.position.x - respawn[i];
            if(relationToPlayer[i] >= objects[i].spawnFrequency)
            {
                respawn[i] += objects[i].spawnFrequency;
                Instantiate(objects[i].prefab, new Vector3(FindObjectOfType<Car>().transform.position.x + 80, objects[i].yLevel ,Random.Range(objects[i].spawnRange.x, objects[i].spawnRange.y)), Quaternion.identity);
            }
        }
    }
}
