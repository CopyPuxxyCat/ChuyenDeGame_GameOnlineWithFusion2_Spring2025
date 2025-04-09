using TMPro;
using UnityEngine;

public class ChatMessageUI : MonoBehaviour
{
    public TMP_Text messageText;

    public void Setup(string nickname, string message)
    {
        if (messageText != null)
        {
            messageText.text = $"<b>{nickname}:</b> {message}";
        }
        else
        {
            Debug.LogError("messageText is not assigned!");
        }
    }
}
