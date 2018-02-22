using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehavior : MonoBehaviour
{
    protected Enemy.Action actionDel;
    protected abstract void EnemyBehavior();

    public void Initialize(Enemy.Action del)
    {
        actionDel = del;
    }
}
