using System;

public class GameConditionsFactory
{
    private readonly GameModeConfig _config;
    private readonly MonoDestroyable _player;
    private readonly ReactiveList<Enemy> _enemies;

    public GameConditionsFactory(GameModeConfig config, MonoDestroyable player, ReactiveList<Enemy> enemies)
    {
        _config = config;
        _player = player;
        _enemies = enemies;
    }

    public GameCondition CreateWinCondition()
    {
        switch (_config.WinCondition)
        {
            case WinConditionType.SurviveTime:
                return new SurviveTimeCondition(_config.TimeToSurvive);

            case WinConditionType.KillEnemies:
                return new KillEnemiesCondition(_enemies, _config.KillsToWin);

            default:
                throw new ArgumentOutOfRangeException(nameof(_config.WinCondition), _config.WinCondition, "Такого условия победы не существует");
        }
    }

    public GameCondition CreateDefeatCondition()
    {
        switch (_config.DefeatCondition)
        {
            case DefeatConditionType.PlayerDead:
                return new PlayerDeadCondition(_player);

            case DefeatConditionType.EnemiesOverflow:
                return new EnemiesOverflowCondition(_enemies, _config.MaxEnemiesAlive);

            default:
                throw new ArgumentOutOfRangeException(nameof(_config.DefeatCondition), _config.DefeatCondition, "Такого условия поражения не существует");
        }
    }
}