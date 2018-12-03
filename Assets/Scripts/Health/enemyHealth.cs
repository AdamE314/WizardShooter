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
            GameObject myPlayer = GameObject.FindGameObjectWithTag("Player");
            this.transform.localEulerAngles = myPlayer.transform.localEulerAngles+(new Vector3(0f,180f,0f));
            var myCorpse = Instantiate(deadPrefab,transform.position,transform.rotation);
            myCorpse.transform.localEulerAngles = myPlayer.transform.localEulerAngles;
            Destroy(gameObject);
        }

	}
}
