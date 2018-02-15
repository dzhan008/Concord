using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        del = enemyAction;
        enemyBehavior = new AlertBehavior(del, alertTrigger);
    }
    protected override void enemyAction()
    {
        switch (state)
        {
            case DemonDogStates.sleep:
                {
                    enemyBehavior = new ChaseBehavior(del, alertTrigger.GetCollided(), navAgent, chaseRange);
                    state = DemonDogStates.chase;
                    animController.SetFloat("speed", 1.0f);
                    break;
                }
            case DemonDogStates.chase:
                {
                    enemyBehavior = new AlertBehavior(del, alertTrigger);
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
