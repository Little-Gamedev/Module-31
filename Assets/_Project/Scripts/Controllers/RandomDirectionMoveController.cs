using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomDirectionMoveController : Controller
{
    private readonly IDirectionalMovable _movable;
    private readonly IDirectionalRotatable _rotatable;
    private readonly float _changeDirectionInterval;

    private float _timeLeft;

    public RandomDirectionMoveController(
        IDirectionalMovable movable,
        IDirectionalRotatable rotatable,
        float changeDirectionInterval)
    {
        if (changeDirectionInterval <= 0)
            throw new ArgumentOutOfRangeException(nameof(changeDirectionInterval), "Время смены должно быт ь меньше нуля");

        _movable = movable;
        _rotatable = rotatable;
        _changeDirectionInterval = changeDirectionInterval;
    }

    public override void Disable()
    {
        base.Disable();

        _movable.SetMoveDirection(Vector3.zero);
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _timeLeft -= deltaTime;

        if (_timeLeft > 0)
            return;

        ChangeDirection();

        _timeLeft = _changeDirectionInterval;
    }

    private void ChangeDirection()
    {
        Vector3 direction = Quaternion.Euler(0, Random.Range(0f, 360f), 0) * Vector3.forward;

        _movable.SetMoveDirection(direction);
        _rotatable.SetRotationDirection(direction);
    }
}