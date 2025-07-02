using UnityEngine;

public abstract class MeleeWeapon
{
    public string weaponName;

    public float damage;

    public float attackSpeed;

    public float abilityCooldown;

    public float angleRad;

    public GameObject player;

    public LayerMask enemyLayer = 6;

    public void Update()
    {
        
    }


    public MeleeWeapon(string name, float damage, float attackSpeed, float abilityCooldown, GameObject player)
    {
        this.weaponName = name;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.abilityCooldown = abilityCooldown;
        this.player = player;
    }

    public MeleeWeapon(string name, float damage, float attackSpeed, GameObject player)
    {
        this.weaponName = name;
        this.damage = damage;
        this.attackSpeed = attackSpeed;
        this.player = player;
    }

    public abstract void Attack();
}
