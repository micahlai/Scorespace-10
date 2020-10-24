using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public DirectionalControl.Direction pointDirection;
    public Transform arrow;
    public LineRenderer highlightAlign;
    public MeshRenderer[] arrowRenderers;
    public Color[] directionColorsMain;
    public Color[] directionColorsShade;


    public Vector3 position;
    
    public float smoothSpeed = 0.125f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (pointDirection == DirectionalControl.Direction.Up)
        {
            ChangeArrowColors(1);
            arrow.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0));
        }
        else if (pointDirection == DirectionalControl.Direction.Down)
        {
            ChangeArrowColors(0);
            arrow.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180));
        }
        else if (pointDirection == DirectionalControl.Direction.Right)
        {
            ChangeArrowColors(3);
            arrow.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90));
        }
        else if (pointDirection == DirectionalControl.Direction.Left)
        {
            ChangeArrowColors(2);
            arrow.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, -90));
        }
        highlightAlign.gameObject.SetActive(false);
        foreach (DirectionalControl d in FindObjectsOfType<DirectionalControl>())
        {
            if (d.pointDirection == DirectionalControl.Direction.Down && d.transform.position.z == position.z && d.transform.position.x > position.x)
            {
                highlightAlign.gameObject.SetActive(true);
                Debug.DrawLine(d.transform.position, position);
                highlightAlign.SetPosition(0, d.transform.position);
                highlightAlign.SetPosition(1, position);
            } if (d.pointDirection == DirectionalControl.Direction.Up && d.transform.position.z == position.z && d.transform.position.x < position.x)
            {
                highlightAlign.gameObject.SetActive(true);
                Debug.DrawLine(d.transform.position, position);
                highlightAlign.SetPosition(0, d.transform.position);
                highlightAlign.SetPosition(1, position);
            }
             if (d.pointDirection == DirectionalControl.Direction.Right && d.transform.position.x == position.x && d.transform.position.z > position.z)
            {
                highlightAlign.gameObject.SetActive(true);
                Debug.DrawLine(d.transform.position, position);
                highlightAlign.SetPosition(0, d.transform.position);
                highlightAlign.SetPosition(1, position);
            }
             if (d.pointDirection == DirectionalControl.Direction.Left && d.transform.position.x == position.x && d.transform.position.z < position.z)
            {
                highlightAlign.gameObject.SetActive(true);
                Debug.DrawLine(d.transform.position, position);
                highlightAlign.SetPosition(0, d.transform.position);
                highlightAlign.SetPosition(1, position);
            }


            
        }
        Car car = FindObjectOfType<Car>();
        Vector3 carPos = new Vector3(car.transform.position.x, 0, car.transform.position.z);

        if (car.m_direction == DirectionalControl.Direction.Down && carPos.z == position.z && carPos.x > position.x)
        {
            highlightAlign.gameObject.SetActive(true);
            Debug.DrawLine(carPos, position);
            highlightAlign.SetPosition(0, carPos);
            highlightAlign.SetPosition(1, position);
        }
        if (car.m_direction == DirectionalControl.Direction.Up && carPos.z == position.z && carPos.x < position.x)
        {
            highlightAlign.gameObject.SetActive(true);
            Debug.DrawLine(carPos, position);
            highlightAlign.SetPosition(0, carPos);
            highlightAlign.SetPosition(1, position);
        }
        if (car.m_direction == DirectionalControl.Direction.Right && carPos.x == position.x && carPos.z > position.z)
        {
            highlightAlign.gameObject.SetActive(true);
            Debug.DrawLine(carPos, position);
            highlightAlign.SetPosition(0, carPos);
            highlightAlign.SetPosition(1, position);
        }
        if (car.m_direction == DirectionalControl.Direction.Left && carPos.x == position.x && carPos.z < position.z)
        {
            highlightAlign.gameObject.SetActive(true);
            Debug.DrawLine(carPos, position);
            highlightAlign.SetPosition(0, carPos);
            highlightAlign.SetPosition(1, position);
        }
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, position, smoothSpeed);
    }

    void ChangeArrowColors(int i)
    {
        foreach (MeshRenderer m in arrowRenderers)
        {
            m.material.SetColor("Color_F689E8B9", directionColorsMain[i]);
            m.material.SetColor("Color_28180829", directionColorsShade[i]);
        }
    }
}
