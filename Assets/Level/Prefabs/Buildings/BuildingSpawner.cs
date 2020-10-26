using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject[] buildings;
    public float zPos;
    public float yPos;
    public bool buildingShow;
    // Start is called before the first frame update
    void Start()
    {
        buildingShow = true; 
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(FindObjectOfType<Car>().transform.position.x + 20, 12, 0);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward));
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {

            if (!buildingShow)
            {
                buildingShow = true;
            }
        }
        else
        {
            if (buildingShow)
            {
                buildingShow = false;
                int r = Mathf.RoundToInt(Random.Range(0.5f, buildings.Length - 0.51f));
                Instantiate(buildings[r], new Vector3(FindObjectOfType<Car>().transform.position.x + 70, yPos, zPos), Quaternion.Euler(new Vector3(0, 180, 0)));
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {

            int r = Mathf.RoundToInt(Random.Range(0.5f, buildings.Length - 0.51f));
            Instantiate(buildings[r], new Vector3(FindObjectOfType<Car>().transform.position.x + 70, yPos, zPos), Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
}
