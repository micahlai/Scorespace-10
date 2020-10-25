using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject[] trees;
    public Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        int r = Mathf.RoundToInt(Random.Range(-0.5f, trees.Length - 0.51f));
        GameObject g = Instantiate(trees[r], transform);
        g.transform.position = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
