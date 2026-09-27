using UnityEngine;

public class PlayerFactory
{
    private readonly ControllersUpdateService _controllersUpdateService;
    private readonly CharactersFactory _charactersFactory;

    public PlayerFactory(ControllersUpdateService controllersUpdateService, CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _charactersFactory = charactersFactory;
    }

    public Hero Create(PlayerConfig config, Vector3 position, Transform cameraTransform)
    {
        Hero player = _charactersFactory.CreateCharacter(
            config.PlayerPrefab,
            position,
            config.MoveSpeed,
            config.RotationSpeed,
            config.MaxHealth);

        Shooter shooter = new Shooter(
            config.BulletPrefab,
            player.ShootPoint,
            player,
            config.BulletSpeed,
            config.BulletDamage);

        CameraRelativeDirection cameraRelativeDirection = new CameraRelativeDirection(cameraTransform);

        Controller controller = new CompositeController(
            new KeyboardDirectionalMoveController(player, cameraRelativeDirection),
            new KeyboardDirectionalRotationController(player, cameraRelativeDirection),
            new ShootController(shooter, config.ShootKeyCode));

        controller.Enable();

        _controllersUpdateService.Add(controller, () => player.IsDestroyed);

        return player;
    }
}