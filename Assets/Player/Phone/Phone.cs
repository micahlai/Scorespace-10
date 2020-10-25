using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public Transform handJoint;
    public Vector3 offset;
    public Vector3 rotOffset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = handJoint.position + offset;
        transform.rotation = Quaternion.Euler(handJoint.rotation.eulerAngles + rotOffset);
    }
}
