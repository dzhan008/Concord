using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class ChaseBehavior : AIBehavior {

    NavMeshAgent navAgent;
    Action action, reset;
    Transform chaseTarget;
    Vector3 startingPos;
    float chaseRange;
    float attackRange;

    public void Initialize(Action action, Action reset, NavMeshAgent navAgent, float attackRange, float chaseRange, Transform chaseTarget)
    {
        this.action      = action;
        this.reset       = reset;
        this.navAgent    = navAgent;
        this.startingPos = navAgent.gameObject.transform.position;
        this.chaseRange  = chaseRange;
        this.attackRange = attackRange;
        this.chaseTarget = chaseTarget;
        StartCoroutine(chase());
    }

    private IEnumerator chase()
    {
        yield return null;

        while(Vector3.Distance(startingPos, gameObject.transform.position) < chaseRange)
        {
            if(Vector3.Distance(gameObject.transform.position, chaseTarget.position) < attackRange)
            {
                action();
            }
            if (chaseTarget.hasChanged)
            {
                navAgent.SetDestination(chaseTarget.position);
            }

            yield return null;
        }
        StartCoroutine(returnToPosition());
    }

    private IEnumerator returnToPosition()
    {
        yield return null;

        navAgent.SetDestination(startingPos);
        while(Vector3.Distance(gameObject.transform.position, startingPos) > .05f)
        {
            yield return null;
        }

        reset();
    }
    
}
