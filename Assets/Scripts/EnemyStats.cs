using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : Stats {

    // TODO: Remove this test
    [SerializeField]
    private Text HPDisplay;
    void Awake() {
        HPDisplay.text = _HP.ToString();
    }

	public const int MaxHP = 1000;

	// HP of the enemy
    public int HP {
        get {
            return _HP;
        }
        set {
            _HP = Mathf.Clamp(value, 0, MaxHP);
            // TODO: Remove thes tests
            HPDisplay.text = _HP.ToString();
            if (_HP == 0) {
                Destroy(gameObject);
            }
        }
    }
    [SerializeField]
    private int _HP = 500;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Weapon>()) {
            Stats player_stats = other.GetComponent<Stats>();
            this.HP -= CalculateDamage(player_stats);
        }
    }

    private int CalculateDamage(Stats player_stats) {
        int damage = player_stats.Strength - this.Defense;
        if (damage <= 0) {
            return 1; // Always do at least 1 damage
        }
        // Elemental bonus
        damage = (int) (damage * Blackboard.GetElementMultiplier(player_stats.Element, this.Element));
        // Variation (based on weapon)
        // TODO: Give player a reference to weapon and stats
        Weapon player_weapon = player_stats.GetComponent<Weapon>();
        damage += (int) (damage * Random.Range(-1f * player_weapon.damageVariation, player_weapon.damageVariation));
        return damage;
    }
}
