using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifeTime = 3f;

    private Vector3 _direction;
    [Networked] public PlayerRef OwnerPlayer { get; set; }

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
        Invoke(nameof(DestroyBullet), lifeTime); // Hủy sau X giây để tránh rác
    }

    void Update()
    {
        if (Object.HasStateAuthority)
        {
            transform.position += _direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Object.HasStateAuthority && other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && playerHealth.Object.InputAuthority != OwnerPlayer)
            {
                playerHealth.RPC_TakeDamage(damage, OwnerPlayer);
                DestroyBullet();
            }

            
        }
    }

    void DestroyBullet()
    {
        Runner.Despawn(Object);
    }
}




