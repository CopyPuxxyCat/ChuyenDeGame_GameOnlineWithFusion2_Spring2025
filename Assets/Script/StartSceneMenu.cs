using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartSceneMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField inp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveNickName()
    {
        GameManagephoton.instance.ShowName = inp.text;
        SceneManager.LoadSceneAsync("MultiplayerGame");
    }
    public void ChooseCharacter(GameObject character)
    {
        GameManagephoton.instance.SelectedCharacter = character;
    }
}
