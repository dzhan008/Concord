using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonDogEnemy : Enemy
{
    private enum DemonDogStates
    {
        sleep = 1,
        chase = 2
    }

    [SerializeField]
    private Trigger alertTrigger;

    [SerializeField]
    private float chaseRange;

    private DemonDogStates state;

    protected void Awake()
    {
        state = DemonDogStates.sleep;
        navAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        del = enemyAction;
        enemyBehavior = gameObject.AddComponent<AlertBehavior>();
        ((AlertBehavior)enemyBehavior).Initialize(del, alertTrigger);
    }
    protected override void enemyAction()
    {
        switch (state)
        {
            case DemonDogStates.sleep:
                {
                    Destroy(enemyBehavior);
                    enemyBehavior = gameObject.AddComponent<ChaseBehavior>();
                    ((ChaseBehavior)enemyBehavior).Initialize(del, alertTrigger.GetCollided(), navAgent, chaseRange);
                    state = DemonDogStates.chase;
                    animController.SetFloat("speed", 1.0f);
                    break;
                }
            case DemonDogStates.chase:
                {
                    Destroy(enemyBehavior);
                    enemyBehavior = gameObject.AddComponent<AlertBehavior>();
                    ((AlertBehavior)enemyBehavior).Initialize(del, alertTrigger);
                    state = DemonDogStates.sleep;
                    animController.SetFloat("speed", 0.0f);
                    break;
                }
        }
    }

    private void Update()
    {
        animController.SetFloat("speed", Mathf.Clamp01(navAgent.velocity.magnitude));
    }

    private void attack()
    {
        animController.SetTrigger("attack");
    }
}
