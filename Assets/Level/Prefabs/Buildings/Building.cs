using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float space = 10;
    public bool hit;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Floor"))
        {
            if (g.GetComponent<Collider>() != null)
            {
                Physics.IgnoreCollision(g.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            transform.Translate(Vector3.right * 6);
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Floor"))
        {
            Physics.IgnoreCollision(g.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Building"))
        {
            if (!hit)
            {
                hit = true;
                transform.Translate(Vector3.left * space);
            }
            
        }
    }
}
