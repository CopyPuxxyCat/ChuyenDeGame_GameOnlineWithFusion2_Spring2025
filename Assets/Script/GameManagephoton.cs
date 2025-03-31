using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagephoton : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManagephoton instance;

    public GameObject SelectedCharacter;

    public string ShowName = "Name";
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
