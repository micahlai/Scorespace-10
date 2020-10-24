using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalControl : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right};
    public Direction pointDirection;
    public Transform arrow;
    public MeshRenderer[] arrowRenderers;
    public Color[] directionColorsMain;
    public Color[] directionColorsShade;
    bool canActivate = true;
    public float Lifetime;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        canActivate = true;
        timer = Lifetime;
        if (pointDirection == Direction.Up)
        {
            ChangeArrowColors(1);
            arrow.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0));
            
        }
        else if (pointDirection == Direction.Down)
        {
            ChangeArrowColors(0);
            arrow.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180));
        }
        else if (pointDirection == Direction.Right)
        {
            ChangeArrowColors(3);
            arrow.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90));
        }
        else if (pointDirection == Direction.Left)
        {
            ChangeArrowColors(2);
            arrow.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, -90));
        }

        

    }
    void ChangeArrowColors(int i)
    {
        foreach (MeshRenderer m in arrowRenderers)
        {
            m.material.SetColor("Color_F689E8B9", directionColorsMain[i]);
            m.material.SetColor("Color_28180829", directionColorsShade[i]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 p = new Vector3(FindObjectOfType<Car>().transform.position.x, transform.position.y, FindObjectOfType<Car>().transform.position.z);
        if (Vector3.Distance(p, transform.position) < 0.1f && canActivate)
        {
            FindObjectOfType<Car>().TurnCar(pointDirection);
            FindObjectOfType<Car>().transform.position = new Vector3(transform.position.x, FindObjectOfType<Car>().transform.position.y, transform.position.z);
            canActivate = false;
        }
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canActivate = true;
        }
    }
}
