using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : Entity
{
    public delegate void Action();
    protected Action del;
    protected AIBehavior enemyBehavior;
    protected NavMeshAgent navAgent;
    protected Animator animController;

    protected abstract void enemyAction();
}
