using Fusion;
using UnityEngine;
using System.Collections.Generic;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab; // Prefab nhân vật sẽ spawn
    private Dictionary<PlayerRef, NetworkObject> spawnedPlayers = new Dictionary<PlayerRef, NetworkObject>(); // Lưu danh sách người chơi đã spawn

    void Start()
    {
        if (GameManagephoton.instance != null)
        {
            playerPrefab = GameManagephoton.instance.SelectedCharacter;
        }
        else
        {
            Debug.LogError("GameManagephoton instance is null!");
        }
    }

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            if (playerPrefab != null)
            {
                Runner.Spawn(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity, player, (runner, obj) =>
                {
                    spawnedPlayers[player] = obj; // Lưu thông tin người chơi đã spawn
                });
            }
            else
            {
                Debug.LogError("Player Prefab is not assigned!");
            }
        }
    }
}
