using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardTwo : MonoBehaviour {

    private GameObject myPlayer;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update () {
        transform.localEulerAngles = myPlayer.transform.localEulerAngles + (new Vector3(0f, 180f, 0f));

    }
}
