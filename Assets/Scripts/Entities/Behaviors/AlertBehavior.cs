using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertBehavior : AIBehavior
{
    private Trigger.OnTrigger triggerDel;

    public void Initialize(Enemy.Action del, Trigger trigger)
    {
        base.Initialize(del);
        triggerDel = EnemyBehavior;
        trigger.Initialize(triggerDel);
    }

    protected override void EnemyBehavior()
    {
        actionDel();
    }
}
