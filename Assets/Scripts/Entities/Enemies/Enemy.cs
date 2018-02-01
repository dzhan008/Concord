using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Entity {

    public delegate void Action();
    protected Action del;
    protected AIBehavior enemyBehavior;

    protected abstract void enemyAction();
    protected abstract AIBehavior getBehavior();

    protected void Awake()
    {
        del = enemyAction;
        enemyBehavior = getBehavior();
    }
}
