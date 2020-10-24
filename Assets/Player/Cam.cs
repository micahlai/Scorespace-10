using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    Vector3 player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float horizontalScroll;
    public bool locked;
    public float scrollScale = 3;
    // Start is called before the first frame update
    void Start()
    {
        horizontalScroll = 0;
        locked = true;
    }
    void FixedUpdate()
    {
        if (locked)
        {
            player = new Vector3(FindObjectOfType<Car>().transform.position.x, FindObjectOfType<Car>().transform.position.y, 0);
        }
        else
        {
            horizontalScroll += Input.mouseScrollDelta.y * scrollScale;
        }
        if(Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            locked = false;
        }
        
        Vector3 o = offset + new Vector3(horizontalScroll, 0, 0);
        Vector3 desiredPosition = player + o;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            locked = true;
            horizontalScroll = 0;
        }
    }
}
