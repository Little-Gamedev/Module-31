using UnityEngine;

public abstract class Character : MonoDestroyable, IDirectionalMovable, IDirectionalRotatable, IDamageable
{
    private DirectionalMover _directionalMover;
    private DirectionalRotator _directionalRotator;
    private Health _health;

    public Vector3 CurrentVelocity => _directionalMover.CurrentVelocity;

    public Vector3 Position => transform.position;

    public Quaternion CurrentRotation => _directionalRotator.CurrentRotation;

    public IReadOnlyVariable<int> CurrentHealth => _health.Current;

    public int MaxHealth => _health.Max;

    public bool IsDead => _health.IsDead;

    public void Initialize(DirectionalMover mover, DirectionalRotator rotator, Health health)
    {
        _directionalMover = mover;
        _directionalRotator = rotator;
        _health = health;

        foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
            initializable.Initialize();
    }

    private void Update()
    {
        _directionalMover.Update(Time.deltaTime);
        _directionalRotator.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 direction) => _directionalMover.SetInputDirection(direction);

    public void SetRotationDirection(Vector3 direction) => _directionalRotator.SetInputDirection(direction);

    public void TakeDamage(int damage)
    {
        if (IsDestroyed)
            return;

        _health.TakeDamage(damage);

        if (_health.IsDead)
            Destroy();
    }
}