using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatPanelManager : MonoBehaviour
{
    public static ChatPanelManager Instance;

    [Header("UI References")]
    public GameObject chatPanel;               // Giao diện chính
    public Transform messageContainer;         // Nơi chứa các message
    public GameObject messagePrefab;           // Prefab cho mỗi message
    public TMP_InputField inputField;          // Ô nhập văn bản

    private bool isChatOpen = false;

    void Awake()
    {
        Instance = this;
        chatPanel.SetActive(false); // Đảm bảo chat panel ẩn lúc đầu
    }

    void Update()
    {
        // Toggle mở/tắt panel bằng phím M
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("M value:  " + GameManagephoton.instance.ShowName + isChatOpen);
            isChatOpen = !isChatOpen;
            chatPanel.SetActive(isChatOpen);
            SetActiveAllChildren(chatPanel, isChatOpen);

            if (isChatOpen)
            {
                inputField.ActivateInputField();
            }
        }

        // Gửi tin nhắn khi bấm Enter (và chat đang mở)
        if (isChatOpen && Input.GetKeyDown(KeyCode.Return))
        {
            SendMessageFromInput();
        }
    }

    // Hàm thêm message hiển thị lên panel
    public void AddMessage(string sender, string message)
    {
        GameObject newMessage = Instantiate(messagePrefab, messageContainer);
        newMessage.GetComponent<ChatMessageUI>().Setup(sender, message);
    }

    // Gửi nội dung đang có trong input
    public void SendMessageFromInput()
    {
        Debug.Log("nhan enter");
        string msg = inputField.text;

        if (!string.IsNullOrEmpty(msg))
        {
            Debug.Log("nhan enter 1");
            // Gửi chat qua ChatSendManager trên Player
            GameManagephoton.instance.LocalPlayer
                .GetComponent<ChatSendManager>()
                .SendChat(msg);
            Debug.Log("nhan enter 2");
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }

    void SetActiveAllChildren(GameObject parent, bool active)
    {
        foreach (Transform child in parent.transform)
        {
            child.gameObject.SetActive(active);
            SetActiveAllChildren(child.gameObject, active);
        }
    }
}


