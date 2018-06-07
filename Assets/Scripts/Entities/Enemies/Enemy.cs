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
        if(other.tag == "Player")
        {
            /*Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            hitDirection.y = 0;
            other.GetComponent<PlayerControls>().KnockBack(hitDirection);
            other.GetComponent<Player>().health -= CalculateDamage();*/
        }
        else if(other.tag == "Weapon")
        {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            if(weapon.owner.state == PlayerStates.attacking)
            {
                Debug.Log("Player is attacking!");
                Vector3 hitDirection = (transform.position - other.transform.position).normalized;
                hitDirection.y = 0;
                KnockBack(hitDirection);
                health -= other.gameObject.GetComponent<Weapon>().CalculateDamage();
            }
        }
    }

    private void KnockBack(Vector3 dir)
    {
        GetComponent<Rigidbody>().AddForce(dir * 30, ForceMode.Impulse);
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
        return 50;
    }

}
