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
                state = EnemyStates.chase;
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
