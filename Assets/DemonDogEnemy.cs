using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemonDogEnemy : Enemy
{
    private enum DemonDogStates
    {
        sleep = 0,
        chase,
        attack,

    }

    [SerializeField]
    private Trigger alertTrigger;
    [SerializeField]
    private float chaseRange;
    [SerializeField]
    private float attackRange;

    private DemonDogStates state;

    protected void Awake()
    {
        state = DemonDogStates.sleep;
        navAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        enemyBehavior = gameObject.AddComponent<AlertBehavior>();
        ((AlertBehavior)enemyBehavior).Initialize(alertTrigger, enemyStateChange);
    }

    protected override void enemyStateChange()
    {
        //Transitions
        switch (state)
        {
            case DemonDogStates.sleep:
                state = DemonDogStates.chase;
                animController.SetTrigger("surprised");
                enemyBehavior = gameObject.AddComponent<ChaseBehavior>();
                ((ChaseBehavior)enemyBehavior).Initialize(
                    enemyStateChange,
                    breakFromChase,
                    navAgent,
                    attackRange,
                    chaseRange,
                    alertTrigger.GetCollided().transform);
                break;
            case DemonDogStates.chase:
                if (!(animController.GetCurrentAnimatorStateInfo(0).IsName("Attack")))
                    state = DemonDogStates.attack;
                break;
            case DemonDogStates.attack:
                state = DemonDogStates.chase;
                break;
            default:
                break;
        }

        //Actions
        switch (state)
        {
            case DemonDogStates.sleep:
                break;
            case DemonDogStates.chase:
                Blackboard.gameManager.Incombat();
                animController.SetFloat("speed", 1);
                break;
            case DemonDogStates.attack:
                if (!(animController.GetCurrentAnimatorStateInfo(0).IsName("Attack")))
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
        state = DemonDogStates.sleep;
        enemyBehavior = gameObject.AddComponent<AlertBehavior>();
        ((AlertBehavior)enemyBehavior).Initialize(alertTrigger, enemyStateChange);
    }
}
