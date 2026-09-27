using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner
{
    private readonly EnemyFactory _enemyFactory;
    private readonly EnemyConfig _enemyConfig;
    private readonly IReadOnlyList<Transform> _spawnPoints;
    private readonly IDamageable _target;
    private readonly ReactiveList<Enemy> _enemies;
    private readonly float _spawnCooldown;

    private float _timeLeft;

    public EnemySpawner(
        EnemyFactory enemyFactory,
        EnemyConfig enemyConfig,
        IReadOnlyList<Transform> spawnPoints,
        IDamageable target,
        ReactiveList<Enemy> enemies,
        float spawnCooldown)
    {
        if (spawnPoints == null || spawnPoints.Count == 0)
            throw new ArgumentException("Нет точек спавна, чтобы заспавнить врагов", nameof(spawnPoints));

        if (spawnCooldown <= 0)
            throw new ArgumentOutOfRangeException(nameof(spawnCooldown), "Кулдаун спавна должен быть больше нуля,окаянный!");

        _enemyFactory = enemyFactory;
        _enemyConfig = enemyConfig;
        _spawnPoints = spawnPoints;
        _target = target;
        _enemies = enemies;
        _spawnCooldown = spawnCooldown;
    }

    public void Update(float deltaTime)
    {
        _timeLeft -= deltaTime;

        if (_timeLeft > 0)
            return;

        Spawn();

        _timeLeft = _spawnCooldown;
    }

    private void Spawn()
    {
        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        Enemy enemy = _enemyFactory.Create(_enemyConfig, spawnPoint.position, _target);

        enemy.Destroyed += OnEnemyDestroyed;
        _enemies.Add(enemy);
    }

    private void OnEnemyDestroyed(MonoDestroyable destroyable)
    {
        destroyable.Destroyed -= OnEnemyDestroyed;

        if (destroyable is Enemy enemy)
            _enemies.Remove(enemy);
    }
}