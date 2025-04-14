using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public TMP_Text leaderboardText;
    public string statisticName = "TopKills";
    public bool isPanelActive = false;

    private void Start()
    {
        leaderboardPanel.SetActive(isPanelActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            
            GetLeaderboard();
            isPanelActive = !isPanelActive;
            leaderboardPanel.SetActive(!isPanelActive);
            
        }
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = statisticName,
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, result =>
        {
            leaderboardText.text = "<b>🏆 Top 10 Killers 🏆</b>\n";

            foreach (var entry in result.Leaderboard)
            {
                leaderboardText.text += $"{entry.Position + 1}. {entry.DisplayName ?? entry.PlayFabId} - {entry.StatValue} kills\n";
            }

        }, error =>
        {
            Debug.LogError("[PlayFab] Failed to get leaderboard: " + error.GenerateErrorReport());
        });
    }
}

