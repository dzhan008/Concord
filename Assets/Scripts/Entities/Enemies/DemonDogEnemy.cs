using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonDogEnemy : Enemy
{


    [SerializeField]
    private Trigger alertTrigger;
    [SerializeField]
    private float chaseRange;
    [SerializeField]
    private float attackRange;

    protected void Awake()
    {
        state = EnemyStates.sleep;
        navAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        enemyBehavior = gameObject.AddComponent<AlertBehavior>();
        ((AlertBehavior)enemyBehavior).Initialize(alertTrigger, enemyStateChange);
    }

    protected void enemyStateChange()
    {
        //Transitions
        switch (state)
        {
            case EnemyStates.sleep:
                state = EnemyStates.chase;
                animController.SetTrigger("suprised");
                enemyBehavior = gameObject.AddComponent<ChaseBehavior>();
                ((ChaseBehavior)enemyBehavior).Initialize(
                    enemyStateChange,
                    breakFromChase,
                    navAgent,
                    attackRange,
                    chaseRange,
                    alertTrigger.GetCollided().transform);
                break;
            case EnemyStates.chase:
                if (!(animController.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) &&
                    !(animController.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) &&
                    !(animController.GetCurrentAnimatorStateInfo(0).IsName("Attack3")))
                    state = EnemyStates.attack;
                break;
            case EnemyStates.attack:
                state = EnemyStates.chase;
                break;
            default:
                break;
        }

        //Actions
        switch (state)
        {
            case EnemyStates.sleep:
                break;
            case EnemyStates.chase:
                Blackboard.gameManager.Incombat();
                animController.SetFloat("speed", 1);
                break;
            case EnemyStates.attack:
                if (!(animController.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) &&
                    !(animController.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) &&
                    !(animController.GetCurrentAnimatorStateInfo(0).IsName("Attack3")))
                    animController.SetTrigger("attack");
                enemyStateChange();
                break;
            default:
                break;
        }
    }

    private void breakFromChase()
    {
        Blackboard.gameManager.OutOfCombat();
        animController.SetFloat("speed", 0);
        state = EnemyStates.sleep;
        enemyBehavior = gameObject.AddComponent<AlertBehavior>();
        ((AlertBehavior)enemyBehavior).Initialize(alertTrigger, enemyStateChange);
    }
}
