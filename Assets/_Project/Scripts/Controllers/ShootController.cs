using UnityEngine;

public class ShootController : Controller
{
    private readonly Shooter _shooter;
    private readonly KeyCode _shootKeyCode;

    public ShootController(Shooter shooter, KeyCode shootKeyCode)
    {
        _shooter = shooter;
        _shootKeyCode = shootKeyCode;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (Input.GetKeyDown(_shootKeyCode))
            Shoot();
    }

    private void Shoot() => _shooter.Shoot();
}
