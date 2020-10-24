using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public GameObject directionPrefab;
    public DirectionalControl.Direction currentDirection;
    public Highlight highlight;
    public float[] values;
    [HideInInspector]
    public static Placement main;
    // Start is called before the first frame update
    void Start()
    {
        highlight = FindObjectOfType<Highlight>();
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<DirectionSelection>().status = values;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                highlight.position = NearestGridPosition(hit.point);
                highlight.pointDirection = currentDirection;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<FloatingColectable>() != null)
                {
                    hit.collider.GetComponent<FloatingColectable>().OnCollected.Invoke();
                }
                else if(hit.collider.gameObject.CompareTag("Floor"))
                {
                    if (values[IntFromDirection(currentDirection)] > 0)
                    {
                        DirectionalControl d = PlaceDirectionalControl(directionPrefab, highlight.transform.position);
                        if (d != null)
                        {
                            d.pointDirection = currentDirection;
                            values[IntFromDirection(currentDirection)] -= 1;

                        }
                    }
                    else
                    {
                        FindObjectOfType<DirectionSelection>().Error(IntFromDirection(currentDirection));
                    }
                }
            }
               
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeSelctionDirection(DirectionalControl.Direction.Up);
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeSelctionDirection(DirectionalControl.Direction.Down);
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeSelctionDirection(DirectionalControl.Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeSelctionDirection(DirectionalControl.Direction.Right);
        }
    }
    public void ChangeSelctionDirection(DirectionalControl.Direction d)
    {
        currentDirection = d;
    }
    public DirectionalControl PlaceDirectionalControl(GameObject prefab, Vector3 position)
    {
        bool existingControl = CheckIfExisting(position);
        if (!existingControl)
        {
            DirectionalControl d = Instantiate(directionPrefab, NearestGridPosition(position), Quaternion.identity).GetComponent<DirectionalControl>();
            return d;
        }
        else
        {
            return null;
        }

        
    }
    public bool CheckIfExisting(Vector3 pos)
    {
        bool existingControl = false;
        foreach (DirectionalControl dc in FindObjectsOfType<DirectionalControl>())
        {
            if (dc.transform.position == NearestGridPosition(pos))
            {
                existingControl = true;
            }
        }
        return existingControl;
    }
    public Vector3 NearestGridPosition(Vector3 Unrounded) {
        Vector3 outPos = Unrounded;
        outPos.x = Mathf.RoundToInt(outPos.x);
        outPos.y = Mathf.RoundToInt(outPos.y);
        outPos.z = Mathf.RoundToInt(outPos.z);
        return outPos;
    }
    public int IntFromDirection(DirectionalControl.Direction d)
    {
        if(d == DirectionalControl.Direction.Left)
        {
            return 0;
        }else if(d == DirectionalControl.Direction.Up)
        {
            return 1;
        }else if(d == DirectionalControl.Direction.Down)
        {
            return 2;
        }else if(d == DirectionalControl.Direction.Right)
        {
            return 3;
        }
        else
        {
            return 0;
        }
    }
}
