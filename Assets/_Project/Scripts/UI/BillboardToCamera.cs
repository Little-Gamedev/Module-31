using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    private Transform _cameraTransform;

    private void Start() => _cameraTransform = Camera.main.transform;

    private void LateUpdate() => transform.forward = _cameraTransform.forward;
}