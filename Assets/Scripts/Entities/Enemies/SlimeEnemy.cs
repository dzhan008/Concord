using UnityEngine;
using UnityEngine.AI;

public class SlimeEnemy : Enemy
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
                break;
            case EnemyStates.chase:
                break;
            case EnemyStates.attack:
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
                break;
            case EnemyStates.attack:
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
