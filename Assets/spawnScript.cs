using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour {

    public float timeToDie = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0) {
            Destroy(gameObject);
        }
	}
}
