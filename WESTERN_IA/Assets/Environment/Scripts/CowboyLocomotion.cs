using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowboyLocomotion : MonoBehaviour
{
    [HideInInspector]
    public GameObject Target;

    private NavMeshAgent _navAgent;

    void Awake()
    {
        _navAgent = GetComponentInParent<NavMeshAgent>();

    }

    private void Start()
    {
    }


    void Update()
    {
        if (Target != null)
        {
            MoveTo(Target.transform);
        }
    }

    public void setSpeed(int _speed) {
        _navAgent.speed = _speed;
    }

    public void MoveTo(Transform target)
    {
        _navAgent.speed *= 2;
        _navAgent.destination = target.position;
        _navAgent.gameObject.transform.LookAt(_navAgent.destination);

    }
}
