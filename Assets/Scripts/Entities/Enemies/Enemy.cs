using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStats))]
public abstract class Enemy : Entity
{
    public delegate void Action();
    protected Action del;
    protected AIBehavior enemyBehavior;
    protected NavMeshAgent navAgent;
    protected Animator animController;

    protected abstract void enemyStateChange();

    /*Stats*/
    [SerializeField]
    private EnemyStats stats;
    private int maxHealth;
    // HP of the enemy
    public int health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = Mathf.Clamp(value, 0, maxHealth);
            if (_health == 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private int _health = 0;

    private void Start()
    {
        maxHealth = _health = stats.Health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            hitDirection.y = 0;
            other.GetComponent<PlayerControls>().KnockBack(hitDirection);
            other.GetComponent<Player>().TakeDamage(CalculateDamage());
            //other.GetComponent<Player>().attacked = true;
        }
        /*else if(other.tag == "Weapon")
        {
            Vector3 hitDirection = (transform.position - other.transform.position).normalized;
            hitDirection.y = 0;
            KnockBack(hitDirection);
            health -= 100;
        }*/
    }

    public void Damage(int dmg)
    {
        health -= dmg - stats.Defense;
        KnockBack(Vector3.right);
    }

    private void KnockBack(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir, ForceMode.Impulse);
    }
    private int CalculateDamage()
    {
        /*int damage = player_stats.Strength - this.Defense;
        if (damage <= 0) {
            return 1; // Always do at least 1 damage
        }
        // Elemental bonus
        damage = (int) (damage * Blackboard.GetElementMultiplier(player_stats.Element, this.Element));
        // Variation (based on weapon)
        // TODO: Give player a reference to weapon and stats
        Weapon player_weapon = player_stats.GetComponent<Weapon>();
        damage += (int) (damage * Random.Range(-1f * player_weapon.damageVariation, player_weapon.damageVariation));
        return damage;*/
        return stats.Strength * 5;
    }

}
