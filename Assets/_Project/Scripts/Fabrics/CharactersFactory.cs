using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class CharactersFactory
{
    public T CreateCharacter<T>(
    T prefab,
    Vector3 position,
    float moveSpeed,
    float rotationSpeed,
    int maxHealth)
    where T : Character
    {
        T instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

        DirectionalMover mover;
        DirectionalRotator rotator;

        if (instance.TryGetComponent(out CharacterController characterController))
        {
            mover = new CharacterControllerDirectionalMover(characterController, moveSpeed);
            rotator = new TransformDirectionalRotator(instance.transform, rotationSpeed);
        }
        else if (instance.TryGetComponent(out Rigidbody rigidbody))
        {
            mover = new RigidbodyDirectionalMover(rigidbody, moveSpeed);
            rotator = new RigidbodyDirectionalRotator(rigidbody, rotationSpeed);
        }
        else
        {
            throw new InvalidOperationException($"На префабе {prefab.name} нет CharacterController или Rigidbody. Нет нужного. " +
                $"Как говорится, смотря какие details на какой fabric...а на этой fabric нет подходящих details");
        }

        Health health = new Health(maxHealth);

        instance.Initialize(mover, rotator, health);

        return instance;
    }
}