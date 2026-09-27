public class SurviveTimeCondition : GameCondition
{
    private readonly float _timeToSurvive;

    private float _elapsedTime;

    public SurviveTimeCondition(float timeToSurvive)
    {
        _timeToSurvive = timeToSurvive;
    }

    public override void Update(float deltaTime)
    {
        _elapsedTime += deltaTime;

        if (_elapsedTime >= _timeToSurvive)
            Complete();
    }
}