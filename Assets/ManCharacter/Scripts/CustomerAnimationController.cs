using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerAnimationController : ManAnimationController
{
    private NavMeshAgent mAgent;
    protected override void Start()
    {
        mAgent = GetComponent<NavMeshAgent>();
        base.Start();
    }

    protected override void Update()
    {
        if(mAgent.velocity.magnitude <= 0)
        {
            Idle();
        }
        else
        {
            WalkRun();
        }
    }

    protected override float GetPlayerVelocityMagnitude()
    {
        return mAgent.velocity.magnitude;
    }
}
