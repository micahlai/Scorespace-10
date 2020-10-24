using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = FindObjectOfType<Car>().transform.position;
        transform.position = new Vector3(pos.x, startPos.y, startPos.z);
    }
}
