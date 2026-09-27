using UnityEngine;
using Object = UnityEngine.Object;

public class Shooter
{
    private readonly Bullet _bulletPrefab;
    private readonly Transform _shootPoint;
    private readonly IDamageable _ownerForBullet;
    private readonly float _bulletSpeed;
    private readonly int _bulletDamage;

    public Shooter(Bullet bulletPrefab, Transform shootPoint, IDamageable ownerForBullet, float bulletSpeed, int bulletDamage)
    {
        _bulletPrefab = bulletPrefab;
        _shootPoint = shootPoint;
        _ownerForBullet = ownerForBullet;
        _bulletSpeed = bulletSpeed;
        _bulletDamage = bulletDamage;
    }

    public void Shoot()
    {
        Bullet newBullet = Object.Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation);

        newBullet.Initialize(_ownerForBullet, _shootPoint.forward, _bulletSpeed, _bulletDamage);
    }
}