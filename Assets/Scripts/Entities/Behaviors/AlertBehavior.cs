using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBehavior : AIBehavior
{
    private Trigger.OnTrigger triggerDel;

    public AlertBehavior(Enemy.Action del, Trigger trigger) : base(del)
    {
        triggerDel = EnemyBehavior;
        trigger.Initialize(triggerDel);
    }

    protected override void EnemyBehavior()
    {
        actionDel();
    }
}
