using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagephoton : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManagephoton instance;

    public GameObject SelectedCharacter;

    public string ShowName = "Name";

    public NetworkObject LocalPlayer;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject); // tránh duplicate nếu có nhiều GameManager
        }
    }


}
