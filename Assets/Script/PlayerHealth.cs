using Fusion;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public int maxHealth = 100;

    [Networked] public int currentHealth { get; private set; }

    public delegate void HealthChangedDelegate(int current, int max);
    public event HealthChangedDelegate OnHealthChangedEvent;

    private int lastHealth; // Dùng để theo dõi thay đổi HP

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            currentHealth = maxHealth;
        }
        lastHealth = currentHealth;
    }

    public override void FixedUpdateNetwork()
    {
        // Kiểm tra nếu máu thay đổi, gọi sự kiện cập nhật UI
        if (currentHealth != lastHealth)
        {
            OnHealthChangedEvent?.Invoke(currentHealth, maxHealth);
            lastHealth = currentHealth;
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int damage)
    {
        if (!Object.HasStateAuthority) return; // Chỉ thực hiện trên State Authority

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnHealthChangedEvent?.Invoke(currentHealth, maxHealth); // Cập nhật UI trên máy chủ
        RPC_UpdateHealth(currentHealth); // Gửi cập nhật đến tất cả client
        Debug.Log($"Player bị bắn! Máu còn lại: {currentHealth}");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_UpdateHealth(int newHealth)
    {
        currentHealth = newHealth;
        OnHealthChangedEvent?.Invoke(currentHealth, maxHealth); // Cập nhật UI trên tất cả client
    }
}








