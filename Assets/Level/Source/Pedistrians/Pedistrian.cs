using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pedistrian : MonoBehaviour
{
    public NavMeshAgent agent;
    public enum PedType { Jog, Walk};
    public PedType pedType;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(-35, 0, 0));
        if(pedType == PedType.Jog)
        {
            agent.speed = 7;
            agent.angularSpeed = 80;
        }
        else
        {
            agent.speed = 3.5f;
            agent.angularSpeed = 120;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
