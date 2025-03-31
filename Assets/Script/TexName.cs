using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class TexName : NetworkBehaviour
{
    // Start is called before the first frame update
    public ShowName pn;
    void Start()
    {
        if(HasInputAuthority == true)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
     GetComponent<Text>().text = "" + pn.NetworkedNickname;   
    }
}
