using UnityEngine;

public class EnemyFactory
{
    private readonly ControllersUpdateService _controllersUpdateService;
    private readonly CharactersFactory _charactersFactory;

    public EnemyFactory(ControllersUpdateService controllersUpdateService, CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _charactersFactory = charactersFactory;
    }

    public Enemy Create(EnemyConfig config, Vector3 position, IDamageable target)
    {
        Enemy enemy = _charactersFactory.CreateCharacter(
            config.EnemyPrefab,
            position,
            config.MoveSpeed,
            config.RotationSpeed,
            config.MaxHealth);

        enemy.ContactDamage.Initialize(target, config.ContactDamage, config.ContactDamageCooldown);

        Controller controller = new RandomDirectionMoveController(enemy, enemy, config.ChangeDirectionInterval);

        controller.Enable();

        _controllersUpdateService.Add(controller, () => enemy.IsDestroyed);

        return enemy;
    }
}