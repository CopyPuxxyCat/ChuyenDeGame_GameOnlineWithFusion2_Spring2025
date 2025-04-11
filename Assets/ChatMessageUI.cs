using TMPro;
using UnityEngine;

public class ChatMessageUI : MonoBehaviour
{
    public TMP_Text text;

    public void Setup(string sender, string message)
    {
        text.text = $"<b>{sender}:</b> {message}";
    }
}
