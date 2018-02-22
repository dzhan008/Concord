using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : AIBehavior {

    GameObject chaseTarget;
    NavMeshAgent navAgent;
    float chaseRange;
    Vector3 startingPos;

    public void Initialize(Enemy.Action del, GameObject target, NavMeshAgent nav, float range)
    {
        base.Initialize(del);
        chaseTarget = target;
        navAgent    = nav;
        chaseRange  = range;
        startingPos = navAgent.gameObject.transform.position;
        StartCoroutine(chase());
    }

    protected override void EnemyBehavior()
    {
        
    }

    private IEnumerator chase()
    {
        yield return null;
        while (Vector3.Distance(navAgent.gameObject.transform.position, startingPos) < chaseRange)
        {
            navAgent.destination = chaseTarget.transform.position;
            yield return null;
        }
        actionDel();
    }
}
