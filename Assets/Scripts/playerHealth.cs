using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour {

    public float myHealth = 100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (myHealth <= 0)
        {
            gameObject.GetComponent<playerController>().enabled = false;
        }

	}
}
