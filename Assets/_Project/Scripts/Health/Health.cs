using System;

public class Health
{
    private readonly ReactiveVariable<int> _current;
    private readonly int _max;

    public Health(int max)
    {
        if (max <= 0)
            throw new ArgumentOutOfRangeException(nameof(max), "Максимальное здоровье должно быть больше нуля");

        _max = max;
        _current = new ReactiveVariable<int>(max);
    }

    public IReadOnlyVariable<int> Current => _current;

    public int Max => _max;

    public bool IsDead => _current.Value <= 0;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException(nameof(damage), "Урон не может быть отрицательным");

        if (IsDead)
            return;

        int newValue = _current.Value - damage;

        if (newValue < 0)
            newValue = 0;

        _current.Value = newValue;
    }
}