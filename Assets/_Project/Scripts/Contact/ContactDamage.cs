using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ContactDamage : MonoBehaviour
{
    private IDamageable _target;
    private int _damage;
    private float _cooldown;

    private float _timeLeft;
    private bool _isTargetInZone;
    private bool _isInitialized;

    public void Initialize(IDamageable target, int damage, float cooldown)
    {
        _target = target;
        _damage = damage;
        _cooldown = cooldown;

        _isInitialized = true;
    }

    private void Update()
    {
        if (_isInitialized == false)
            return;

        _timeLeft -= Time.deltaTime;

        if (_isTargetInZone == false || _timeLeft > 0)
            return;

        _target.TakeDamage(_damage);

        _timeLeft = _cooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTarget(other))
            _isTargetInZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsTarget(other))
            _isTargetInZone = false;
    }

    private bool IsTarget(Collider other) => other.TryGetComponent(out IDamageable damageable) && damageable == _target;

    private void OnValidate()
    {
        Collider collider = GetComponent<Collider>();

        if (collider.isTrigger == false)
        {
            collider.isTrigger = true;
            Debug.LogWarning("[ContactDamage] Коллайдер зоны наносимого урона должен быть триггером. Исправил...снова! isTrigger = true");
        }
    }
}