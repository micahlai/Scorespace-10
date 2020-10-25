using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneScreen : MonoBehaviour
{
    public Sprite[] arrows;
    public Color[] colors;
    public int index;
    DirectionalControl nextControl;
    public Image arrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (DirectionalControl d in FindObjectsOfType<DirectionalControl>())
        {
            Car car = FindObjectOfType<Car>();
            Vector3 carPos = new Vector3(car.transform.position.x, 0, car.transform.position.z);
            Vector3 position = d.transform.position;

            if (car.m_direction == DirectionalControl.Direction.Down && carPos.z == position.z && carPos.x > position.x)
            {
                Debug.DrawLine(carPos, position);
                if(nextControl == null)
                {
                    nextControl = d;
                }
                else
                {
                    if(Vector3.Distance(nextControl.transform.position, carPos) > Vector3.Distance(d.transform.position, carPos))
                    {
                        nextControl = d;
                    }
                }
            }
            if (car.m_direction == DirectionalControl.Direction.Up && carPos.z == position.z && carPos.x < position.x)
            {
                Debug.DrawLine(carPos, position);
                if (nextControl == null)
                {
                    nextControl = d;
                }
                else
                {
                    if (Vector3.Distance(nextControl.transform.position, carPos) > Vector3.Distance(d.transform.position, carPos))
                    {
                        nextControl = d;
                    }
                }
            }
            if (car.m_direction == DirectionalControl.Direction.Right && carPos.x == position.x && carPos.z > position.z)
            {
                Debug.DrawLine(carPos, position);
                if (nextControl == null)
                {
                    nextControl = d;
                }
                else
                {
                    if (Vector3.Distance(nextControl.transform.position, carPos) > Vector3.Distance(d.transform.position, carPos))
                    {
                        nextControl = d;
                    }
                }
            }
            if (car.m_direction == DirectionalControl.Direction.Left && carPos.x == position.x && carPos.z < position.z)
            {
                Debug.DrawLine(carPos, position);
                if (nextControl == null)
                {
                    nextControl = d;
                }
                else
                {
                    if (Vector3.Distance(nextControl.transform.position, carPos) > Vector3.Distance(d.transform.position, carPos))
                    {
                        nextControl = d;
                    }
                }
            }
        }
        if (nextControl == null)
        {
            index = 4;
        }
        else
        {
            if (nextControl.pointDirection == DirectionalControl.Direction.Left)
            {
                index = 0;
            }
            else if (nextControl.pointDirection == DirectionalControl.Direction.Up)
            {
                index = 1;
            }
            else if (nextControl.pointDirection == DirectionalControl.Direction.Down)
            {
                index = 2;
            }
            else if (nextControl.pointDirection == DirectionalControl.Direction.Right)
            {
                index = 3;
            }
        }
        arrow.sprite = arrows[index];
        arrow.color = colors[index];
    }
}
