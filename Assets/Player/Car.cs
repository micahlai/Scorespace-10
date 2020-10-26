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
    public Color buffedColor;
    public Color normalColor;
    public string[] bonkSFX;
    public int completeDistance = 100;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("City");
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
            if (FindObjectOfType<ScoreSystem>() != null)
            {
                if (FindObjectOfType<ScoreSystem>().scoreType == ScoreSystem.ScoreType.Distance)
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime * (((FindObjectOfType<ScoreSystem>().score) / 300) + 1));
                    anim.SetFloat("Speed", (FindObjectOfType<ScoreSystem>().score / 300) + 1);
                }
                else
                {
                    transform.Translate(Vector3.right * speed * Time.deltaTime * 1.5f);
                    anim.SetFloat("Speed", 1.5f);
                }
            }
            else
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                anim.SetFloat("Speed", 1);
            }
            anim.SetBool("Text", false);
        }
        else if(!turning)
            {
            anim.SetBool("Text", true);
        }
        else
        {
            anim.SetBool("Text", false);
        }
        if (m_direction == DirectionalControl.Direction.Up || m_direction == DirectionalControl.Direction.Down)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.RoundToInt(transform.position.z));
        }else if (m_direction == DirectionalControl.Direction.Left || m_direction == DirectionalControl.Direction.Right)
        {
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), transform.position.y, transform.position.z);
        }
        if (FindObjectOfType<HealthBar>() != null)
        {
            if(lives > startLives)
            {
                FindObjectOfType<HealthBar>().SetColor(buffedColor);
            }
            else
            {
                FindObjectOfType<HealthBar>().SetColor(normalColor);
            }
            
            FindObjectOfType<HealthBar>().SetSize((float)lives / startLives);
        }
        MusicManager m = FindObjectOfType<MusicManager>();

        if(disableTimer >-disableTime)
        {
            if (!dead)
            {
                m.SetVolume("Melody", Mathf.Lerp(m.GetVolume("Melody"), 0, Time.unscaledDeltaTime / 4));
                m.SetVolume("Bass", Mathf.Lerp(m.GetVolume("Bass"), 0, Time.unscaledDeltaTime / 4));
            }
            else
            {
                m.SetVolume("Melody", Mathf.Lerp(m.GetVolume("Melody"), 0, Time.unscaledDeltaTime));
                m.SetVolume("Bass", Mathf.Lerp(m.GetVolume("Bass"), 0, Time.unscaledDeltaTime));
            }
        }
        if(transform.position.x >= completeDistance && FindObjectOfType<ScoreSystem>().scoreType == ScoreSystem.ScoreType.Time)
        {
            StartCoroutine(Complete());
        }

    }
    public void TurnCar(DirectionalControl.Direction direction)
    {
        
        StartCoroutine(TurnAnim(direction));
        m_direction = direction;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Street"))
        {
            FindObjectOfType<Street>().startCar();
        }
        if (other.gameObject.CompareTag("Restrict"))
        {
            TurnCar(DirectionalControl.Direction.Left);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Building")) && disableTimer > disableTime)
        {
            if (FindObjectOfType<ScoreSystem>().scoreType == ScoreSystem.ScoreType.Distance)
            {
                HurtDistance();
                TakeDamage();
            }
            else
            {
                HurtDistance();
            }

        }
        if (collision.gameObject.CompareTag("Car"))
        {
            if (!dead && FindObjectOfType<ScoreSystem>().scoreType == ScoreSystem.ScoreType.Distance)
            {
                int r = Mathf.RoundToInt(Random.Range(0.5f, bonkSFX.Length - 0.51f));
                FindObjectOfType<AudioManager>().Play("MetalBonk");
                FindObjectOfType<AudioManager>().Play(bonkSFX[r]);

                lives = 0;
                StartCoroutine(Death());
                transform.Translate(Vector3.left * 2);
                disableTimer = 0;

                MusicManager m = FindObjectOfType<MusicManager>();

                m.SetVolume("Melody", -80);
                m.SetVolume("Bass", -80);
            }else if(FindObjectOfType<ScoreSystem>().scoreType == ScoreSystem.ScoreType.Time)
            {
                int r = Mathf.RoundToInt(Random.Range(0.5f, bonkSFX.Length - 0.51f));
                FindObjectOfType<AudioManager>().Play("MetalBonk");
                FindObjectOfType<AudioManager>().Play(bonkSFX[r]);
                FindObjectOfType<ScoreSystem>().incomplete = true;
                lives = 0;
                StartCoroutine(Death());
                transform.Translate(Vector3.left * 2);
                disableTimer = 0;

                MusicManager m = FindObjectOfType<MusicManager>();

                m.SetVolume("Melody", -80);
                m.SetVolume("Bass", -80);
            }
        }
        if (collision.gameObject.CompareTag("Streetlamp") && disableTimer > disableTime)
        {
            if (FindObjectOfType<ScoreSystem>().scoreType == ScoreSystem.ScoreType.Distance)
            {
                if (!dead)
                {

                    HurtDistance();

                    FindObjectOfType<AudioManager>().Play("MetalBonk");
                    TakeDamage();
                    
                }
            }
            else
            {
                HurtDistance();
                FindObjectOfType<AudioManager>().Play("MetalBonk");
            }
           
        }
    }
    public void HurtDistance()
    {
        if (!dead)
        {
            int r = Mathf.RoundToInt(Random.Range(0.5f, bonkSFX.Length - 0.51f));
            FindObjectOfType<AudioManager>().Play(bonkSFX[r]);

            
            MusicManager m = FindObjectOfType<MusicManager>();

            m.SetVolume("Melody", -80);
            m.SetVolume("Bass", -80);
            transform.Translate(Vector3.left * 2);
            disableTimer = 0;
        }
    }
    public void TakeDamage()
    {
        if (lives > 0)
        {
            lives -= 1;
            if (lives <= 0)
            {
                StartCoroutine(Death());

            }
            
        }
    }
    public void AddHealthNormal()
    {
        if(lives < 3)
        {
            lives += 1;
        }
    }
    public void AddHealthBonus()
    {
        if (lives < 4)
        {
            lives += 1;
        }
    }
    IEnumerator Death()
    {
        dead = true;
        if (FindObjectOfType<ScoreSystem>().scoreType == ScoreSystem.ScoreType.Distance)
        {
            FindObjectOfType<Death>().score = FindObjectOfType<ScoreSystem>().stringScore;
        }
        else
        {
            FindObjectOfType<Death>().score = "Incomplete";
        }
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
        yield return new WaitForSeconds(4);
            FindObjectOfType<Death>().Die();
        yield return null;
        
    }
    IEnumerator Complete()
    {
        dead = true;
        FindObjectOfType<Death>().Complete = true;
            FindObjectOfType<Death>().score = FindObjectOfType<ScoreSystem>().stringScore;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        
        yield return new WaitForSeconds(2);
        FindObjectOfType<Death>().Die();
        yield return null;

    }
    IEnumerator TurnAnim(DirectionalControl.Direction d)
    {
        if (d == DirectionalControl.Direction.Up)
        {
            FindObjectOfType<AudioManager>().Play("Up");
        }
        else if (d == DirectionalControl.Direction.Down)
        {
            FindObjectOfType<AudioManager>().Play("Down");

        }
        else if (d == DirectionalControl.Direction.Right)
        {
            FindObjectOfType<AudioManager>().Play("Right");
        }
        else if (d == DirectionalControl.Direction.Left)
        {
            FindObjectOfType<AudioManager>().Play("Left");
        }
        turning = true;
        if(Check180(m_direction, d))
        {
            anim.SetTrigger("180");
            yield return new WaitForSeconds(0.9f);
            
        }else if(CheckCW90(m_direction, d))
        {
            anim.SetTrigger("Right");
            yield return new WaitForSeconds(0.9f);
        }
        else if (CheckCCW90(m_direction, d))
        {
            anim.SetTrigger("Left");
            yield return new WaitForSeconds(0.9f);
        }
        if (d == DirectionalControl.Direction.Up)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0));
        }
        else if (d == DirectionalControl.Direction.Down)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180));
            
        }
        else if (d == DirectionalControl.Direction.Right)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 90));
        }
        else if (d == DirectionalControl.Direction.Left)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, -90));
        }
        turning = false;
        yield return null;
    }
    bool Check180(DirectionalControl.Direction d1, DirectionalControl.Direction d2)
    {
        return (d1 == DirectionalControl.Direction.Up && d2 == DirectionalControl.Direction.Down) || (d1 == DirectionalControl.Direction.Down && d2 == DirectionalControl.Direction.Up) || (d1 == DirectionalControl.Direction.Left && d2 == DirectionalControl.Direction.Right) || (d1 == DirectionalControl.Direction.Right && d2 == DirectionalControl.Direction.Left);
    }
    bool CheckCW90(DirectionalControl.Direction d1, DirectionalControl.Direction d2)
    {
        return (d1 == DirectionalControl.Direction.Up && d2 == DirectionalControl.Direction.Right) || (d1 == DirectionalControl.Direction.Right && d2 == DirectionalControl.Direction.Down) || (d1 == DirectionalControl.Direction.Down && d2 == DirectionalControl.Direction.Left) || (d1 == DirectionalControl.Direction.Left && d2 == DirectionalControl.Direction.Up);
    }
    bool CheckCCW90(DirectionalControl.Direction d1, DirectionalControl.Direction d2)
    {
        return (d1 == DirectionalControl.Direction.Up && d2 == DirectionalControl.Direction.Left) || (d1 == DirectionalControl.Direction.Left && d2 == DirectionalControl.Direction.Down) || (d1 == DirectionalControl.Direction.Down && d2 == DirectionalControl.Direction.Right) || (d1 == DirectionalControl.Direction.Right && d2 == DirectionalControl.Direction.Up);
    }
}

