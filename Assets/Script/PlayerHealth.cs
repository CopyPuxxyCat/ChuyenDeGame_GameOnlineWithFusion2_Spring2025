using Fusion;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public int maxHealth = 100;
    [Networked] public int KillCount { get; private set; }
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
            if(currentHealth < 11)
            {
                GameManagephoton.instance.LocalPlayer.transform.localPosition = new Vector3(0, 1, 0);
                currentHealth = 100;
            }
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int damage, PlayerRef attacker)
    {
        if (!Object.HasStateAuthority) return; // Chỉ thực hiện trên State Authority
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        OnHealthChangedEvent?.Invoke(currentHealth, maxHealth); // Cập nhật UI trên máy chủ
        RPC_UpdateHealth(currentHealth); // Gửi cập nhật đến tất cả client
        if(currentHealth < 11)
        {
            RPC_AddKill(attacker);
        }    
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_UpdateHealth(int newHealth)
    {
        currentHealth = newHealth;
        OnHealthChangedEvent?.Invoke(currentHealth, maxHealth); // Cập nhật UI trên tất cả client
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_AddKill(PlayerRef attacker)
    {
        NetworkObject attackerObj = GetPlayerByRef(attacker);
        if (attackerObj != null)
        {
            var combat = attackerObj.GetComponent<PlayerHealth>();
            combat.KillCount++;
            PlayFabStatsManager.playerKillCount++;
            if (attackerObj.HasInputAuthority)
            {
                KillUIManager.instance.UpdateKillUI(combat.KillCount);
            }
        }
    }

    private NetworkObject GetPlayerByRef(PlayerRef playerRef)
    {
        foreach (var obj in FindObjectsOfType<NetworkObject>())
        {
            // Loại trừ các object không phải player
            if (!obj.GetComponent<PlayerHealth>()) continue;

            //Debug.Log($"[CHECK] Object: {obj.name}, InputAuthority: {obj.InputAuthority}");

            if (obj.InputAuthority == playerRef)
            {
                //Debug.Log($"[FOUND] Found player object for {playerRef}: {obj.name}");
                return obj;
            }
        }

        //Debug.LogError($"[ERROR] Không tìm thấy Player có PlayerRef = {playerRef}");
        return null;
    }
}








