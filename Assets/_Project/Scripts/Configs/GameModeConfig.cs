using UnityEngine;

[CreateAssetMenu(fileName = "GameModeConfig", menuName = "Configs/GameMode")]
public class GameModeConfig : ScriptableObject
{
    [field: Header("Правила")]
    [field: SerializeField] public WinConditionType WinCondition { get; private set; }
    [field: SerializeField] public DefeatConditionType DefeatCondition { get; private set; }

    [field: Header("Победа")]
    [field: SerializeField, Min(1)] public float TimeToSurvive { get; private set; } = 60;
    [field: SerializeField, Min(1)] public int KillsToWin { get; private set; } = 10;

    [field: Header("Поражение")]
    [field: SerializeField, Min(1)] public int MaxEnemiesAlive { get; private set; } = 15;

    [field: Header("Спавн врагов")]
    [field: SerializeField, Min(0.1f)] public float EnemySpawnCooldown { get; private set; } = 2;
}