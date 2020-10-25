using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    public float speed;
    public DirectionalControl.Direction m_direction;
    public bool turning;
    bool turnfull = false;
    public Quaternion turn;
    public Animator anim;
    public bool dead;
    public Rigidbody[] characterJoints;
    public Collider[] characterColliders;
    public int lives;
    int startLives;
    public float disableTime = 2;
    float disableTimer;
    public NavMeshObstacle carveNavMesh;
    public NavMeshObstacle avoidanceNavMesh;
    public string[] bonkSFX;
    // Start is called before the first frame update
    void Start()
    {
        
        m_direction = DirectionalControl.Direction.Up;
        turning = false;
        turnfull = false;
        foreach (Rigidbody r in characterJoints)
        {
            r.isKinematic = true;
        }
        foreach (Collider c in characterColliders)
        {
            c.enabled = false;
        }
        anim.enabled = true;
        lives = 3;
        startLives = lives;
        disableTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        disableTimer += Time.deltaTime;
        avoidanceNavMesh.enabled = !(disableTimer >= disableTime) || dead;
        carveNavMesh.enabled = !(disableTimer >= disableTime) || dead;
        if (!turning && !dead && disableTimer >= disableTime)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (m_direction == DirectionalControl.Direction.Up || m_direction == DirectionalControl.Direction.Down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z));
        }else if (m_direction == DirectionalControl.Direction.Left || m_direction == DirectionalControl.Direction.Right)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z);
        }
        if (transform.rotation != turn && turning && !dead)
        {
                transform.rotation = Quaternion.Lerp(transform.rotation, turn, Time.deltaTime * 2);
                if (Mathf.Abs(transform.rotation.y - turn.y) < 0.1f || m_direction == DirectionalControl.Direction.Down && (transform.rotation.y < -0.9 || transform.rotation.y > 0.9))
                {
                    transform.rotation = turn;
                    turning = false;
                }
            
        }
        else
        {
            turning = false;
        }
        FindObjectOfType<HealthBar>().SetSize((float)lives / startLives);

        MusicManager m = FindObjectOfType<MusicManager>();

        if(disableTimer >-disableTime)
        {
            if (!dead)
            {
                m.SetVolume("Melody", Mathf.Lerp(m.GetVolume("Melody"), 0, Time.deltaTime / 4));
                m.SetVolume("Bass", Mathf.Lerp(m.GetVolume("Bass"), 0, Time.deltaTime / 4));
            }
            else
            {
                m.SetVolume("Melody", Mathf.Lerp(m.GetVolume("Melody"), 0, Time.deltaTime));
                m.SetVolume("Bass", Mathf.Lerp(m.GetVolume("Bass"), 0, Time.deltaTime));
            }
        }

    }
    public void TurnCar(DirectionalControl.Direction direction)
    {
        turning = true;
        m_direction = direction;
        if(direction == DirectionalControl.Direction.Up)
        {
            turn = Quaternion.Euler(new Vector3(transform.rotation.x, 0));
        }else if (direction == DirectionalControl.Direction.Down)
        {
            turn = Quaternion.Euler(new Vector3(transform.rotation.x, 180));
        }else if (direction == DirectionalControl.Direction.Right)
        {
            turn = Quaternion.Euler(new Vector3(transform.rotation.x, 90));
        }
        else if (direction == DirectionalControl.Direction.Left)
        {
            turn = Quaternion.Euler(new Vector3(transform.rotation.x, -90));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Street"))
        {
            FindObjectOfType<Street>().startCar();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && disableTimer > disableTime)
        {
            int r = Mathf.RoundToInt(Random.Range(0.5f, bonkSFX.Length - 0.51f));
            FindObjectOfType<AudioManager>().Play(bonkSFX[r]);

            if (lives > 0)
            {
                lives -= 1;
                if (lives <= 0)
                {
                    Death();
                }
                transform.Translate(Vector3.left * 2);
                disableTimer = 0;
            }
            MusicManager m = FindObjectOfType<MusicManager>();

            m.SetVolume("Melody", -80);
            m.SetVolume("Bass", -80);
        }
        if (collision.gameObject.CompareTag("Car"))
        {
            int r = Mathf.RoundToInt(Random.Range(0.5f, bonkSFX.Length - 0.51f));
            FindObjectOfType<AudioManager>().Play(bonkSFX[r]);

            lives = 0;
            Death();
                transform.Translate(Vector3.left * 2);
                disableTimer = 0;
            
            MusicManager m = FindObjectOfType<MusicManager>();

            m.SetVolume("Melody", -80);
            m.SetVolume("Bass", -80);
        }
    }
    public void Death()
    {
        dead = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        foreach(Rigidbody r in characterJoints)
        {
            r.isKinematic = false;
        }
        foreach(Collider c in characterColliders)
        {
            c.enabled = true;
        }
        anim.enabled = false;
    }
}

