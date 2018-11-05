using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    
    private GameObject myPlayer;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //transform.LookAt(myCamera.transform.position, -Vector3.up);
        transform.localEulerAngles = myPlayer.transform.localEulerAngles;
    }
}

