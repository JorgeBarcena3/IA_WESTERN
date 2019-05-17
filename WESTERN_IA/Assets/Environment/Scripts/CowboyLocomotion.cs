using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowboyLocomotion : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent _navAgent;

    void Awake()
    {
        _navAgent = GetComponentInParent<NavMeshAgent>();

    }

    void Update()
    {
        if (Target != null)
        {
            if (!_navAgent.hasPath)
            {
                MoveTo(Target.transform);
            }
        }
    }

    public void MoveTo(Transform target)
    {
        _navAgent.destination = target.position;
        _navAgent.gameObject.transform.LookAt(_navAgent.destination);
        
    }
}
