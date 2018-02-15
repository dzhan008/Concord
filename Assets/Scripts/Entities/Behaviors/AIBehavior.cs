using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehavior : MonoBehaviour
{
    protected Enemy.Action actionDel;
    protected abstract void EnemyBehavior();

    public AIBehavior(Enemy.Action del)
    {
        actionDel = del;
    }
}
