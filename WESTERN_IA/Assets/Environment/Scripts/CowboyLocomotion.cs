﻿using System.Collections;
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
            if (!_navAgent.hasPath)
                MoveTo(Target.transform);
        }
    }

    public void setSpeed(float _speed)
    {
        _navAgent.speed = _speed;
    }

    public void MoveTo(Transform target)
    {
        _navAgent.destination = target.position;
        _navAgent.gameObject.transform.LookAt(_navAgent.destination);

    }
}
