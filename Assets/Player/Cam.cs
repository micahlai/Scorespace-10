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
    public Vector2 maxScroll;
    float lockX;
    public Transform pelvis;
    // Start is called before the first frame update
    void Start()
    {
        horizontalScroll = 0;
        locked = true;
    }
    void FixedUpdate()
    {
        horizontalScroll += Input.mouseScrollDelta.y * scrollScale;
        if (locked)
        {
            if (FindObjectOfType<Car>().dead)
            {
                player = pelvis.position;
            }
            else
            {
                player = new Vector3(FindObjectOfType<Car>().transform.position.x, FindObjectOfType<Car>().transform.position.y, 0);
            }
        }
        else
        {
            if(transform.position.x - FindObjectOfType<Car>().transform.position.x < maxScroll.x || transform.position.x - FindObjectOfType<Car>().transform.position.x > maxScroll.y)
            {
                locked = true;
                horizontalScroll = 0;
            }
        }
        if (horizontalScroll + player.x > maxScroll.y + FindObjectOfType<Car>().transform.position.x)
        {
            horizontalScroll = maxScroll.y;
        }
        else if (horizontalScroll + player.x < maxScroll.x + FindObjectOfType<Car>().transform.position.x)
        {
            horizontalScroll = maxScroll.x;
        }
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            locked = false;
            lockX = transform.position.x;
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
