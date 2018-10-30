using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    
    public GameObject myPlayer;

    void Update()
    {
        //transform.LookAt(myCamera.transform.position, -Vector3.up);
        transform.localEulerAngles = myPlayer.transform.localEulerAngles;
    }
}

