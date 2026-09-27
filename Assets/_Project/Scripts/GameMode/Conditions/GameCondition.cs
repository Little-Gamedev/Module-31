using System;

public abstract class GameCondition : IDisposable
{
    public event Action Completed;

    public bool IsCompleted { get; private set; }

    public virtual void Update(float deltaTime)
    {
    }

    public virtual void Dispose()
    {
    }

    protected void Complete()
    {
        if (IsCompleted)
            return;

        IsCompleted = true;

        Completed?.Invoke();
    }
}