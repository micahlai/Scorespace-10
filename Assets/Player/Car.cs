using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed;
    public DirectionalControl.Direction m_direction;
    
    // Start is called before the first frame update
    void Start()
    {
        m_direction = DirectionalControl.Direction.Up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (m_direction == DirectionalControl.Direction.Up || m_direction == DirectionalControl.Direction.Down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z));
        }else if (m_direction == DirectionalControl.Direction.Left || m_direction == DirectionalControl.Direction.Right)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z);
        }
    }
    public void TurnCar(DirectionalControl.Direction direction)
    {
        m_direction = direction;
        if(direction == DirectionalControl.Direction.Up)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0));
        }else if (direction == DirectionalControl.Direction.Down)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180));
        }else if (direction == DirectionalControl.Direction.Right)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90));
        }
        else if (direction == DirectionalControl.Direction.Left)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, -90));
        }
    }
}

