using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehavior : MonoBehaviour {

    Enemy.Action del;
    protected abstract void EnemyBehavior();

    public AIBehavior(Enemy.Action del)
    {
        this.del = del;
    }
}
