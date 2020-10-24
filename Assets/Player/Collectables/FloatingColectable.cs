using UnityEngine;
using UnityEngine.Events;
using System.Collections;

// Makes objects float up & down while gently spinning.
public class FloatingColectable : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    public GameObject explode;
    public static GameObject explodeParticle;
    public UnityEvent OnCollected;
    public Color deathColor;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Use this for initialization
    private void Awake()
    {
        if (explode != null)
        {
            explodeParticle = explode;
        }
    }
    void Start()
    {
        
      
        
            explode = explodeParticle;
        
        OnCollected.AddListener(Explode);
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
    void Explode()
    {
        ParticleSystem p = Instantiate(explode, transform.position, transform.rotation).GetComponent<ParticleSystem>();
        p.Play();
        p.startColor = deathColor;
        Destroy(p.gameObject, 5);
        Destroy(gameObject);
    }
}
