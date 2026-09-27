public class EnemiesOverflowCondition : GameCondition
{
    private readonly ReactiveList<Enemy> _enemies;
    private readonly int _maxEnemies;

    public EnemiesOverflowCondition(ReactiveList<Enemy> enemies, int maxEnemies)
    {
        _enemies = enemies;
        _maxEnemies = maxEnemies;

        _enemies.Added += OnEnemyAdded;
    }

    public override void Dispose()
    {
        _enemies.Added -= OnEnemyAdded;
    }

    private void OnEnemyAdded(Enemy enemy)
    {
        if (_enemies.Elements.Count > _maxEnemies)
            Complete();
    }
}