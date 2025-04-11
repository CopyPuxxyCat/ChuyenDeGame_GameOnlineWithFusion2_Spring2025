using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSendManager : NetworkBehaviour
{
    public void SendChat(string message)
    {
        Debug.Log("goi gui chat 1");
        if (HasInputAuthority)
        {
            Debug.Log("goi gui chat 2");
            ChatReceiveManager.Instance.RPC_ReceiveMessage(Object.InputAuthority, message);
        }
    }
}
