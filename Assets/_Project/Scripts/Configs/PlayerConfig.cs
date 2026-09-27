using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]
public class PlayerConfig : ScriptableObject
{
    [field: Header("Персонаж")]
    [field: SerializeField] public Hero PlayerPrefab { get; private set; }
    [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 9;
    [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField, Min(1)] public int MaxHealth { get; private set; } = 100;

    [field: Header("Стрельба")]
    [field: SerializeField] public Bullet BulletPrefab { get; private set; }
    [field: SerializeField] public KeyCode ShootKeyCode { get; private set; } = KeyCode.Space;
    [field: SerializeField, Min(1)] public float BulletSpeed { get; private set; } = 20;
    [field: SerializeField, Min(1)] public int BulletDamage { get; private set; } = 5;
}