using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancingCamera : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("FacingCamera"))
        {
            go.transform.LookAt(transform.position);
        }
    }
}
