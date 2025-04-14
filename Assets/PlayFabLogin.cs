using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    void Start()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };


        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }


    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Đăng nhập PlayFab thành công!" + result.PlayFabId);
        GetAccountInfo();
    }


    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Đăng nhập thất bại: " + error.GenerateErrorReport());
    }

    void GetAccountInfo()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), result =>
        {
            string currentName = result.AccountInfo.TitleInfo.DisplayName;

            if (string.IsNullOrEmpty(currentName))
            {
                // Đặt tên hiển thị lần đầu, ví dụ random hoặc cho phép người dùng nhập sau này
                string newDisplayName = "Player_" + GameManagephoton.instance.ShowName;
                SetDisplayName(newDisplayName);
            }

        }, error =>
        {
            Debug.LogError("Lỗi lấy AccountInfo: " + error.GenerateErrorReport());
        });
    }

    void SetDisplayName(string name)
    {
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        },
        result => Debug.Log("Tên hiển thị đã được cập nhật: " + result.DisplayName),
        error => Debug.LogError("Lỗi cập nhật tên hiển thị: " + error.GenerateErrorReport()));
    }

}
