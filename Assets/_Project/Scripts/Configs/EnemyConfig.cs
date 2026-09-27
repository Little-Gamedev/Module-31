using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy")]
public class EnemyConfig : ScriptableObject
{
    [field: Header("Персонаж")]
    [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
    [field: SerializeField, Min(0)] public float MoveSpeed { get; private set; } = 6;
    [field: SerializeField, Min(0)] public float RotationSpeed { get; private set; } = 900;
    [field: SerializeField, Min(1)] public int MaxHealth { get; private set; } = 100;

    [field: Header("Поведение")]
    [field: SerializeField, Min(0.1f)] public float ChangeDirectionInterval { get; private set; } = 4;

    [field: Header("Урон касанием")]
    [field: SerializeField, Min(0.1f)] public float ContactDamageCooldown { get; private set; } = 2;
    [field: SerializeField, Min(1)] public int ContactDamage { get; private set; } = 10;
}
