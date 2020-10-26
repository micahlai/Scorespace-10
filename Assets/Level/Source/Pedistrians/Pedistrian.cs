using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pedistrian : MonoBehaviour
{
    public enum PedType { Jog, Walk};
    public PedType pedType;
    public float speed;
    public Material[] materials;
    public float targetZ;
    // Start is called before the first frame update
    void Start()
    {
        int r = Mathf.RoundToInt(Random.Range(0.5f, materials.Length - 0.51f));
        GetComponentInChildren<SkinnedMeshRenderer>().material = materials[r];
        if (pedType == PedType.Jog)
        {
            speed = 7;
        }
        else
        {
            speed = 3.5f;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        targetZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector3(transform.position.x + Time.deltaTime * -speed, transform.position.y, Mathf.Lerp(transform.position.z, targetZ, Time.deltaTime * 2));
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            targetZ -= 3;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Street"))
        {
            targetZ += 6;
        }
    }
}
