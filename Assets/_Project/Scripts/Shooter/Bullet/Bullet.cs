using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private IDamageable _owner;
    private Vector3 _direction;
    private float _speed;
    private int _damage;

    private bool _isInitialized = false;

    public void Initialize(IDamageable owner, Vector3 direction, float speed, int damage)
    {
        _owner = owner;
        _direction = direction;
        _speed = speed;
        _damage = damage;

        _isInitialized = true;
    }

    private void Update()
    {
        if (_isInitialized == false)
            return;

        transform.position += _direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        bool hasDamageable = other.TryGetComponent(out IDamageable damageable);

        if (hasDamageable && damageable == _owner)
            return;

        if (hasDamageable)
            damageable.TakeDamage(_damage);

        Destroy(gameObject);
    }

    private void OnValidate()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody.isKinematic == false)
        {
            rigidbody.isKinematic = true;
            Debug.LogWarning("Пуля должна быть кинематичной!Настройка изменена, на isKinematic = true. Не делай так больше.");
        }

        if (rigidbody.useGravity)
        {
            rigidbody.useGravity = false;
            Debug.LogWarning("Вот зачем тебе в уроке гравитация для пули??? Я её убрал. Вот негодяй");
        }

        Collider collider = GetComponent<Collider>();

        if (collider.isTrigger == false)
        {
            collider.isTrigger = true;
            Debug.LogWarning("Коллайдер должен быть триггером...первый класс...садись, два! Поправил сам.");
        }
    }
}