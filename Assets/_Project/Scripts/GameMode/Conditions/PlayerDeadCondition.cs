public class PlayerDeadCondition : GameCondition
{
    private readonly MonoDestroyable _player;

    public PlayerDeadCondition(MonoDestroyable player)
    {
        _player = player;

        _player.Destroyed += OnPlayerDestroyed;
    }

    public override void Dispose()
    {
        _player.Destroyed -= OnPlayerDestroyed;
    }

    private void OnPlayerDestroyed(MonoDestroyable destroyable)
    {
        Complete();
    }
}