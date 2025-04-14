using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabStatsManager : MonoBehaviour
{
    public static int playerKillCount = 0; // Cập nhật giá trị này mỗi lần player kill
    public string statisticName = "TopKills"; // Tên bảng thống kê trên PlayFab

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SubmitKillCount();
        }
    }

    public void SubmitKillCount()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = statisticName,
                    Value = playerKillCount
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result => Debug.Log("[PlayFab] Kill stats submitted successfully."),
            error => Debug.LogError("[PlayFab] Error submitting kill stats: " + error.GenerateErrorReport()));
    }
}
