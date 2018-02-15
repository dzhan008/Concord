using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : AIBehavior {

    GameObject chaseTarget;
    NavMeshAgent navAgent;
    float chaseRange;
    Vector3 startingPos;

    public ChaseBehavior(Enemy.Action del, GameObject target, NavMeshAgent nav, float range) : base(del)
    {
        chaseTarget = target;
        navAgent    = nav;
        chaseRange  = range;
        startingPos = transform.position;
    }

    protected override void EnemyBehavior()
    {
        
    }

    private void Update()
    {
        navAgent.destination = chaseTarget.transform.position;
        if(Vector3.Distance(transform.position, startingPos) < chaseRange)
        {
            actionDel();
        }
    }
}
