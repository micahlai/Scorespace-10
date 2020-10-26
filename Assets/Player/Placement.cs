using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public GameObject directionPrefab;
    public DirectionalControl.Direction currentDirection;
    public Highlight highlight;
    public float[] values;
    public GameObject Dust;
    public GameObject burst;
    [HideInInspector]
    public static Placement main;
    // Start is called before the first frame update
    void Start()
    {
        highlight = FindObjectOfType<Highlight>();
        main = this;
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = 10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<DirectionSelection>().status = values;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        highlight.pointDirection = currentDirection;
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                highlight.position = NearestGridPosition(hit.point);
                
            }
        }
        bool b = true;
        if(FindObjectOfType<PauseUI>() != null)
        {
            b = !FindObjectOfType<PauseUI>().isPaused;
        }
        if (Input.GetMouseButtonDown(0) && b)
        {
            
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<FloatingColectable>() != null)
                {
                    hit.collider.GetComponent<FloatingColectable>().OnCollected.Invoke();
                }
                else
                {
                    if (values[IntFromDirection(currentDirection)] > 0)
                    {
                        DirectionalControl d = PlaceDirectionalControl(directionPrefab, highlight.position);
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
        if (Input.GetMouseButtonDown(1) && b)
        {

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<DirectionalControl>() != null)
                {
                    ParticleSystem p = Instantiate(Dust, hit.collider.GetComponent<DirectionalControl>().transform.position, Quaternion.Euler(new Vector3(-90, 0, 0))).GetComponent<ParticleSystem>();
                    p.Play();
                    Destroy(p.gameObject, 5);
                    FindObjectOfType<AudioManager>().Play("Remove");
                    Destroy(hit.collider.GetComponent<DirectionalControl>().gameObject);
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeSelctionDirection(DirectionalControl.Direction.Up);
        }else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeSelctionDirection(DirectionalControl.Direction.Down);
        }else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelctionDirection(DirectionalControl.Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
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
            FindObjectOfType<AudioManager>().Play("PlaceArrow");
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
