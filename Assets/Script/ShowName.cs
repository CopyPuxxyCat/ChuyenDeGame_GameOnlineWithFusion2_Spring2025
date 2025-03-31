using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowName : NetworkBehaviour
{
    // Start is called before the first frame update
    [Networked, OnChangedRender(nameof(NicknameChanged))]
    public string NetworkedNickname { get; set; } = "Name";

    void Start()
    {
        NetworkedNickname = GameManagephoton.instance.ShowName;
    }
    void NicknameChanged()
    {
        Debug.Log($"Nickname changed to: {NetworkedNickname}");
    }
}
