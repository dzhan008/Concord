using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The role that the player chooses to be (warrior, mage, )
    public enum Role
    {
        sword = 0,
        maceAndShield = 1,
        spear = 2,
        dagger = 3,
        bow = 4
    }

    public enum PlayerStates
    {
        idle = 0,
        attacking = 1,
        dead = 2
    }

public class Player : Entity {

    public Role currentRole;
    public int playerLevel = 0;
    public Transform WeaponGrip;
    public Inventory MyInventory;
    public PlayerInfo MyPlayerInfo;
    [SerializeField]
    public Stats playerStats;
    public PlayerStates state = PlayerStates.idle;

    public string playerName;

    //Player's Health
    public int maxHealth;
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
                //Destroy(gameObject);
                Debug.Log("Dead");
            }
        }
    }

    private int _health = 0;

    //Player's Stats
    private int _strength = 0;
    public int strength
    {
        get
        {
            return _strength;
        }
    }

    private int _agility = 0;
    public int agility
    {
        get
        {
            return _agility;
        }
    }

    private int _intelligence = 0;
    public int intelligence
    {
        get
        {
            return _intelligence;
        }
    }

    // TODO: Remove this test when game starts
    public void Awake()
    {
        Initialize(EntID);
    }

    private void Start()
    {
        maxHealth = _health = playerStats.Health;

        _strength = playerStats.Strength;
        _agility = playerStats.Agility;
        _intelligence = playerStats.Intelligence;
    }

    public void CheckHeldInventory()
    {
        if (WeaponGrip)
        {
            Item held_item = WeaponGrip.GetComponentInChildren<Item>();
            if (held_item)
            {
                AddItem(held_item);
            }
        }
    }

    public void Initialize(int ID)
    {
        EntID = ID;
        Blackboard.setPlayerRef(this, EntID);
        MyInventory = ScriptableObject.CreateInstance<Inventory>();
    }

    // Encapsulated Inventory functions

    public bool AddItem(Item new_item) {
        return MyInventory.AddItem(new_item, MyPlayerInfo);
    }

    public void ScrollLeft() {
        MyInventory.ScrollLeft();
    }

    public void ScrollRight() {
        MyInventory.ScrollRight();
    }

    public void UseItem() {
        Debug.Log("Used: "+ MyInventory.CurrentItem);
        // If it's a weapon, equip it
        Weapon use_weapon = MyInventory.CurrentItem.MyPrefab.GetComponent<Weapon>();
        if (use_weapon) {
            Weapon old_weapon = WeaponGrip.GetComponentInChildren<Weapon>();
            if (old_weapon) {
                print("destroying: "+old_weapon.name);
                old_weapon.gameObject.SetActive(false);
                Destroy(old_weapon.gameObject);
            }
            Weapon new_weapon = Instantiate(use_weapon, WeaponGrip);
            new_weapon.transform.localPosition = Vector3.zero;
            new_weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}