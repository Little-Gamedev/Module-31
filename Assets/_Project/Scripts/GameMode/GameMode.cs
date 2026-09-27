using System;
using System.Collections.Generic;

public class GameMode : IDisposable
{
    public event Action Win;
    public event Action Defeat;

    private readonly EnemySpawner _enemySpawner;
    private readonly ReactiveList<Enemy> _enemies;
    private readonly GameCondition _winCondition;
    private readonly GameCondition _defeatCondition;

    private bool _isRunning;

    public GameMode(
        EnemySpawner enemySpawner,
        ReactiveList<Enemy> enemies,
        GameCondition winCondition,
        GameCondition defeatCondition)
    {
        _enemySpawner = enemySpawner;
        _enemies = enemies;
        _winCondition = winCondition;
        _defeatCondition = defeatCondition;
    }

    public void Start()
    {
        _winCondition.Completed += OnWinConditionCompleted;
        _defeatCondition.Completed += OnDefeatConditionCompleted;

        _isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        _enemySpawner.Update(deltaTime);
        _winCondition.Update(deltaTime);
        _defeatCondition.Update(deltaTime);
    }

    public void Dispose() => StopConditions();

    private void OnWinConditionCompleted()
    {
        EndGame();
        Win?.Invoke();
    }

    private void OnDefeatConditionCompleted()
    {
        EndGame();
        Defeat?.Invoke();
    }

    private void EndGame()
    {
        _isRunning = false;

        StopConditions();
        DestroyEnemies();
    }

    private void StopConditions()
    {
        _winCondition.Completed -= OnWinConditionCompleted;
        _defeatCondition.Completed -= OnDefeatConditionCompleted;

        _winCondition.Dispose();
        _defeatCondition.Dispose();
    }

    private void DestroyEnemies()
    {
        IReadOnlyList<Enemy> enemies = _enemies.Elements;

        for (int i = enemies.Count - 1; i >= 0; i--)
            enemies[i].Destroy();
    }
}