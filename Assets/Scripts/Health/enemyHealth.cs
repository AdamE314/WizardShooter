using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour {

    public GameObject deadPrefab;

    public float myHealth = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Death
        if (myHealth <= 0)
        {
            Instantiate(deadPrefab,transform.position,transform.rotation);
            Destroy(gameObject);
        }

	}
}
