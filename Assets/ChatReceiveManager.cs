using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatReceiveManager : NetworkBehaviour
{
    public static ChatReceiveManager Instance;

    void Awake()
    {
        Instance = this;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_ReceiveMessage(PlayerRef sender, string message)
    {
        Debug.Log("nhan chat tu player");
        string senderName = sender.ToString(); // Hoặc tên tùy chỉnh
        ChatPanelManager.Instance.AddMessage(senderName, message);
    }
}

