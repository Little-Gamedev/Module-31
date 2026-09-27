using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private ContactDamage _contactDamage;

    public ContactDamage ContactDamage => _contactDamage;
}