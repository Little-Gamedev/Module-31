using UnityEngine;

public class Hero : Character
{
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private Transform _shootPoint;

    public Transform CameraTarget => _cameraTarget;

    public Transform ShootPoint => _shootPoint;
}