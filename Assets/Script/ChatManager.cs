/*using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatManager : NetworkBehaviour
{
    public TMP_InputField inputField;
    public Button sendButton;
    public Transform messageContainer;
    public GameObject messagePrefab;
    public float messageLifetime = 300f;

    private List<GameObject> messageObjects = new();
    private bool isReady = false; // ✅ Đảm bảo Spawned xong mới chạy

    public GameObject chatPanel;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            sendButton.onClick.AddListener(SendMessageToServer);
        }
        else
        {
            inputField.interactable = false;
            sendButton.interactable = false;
        }

        isReady = true; // ✅ Đánh dấu đã Spawned
    }

    void Update()
    {
        *//*if (!isReady) return; // ✅ Tránh lỗi nếu chưa Spawned
        if (!Object.HasInputAuthority) return;*//*

        // Mở chat bằng Enter
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("call M");
            chatPanel.SetActive(true);
            inputField.ActivateInputField();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Ẩn chat bằng Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            chatPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    

    public void SendMessageToServer()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) return;

        string nickname = GameManagephoton.instance.ShowName;
        string content = inputField.text;
        inputField.text = "";

        RPC_SendMessage(nickname, content);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendMessage(string nickname, string message)
    {
        GameObject msgObj = Instantiate(messagePrefab, messageContainer);
        msgObj.GetComponent<TMP_Text>().text = $"<b>{nickname}:</b> {message}";
        messageObjects.Add(msgObj);
        Destroy(msgObj, messageLifetime);
    }
}*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class ChatManager : NetworkBehaviour
{
    public GameObject chatPanel; // Panel chứa InputField và messages
    public TMP_InputField inputField;
    public Button sendButton;
    public Transform messageContainer;
    public GameObject messagePrefab;
    public float messageLifetime = 300f;

    private List<GameObject> messageObjects = new List<GameObject>();
    private bool chatVisible = false;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            sendButton.onClick.AddListener(OnSendClicked);
            chatPanel.SetActive(false); // Tắt chat khi bắt đầu
        }
        else
        {
            inputField.interactable = false;
            sendButton.interactable = false;
        }
    }

    void Update()
    {
        if (!Object.HasInputAuthority) return;

        // Hiện chat bằng Enter
        if (Input.GetKeyDown(KeyCode.Return))
        {
            chatVisible = !chatVisible;
            chatPanel.SetActive(chatVisible);

            if (chatVisible)
            {
                inputField.ActivateInputField();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        // Ẩn chat bằng ESC
        if (Input.GetKeyDown(KeyCode.Escape) && chatVisible)
        {
            chatVisible = false;
            chatPanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnSendClicked()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) return;

        string nickname = GameManagephoton.instance.ShowName;
        string message = inputField.text;
        inputField.text = "";

        RPC_SendMessage(nickname, message);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_SendMessage(string nickname, string message)
    {
        GameObject msgObj = Instantiate(messagePrefab, messageContainer);
        msgObj.GetComponent<ChatMessageUI>().Setup(nickname, message);
        Destroy(msgObj, messageLifetime);
    }
}





