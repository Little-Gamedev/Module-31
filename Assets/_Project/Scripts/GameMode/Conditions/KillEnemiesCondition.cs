public class KillEnemiesCondition : GameCondition
{
    private readonly ReactiveList<Enemy> _enemies;
    private readonly int _killsToComplete;

    private int _kills;

    public KillEnemiesCondition(ReactiveList<Enemy> enemies, int killsToComplete)
    {
        _enemies = enemies;
        _killsToComplete = killsToComplete;

        _enemies.Removed += OnEnemyRemoved;
    }

    public override void Dispose()
    {
        _enemies.Removed -= OnEnemyRemoved;
    }

    private void OnEnemyRemoved(Enemy enemy)
    {
        _kills++;

        if (_kills >= _killsToComplete)
            Complete();
    }
}