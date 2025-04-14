using TMPro;
using UnityEngine;

public class KillUIManager : MonoBehaviour
{
    public static KillUIManager instance;
    public TMP_Text killText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateKillUI(int kills)
    {
        killText.text =  ($"{kills}");
    }
}
